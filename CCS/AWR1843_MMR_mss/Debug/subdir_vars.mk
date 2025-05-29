################################################################################
# Automatically-generated file. Do not edit!
################################################################################

SHELL = cmd.exe

# Add inputs and outputs from these tool invocations to the build variables 
CMD_SRCS += \
../mss_mmw_linker.cmd 

ASM_SRCS += \
../sys_intvecs.asm \
../sys_startup.asm 

C_SRCS += \
../can_message.c \
../cli.c \
../mss_main.c \
../sleep.c 

C_DEPS += \
./can_message.d \
./cli.d \
./mss_main.d \
./sleep.d 

OBJS += \
./can_message.obj \
./cli.obj \
./mss_main.obj \
./sleep.obj \
./sys_intvecs.obj \
./sys_startup.obj 

ASM_DEPS += \
./sys_intvecs.d \
./sys_startup.d 

OBJS__QUOTED += \
"can_message.obj" \
"cli.obj" \
"mss_main.obj" \
"sleep.obj" \
"sys_intvecs.obj" \
"sys_startup.obj" 

C_DEPS__QUOTED += \
"can_message.d" \
"cli.d" \
"mss_main.d" \
"sleep.d" 

ASM_DEPS__QUOTED += \
"sys_intvecs.d" \
"sys_startup.d" 

C_SRCS__QUOTED += \
"../can_message.c" \
"../cli.c" \
"../mss_main.c" \
"../sleep.c" 

ASM_SRCS__QUOTED += \
"../sys_intvecs.asm" \
"../sys_startup.asm" 


