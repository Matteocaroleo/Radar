/*
 * mss_functions.h
 *
 *  Created on: 22 ago 2019
 *      Author: X86
 */

#ifndef MSS_FUNCTIONS_H_
#define MSS_FUNCTIONS_H_

void cmdUartSetup(bool flag);
void configureDSS();
int32_t send_configuration();
int32_t stopProcedure();
int32_t startProcedure();
//static int32_t sensorStop();
//static int32_t sensorStart();
static int32_t notifySensorStart();
static int32_t notifySensorStop();

extern int32_t MmwDemo_mboxWrite(MmwDemo_message     * message);



#endif /* MSS_FUNCTIONS_H_ */
