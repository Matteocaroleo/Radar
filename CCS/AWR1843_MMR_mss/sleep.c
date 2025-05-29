/* Standard Include Files. */
#include <sleep.h>

//#define N_MS 13300
//#define N_S  2000000

void sleep_ms(uint32_t n){

    for (volatile int i=0; i<n*N_MS; i++);
}
//0.1 s
void sleep_c(uint32_t n){

    for (volatile int i=0; i<n*N_S; i++);
}
