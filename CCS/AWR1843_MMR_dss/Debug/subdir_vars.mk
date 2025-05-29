################################################################################
# Automatically-generated file. Do not edit!
################################################################################

SHELL = cmd.exe

# Add inputs and outputs from these tool invocations to the build variables 
CMD_SRCS += \
../dss_mmw_linker.cmd 

SYSCFG_SRCS += \
../empty.syscfg 

ASM_SRCS += \
../intvecs.asm 

C_SRCS += \
../dss_config_edma_util.c \
../dss_data_path.c \
../dss_main.c \
../dss_vitalSignsDemo_utilsFunc.c \
./syscfg/pin_mux_config.c 

GEN_FILES += \
./syscfg/pin_mux_config.c 

GEN_MISC_DIRS += \
./syscfg/ 

C_DEPS += \
./dss_config_edma_util.d \
./dss_data_path.d \
./dss_main.d \
./dss_vitalSignsDemo_utilsFunc.d \
./syscfg/pin_mux_config.d 

OBJS += \
./dss_config_edma_util.oe674 \
./dss_data_path.oe674 \
./dss_main.oe674 \
./dss_vitalSignsDemo_utilsFunc.oe674 \
./syscfg/pin_mux_config.oe674 \
./intvecs.oe674 

ASM_DEPS += \
./intvecs.d 

GEN_MISC_FILES += \
./syscfg/pin_mux_config.h \
./syscfg/summary.csv 

GEN_MISC_DIRS__QUOTED += \
"syscfg\" 

OBJS__QUOTED += \
"dss_config_edma_util.oe674" \
"dss_data_path.oe674" \
"dss_main.oe674" \
"dss_vitalSignsDemo_utilsFunc.oe674" \
"syscfg\pin_mux_config.oe674" \
"intvecs.oe674" 

GEN_MISC_FILES__QUOTED += \
"syscfg\pin_mux_config.h" \
"syscfg\summary.csv" 

C_DEPS__QUOTED += \
"dss_config_edma_util.d" \
"dss_data_path.d" \
"dss_main.d" \
"dss_vitalSignsDemo_utilsFunc.d" \
"syscfg\pin_mux_config.d" 

GEN_FILES__QUOTED += \
"syscfg\pin_mux_config.c" 

ASM_DEPS__QUOTED += \
"intvecs.d" 

C_SRCS__QUOTED += \
"../dss_config_edma_util.c" \
"../dss_vitalSignsDemo_utilsFunc.c" \
"./syscfg/pin_mux_config.c" 

SYSCFG_SRCS__QUOTED += \
"../empty.syscfg" 

ASM_SRCS__QUOTED += \
"../intvecs.asm" 


