#ifndef CAN_MESSAGE_H
#define CAN_MESSAGE_H

#include <stdint.h>
#include <stdio.h>
//#include <ti/drivers/can/can.h>
#include "communication1843.h"
#include "sleep.h"
#include "mss_mmw.h"
/******************************************************************************
 *  @b Description
 *  @n
 *     Function to write the create N=data_message_size/8byte an messages given the full frame message.
 *     Can Message is:
 *     -1 byte (excessive):         Target ID
 *     -1 byte (not enough):        x
 *     -1 byte (maybe not enough):  y
 *     -1 byte:                     z
 *     -1 byte(excessive):          Classification
 *     -1 byte:                     Auxiliary (to be decided what it is- Maybe Temperature Control or Stop flag when target is near or other stuff)
 *
 *
 *  @param[in]     *data_message        pointer to the message of the data of frame
 *  @param[in]      data_message_size   size of the message in bytes
 *
 *
 ******************************************************************************/
//void send_can_message(uint8_t *data_message,uint16_t data_message_size, uint16_t idVal);

#endif //CAN_MESSAGE_H
