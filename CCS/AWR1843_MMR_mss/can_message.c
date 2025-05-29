//#include <can_message.h>
//#include <ti/drivers/can/can.h>

//extern MmwDemo_MCB  gMmwMssMCB;
//extern CAN_DCANData dcanTxData;
//extern volatile uint32_t gTxDoneFlag;
//extern CAN_DCANMsgObjCfgParams dcanTxCfgParams;

//void send_can_message(uint8_t *data_message,uint16_t data_message_size, uint16_t idVal)
//{
//    int8_t Npackets;
//    int32_t retVal;
//    int32_t errCode;
//    uint32_t iterationCount = 0U;
//    uint8_t flag_last=0;
//
//    Npackets=(data_message_size)/8;
//    uint8_t rest = (data_message_size)%8;
//
//    if(rest!=0)
//        flag_last=1;
//
//    uint32_t j=0;
//    uint8_t zeros[] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
//
//    while(j<Npackets||flag_last==1)
//    {
//        //   for(i=0;i<8;i++)
//        if(j<Npackets)
//        {
//            memcpy((void *)&dcanTxData.msgData[0], (void *)&data_message[j*8], 8);
//        } else {
//            if(flag_last)
//            {
//                memcpy((void *)&dcanTxData.msgData[0], (void *)&data_message[j*8], rest);
//                memcpy((void *)&dcanTxData.msgData[0], (void *)&zeros, (8-rest));
//                flag_last = 0;
//            }
//        }
//
//        //Send data over Tx message object
//        dcanTxCfgParams.msgIdentifier = idVal;
//
//        gMmwMssMCB.txMsgObjHandle = CAN_createMsgObject (gMmwMssMCB.canHandle, 0x1U, &dcanTxCfgParams, &errCode);
//        if (gMmwMssMCB.txMsgObjHandle == NULL)
//        {
//            printf("Error: CAN create Tx message object failed [Error code %d]\n", errCode);
//
//        }
//
//        retVal = CAN_transmitData (gMmwMssMCB.txMsgObjHandle, &dcanTxData, &errCode);
//        if (retVal < 0)
//        {
//            printf("Error: CAN transmit data for iteration %d failed [Error code %d]\n", iterationCount, errCode);
//        }
//
//        //wait completion 200us, (if the number of target increase it may require to be increased
//        //for (int i=0;i<7500;i++);
//        while(!gTxDoneFlag);
//
//        gTxDoneFlag=0;
//
//        j++;
//    }
//}
