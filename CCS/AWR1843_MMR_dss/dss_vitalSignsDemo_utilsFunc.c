/**
 *   @file  post_processing.c
 *
 *   @brief
 *      Routines for post processing and Miscellaneous Functions for the Vital Signs Demo
 *
 *  \par

 *  NOTE:
 * Copyright (c) 2018 Texas Instruments Incorporated
 *
 * All rights reserved not granted herein.
 * Limited License.
 *
 * Texas Instruments Incorporated grants a world-wide, royalty-free, non-exclusive license
 * under copyrights and patents it now or hereafter owns or controls to make, have made, use,
 * import, offer to sell and sell ("Utilize") this software subject to the terms herein.
 *
 * With respect to the foregoing patent license, such license is granted  solely to the extent
 * that any such patent is necessary to Utilize the software alone.  The patent license shall
 * not apply to any combinations which include this software, other than combinations with
 * devices manufactured by or for TI (“TI Devices”). No hardware patent is licensed hereunder.
 *
 * Redistributions must preserve existing copyright notices and reproduce this license
 * (including the above copyright notice and the disclaimer and (if applicable) source code
 * license limitations below) in the documentation and/or other materials provided with the
 * distribution.
 *
 * Redistribution and use in binary form, without modification, are permitted provided that
 * the following conditions are met:
 *
 * No reverse engineering, decompilation, or disassembly of this software is permitted with
 * respect to any software provided in binary form. Any redistribution and use are licensed
 * by TI for use only with TI Devices. Nothing shall obligate TI to provide you with source
 * code for the software licensed and provided to you in object code.
 *
 * If software source code is provided to you, modification and redistribution of the source
 * code are permitted provided that the following conditions are met:
 *
 * Any redistribution and use of the source code, including any resulting derivative works,
 * are licensed by TI for use only with TI Devices.
 * Any redistribution and use of any object code compiled from the source code and any
 * resulting derivative works, are licensed by TI for use only with TI Devices.
 *
 * Neither the name of Texas Instruments Incorporated nor the names of its suppliers may be
 * used to endorse or promote products derived from this software without specific prior
 * written permission.
 *
 * DISCLAIMER.
 *
 * THIS SOFTWARE IS PROVIDED BY TI AND TI’S LICENSORS "AS IS" AND ANY EXPRESS OR IMPLIED
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL TI AND TI’S
 * LICENSORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
 * OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

/* Standard Include Files. */
#include "dss_vitalSignsDemo_utilsFunc.h"

#include <stdint.h>
#include <stdlib.h>
#include <stddef.h>
#include <string.h>
#include <stdio.h>
#include <math.h>

/** @fn  float filter_IIR_BiquadCascade()
*   @brief Filters the data 
*
*/
#define PI 3.1415926535897


float  filter_IIR_BiquadCascade(float DataIn, float *pFilterCoefs, float *pScaleVals, float *pDelay, uint16_t numStages)
{
    // The implementation is a “Direct Form 2” type that requires only 2 delays per Filter.
    // https://en.wikipedia.org/wiki/Digital_biquad_filter
    // https://www.advsolned.com/implementing-biquad-iir-filters-with-the-asn-filter-designer-and-the-arm-cmsis-dsp-software-framework/
	float a1,a2;
	float b0,b1,b2;
	float y, input, output;
	float scaleVal;
	uint16_t indexStage;
	uint16_t numCoefsStage;
	uint16_t indexTemp;
	
	numCoefsStage = 6;
	input = DataIn;
	for (indexStage = 0; indexStage < numStages; indexStage++)
	{
		indexTemp = numCoefsStage*indexStage;
		b0 = pFilterCoefs[ indexTemp ];
		b1 = pFilterCoefs[ indexTemp + 1];
		b2 = pFilterCoefs[ indexTemp + 2];
		a1 = pFilterCoefs[ indexTemp + 4];
		a2 = pFilterCoefs[ indexTemp + 5];
		scaleVal = pScaleVals[indexStage];

		pDelay[indexTemp] = scaleVal*input - a1*pDelay[indexTemp + 1] - a2*pDelay[indexTemp + 2];
        y =  b0*pDelay[indexTemp] + b1*pDelay[indexTemp + 1] + b2*pDelay[indexTemp + 2];

        pDelay[indexTemp + 2] = pDelay[indexTemp + 1];
        pDelay[indexTemp + 1] = pDelay[indexTemp];

	    input = y;
    }
	output = y;

	return output;

}


float unwrap(float phase, float phasePrev, float *diffPhaseCorrectionCum)
{
	float modFactorF;
	float diffPhase;
	float diffPhaseMod;
	float diffPhaseCorrection;
	float phaseOut;

	// incremental phase variation
	diffPhase = phase - phasePrev;

	if (diffPhase > PI)
		modFactorF = 1;
	else if (diffPhase < - PI)
	    modFactorF = -1;
	else
		modFactorF = 0;

	diffPhaseMod = diffPhase - modFactorF*2*PI;

	// preserve variation sign for +pi vs. -pi
	if ((diffPhaseMod == -PI) && (diffPhase > 0))
		diffPhaseMod = PI;

	// incremental phase correction
	diffPhaseCorrection = diffPhaseMod - diffPhase;

	// Ignore correction when incremental variation is smaller than cutoff
	if (((diffPhaseCorrection < PI) && (diffPhaseCorrection>0)) ||
			((diffPhaseCorrection > -PI) && (diffPhaseCorrection<0)))
		diffPhaseCorrection = 0;

	// Find cumulative sum of deltas
	*diffPhaseCorrectionCum = *diffPhaseCorrectionCum + diffPhaseCorrection;
	phaseOut = phase + *diffPhaseCorrectionCum;
    return phaseOut;
 }


float  filter_RemoveImpulseNoise(float dataPrev2, float dataPrev1 , float dataCurr, float thresh)
{
	float forwardDiff,backwardDiff;
	float x1, x2, y1, y2, x, y;
    float pDataIn[3];

    pDataIn[0] = dataPrev2 ;
	pDataIn[1] = dataPrev1 ;
	pDataIn[2] = dataCurr  ;

	backwardDiff = pDataIn[1] - pDataIn[0];
	forwardDiff  = pDataIn[1] - pDataIn[2];

	x1 = 0;
	x2 = 2;
	y1 = pDataIn[0];
	y2 = pDataIn[2];
	x  = 1;

	/* Interpolate between x1 and x2 if the forwardDiff and backwardDiff exceed the threshold */
	if ( ((forwardDiff > thresh) && (backwardDiff > thresh)) || ((forwardDiff < -thresh) && (backwardDiff < -thresh)))
	y = y1 + ( ((x-x1)*(y2 - y1))/(x2 - x1) );
	else
	y = pDataIn[1];

	return y;
}




