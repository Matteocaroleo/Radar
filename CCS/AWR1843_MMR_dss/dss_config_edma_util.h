
#ifndef DSS_CONFIG_EDMA_UTIL_H
#define DSS_CONFIG_EDMA_UTIL_H

#include <ti/drivers/edma/edma.h>

#ifdef __cplusplus
extern "C" {
#endif

/******************************************************************************
 *  @b Description
 *  @n
 *     Sets up a EDMA channel for transferring a contiguous set of data
 *     (of length Acnt Bytes) from a source to a destination buffer.
 *     The EDMA can be configured for multiple transfers, so that subsequent
 *     initiations of the EDMA channel, initiate these subsequent transfers.
 *     The number of such multiple transfers is defined by Bcnt.
 *     For subsequent transfers the start pointer of the source buffer increments
 *     by srcBIdx bytes. The destination buffer remains the same across subsequent
 *     transfers. The EDMA is programmed as an "A-Synchronized transfer")
 *
 *     The ParamSet (parameter set) into which the EDMA is programmed is the same
 *     as the EDMA channel number. Additionally, the same parameters are programmed
 *     into a shadow ParamSet which is linked to the original ParamSet. Thus after all
 *     the Bnt transfers are complete, the ParamSet is automatically reinitialized
 *     and ready for another set of Bcnt transfers.
 *
 *     This routine is useful when data from external memory has to be brought
 *     sequentially in chunks into the DSP's internal buffer (L1/L2) for signal
 *     processing. The automatic reinitialization ensures that a one-time
 *     programming suffices (e.g.does not need to be re-programmed every frame)
 *
 *
 *  @param[in]  srcBuff        Pointer to source buffer
 *  @param[in]  dstBuff        Pointer to destination buffer
 *  @param[in]  chId           EDMA channel Id. (The ParamSet allocated to
 *                             this EDMA is also indexed by this EDMA channel number).
 *  @param[in]  isEventTriggered true if event triggered else (manual/chain triggered) false. 
 *  @param[in]  shadowParamId  EDMA channel number for "shadow channels" which
 *                             stores a replica of the orignal EDMA channel.
 *  @param[in]                 This enables reload of the EDMA channel registers
 *                             once an AB-synchronized transfer is complete.
 *  @param[in]  aCount         This specifies the Acnt of the transfer. (i.e number
 *                             of contiguous bytes per transfer).
 *  @param[in]  bCount         Number of EDMA transfers (each of length Acnt bytes).
 *  @param[in]  srcBIdx        Specifies the byte offset between the source pointer
 *                             of subsequent EDMA transfers.
 *  @param[in]  dstBIdx        Specifies the byte offset between the source pointer
 *                             of subsequent EDMA transfers.
 *  @param[in]  eventQueueId   Event Queue Id on which to schedule the transfer.
 *  @param[in]  transferCompletionCallbackFxn Transfer completion call back function.
 *  @param[in]  transferCompletionCallbackFxnArg Transfer completion call back function argument.
 *  @param[in]  isEventTriggered true if event triggered else (manual/chain triggered) false.
 *
 *  @retval
 *      EDMA driver error code, see "EDMA_ERROR_CODES" in EDMA API.
 *
 ******************************************************************************/
int32_t EDMAutil_configType1(EDMA_Handle handle,
    uint8_t *srcBuff,
    uint8_t *dstBuff,
    uint8_t chId,
    bool isEventTriggered,
    uint16_t shadowParamId,
    uint16_t aCount,
    uint16_t bCount,
    int16_t srcBIdx,
    int16_t dstBIdx,
    uint8_t eventQueueId,
    EDMA_transferCompletionCallbackFxn_t transferCompletionCallbackFxn,
    uintptr_t transferCompletionCallbackFxnArg);


/******************************************************************************
 *  @b Description
 *  @n
 *    Reconfigures source and destination addresses of a given channel Id
 *    (whose param Id is assumed to be already configured to be same as channel Id)
 *    and then starts a transfer on that channel.
 *
 *  @param[in]  srcBuff        Pointer to source buffer. If NULL, does not update.
 *  @param[in]  dstBuff        Pointer to destination buffer. If NULL, does not update.
 *  @param[in]  chId           EDMA channel Id.
 *  @param[in]  triggerEnabled =1: trigger EDMA, =0: does not trigger EDMA
 *
 *  @retval
 *      EDMA driver error code, see "EDMA_ERROR_CODES" in EDMA API.
 *
 *******************************************************************************/
int32_t EDMAutil_setAndTrigger(EDMA_Handle handle,
    uint8_t *srcBuff,
    uint8_t *dstBuff,
    uint8_t chId,
    uint8_t triggerEnabled);

#ifdef __cplusplus
}
#endif
   
#endif
