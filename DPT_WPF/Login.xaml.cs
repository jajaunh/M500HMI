using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DPT_WPF
{
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Window
    {

        string machineName = "";

        public Login()
        {

            InitializeComponent();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string tempPath = @"C:\Depert_Profile\machineProfile3.dpf";
            string _contentProfile = "";

            using (StreamReader reader = new StreamReader(tempPath))
            {
                _contentProfile = reader.ReadToEnd();
            }

            DefineValue dv = new DefineValue();

            #region FTP-Profile
            cd_Profile cp = new cd_Profile();

            var dic = cp.SplitProfile(_contentProfile);

            DefineValueSetter(dic);

            #endregion
        }

        private DefineValue DefineValueSetter(Dictionary<string, string> dic)
        {
            DefineValue dv = new DefineValue();

            dv.FtpName = dic["FTPName"];
            dv.HostName = dic["HostName"];
            dv.UserName = dic["ID"];
            dv.Password = dic["PW"];
            machineName = dic["Machine Name"];

            string readCount = dic["ReadCount"];
            string writeCount = dic["WrtieCount"];
            dv.ReadCount = Convert.ToInt32(readCount);

            dv.OPCItemNameTextBoxes = new string[dv.ReadCount];
            dv.OPCItemValueTextBoxes = new string[Convert.ToInt32(readCount)];
            dv.OPCItemQualityTextBoxes = new string[Convert.ToInt32(readCount)];

            dv.OPCItemWriteValueTextBoxes = new string[Convert.ToInt32(writeCount)];
            dv.OPCItemNameWriteTextBoxes = new string[Convert.ToInt32(writeCount)];

            #region Read (486)
            /*===============================Read==================================*/

            //[gIntp.Input.Machine.Command]
            dv.OPCItemNameTextBoxes[0] = dic["bAutoParamApply"];
            dv.OPCItemNameTextBoxes[1] = dic["bAutoPause"];
            dv.OPCItemNameTextBoxes[2] = dic["bAutoStart"];
            dv.OPCItemNameTextBoxes[3] = dic["bStop"];
            dv.OPCItemNameTextBoxes[4] = dic["bHomeParamApply"];
            dv.OPCItemNameTextBoxes[5] = dic["bInternalOperationOn"];
            dv.OPCItemNameTextBoxes[6] = dic["bHomeAll"];
            dv.OPCItemNameTextBoxes[7] = dic["bHomeStop"];
            dv.OPCItemNameTextBoxes[8] = dic["bResetHome"];
            dv.OPCItemNameTextBoxes[408] = dic["bDocking"];
            dv.OPCItemNameTextBoxes[409] = dic["bUnDocking"];
            dv.OPCItemNameTextBoxes[410] = dic["bDockingConfirm"];
            dv.OPCItemNameTextBoxes[411] = dic["bUnDockingConfirm"];
            dv.OPCItemNameTextBoxes[459] = dic["bPrintStart"];
            dv.OPCItemNameTextBoxes[475] = dic["bPrintFinsh"];

            //[gIntp.Input.Machine.Param]
            dv.OPCItemNameTextBoxes[9] = dic["rSpeedOverride"];
            dv.OPCItemNameTextBoxes[10] = dic["rMovePosition"];
            dv.OPCItemNameTextBoxes[11] = dic["rDockingPosition"];
            dv.OPCItemNameTextBoxes[12] = dic["rUnDockingPosition"];
            dv.OPCItemNameTextBoxes[13] = dic["rReadyPosition"];
            dv.OPCItemNameTextBoxes[14] = dic["rPowderFeederAngle"];
            dv.OPCItemNameTextBoxes[15] = dic["rPowderGetPosition"];
            dv.OPCItemNameTextBoxes[16] = dic["rPrintDistance"];
            dv.OPCItemNameTextBoxes[17] = dic["rRecoaterMovePosition"];

            //[gIntp.Input.Axes[0].Command]
            dv.OPCItemNameTextBoxes[18] = dic["bAbsolute_Xco"];
            dv.OPCItemNameTextBoxes[19] = dic["bAdditiveNeg_Xco"];
            dv.OPCItemNameTextBoxes[20] = dic["bAdditivePos_Xco"];
            dv.OPCItemNameTextBoxes[21] = dic["bCyclicPos_Xco"];
            dv.OPCItemNameTextBoxes[22] = dic["bHome_Xco"];
            dv.OPCItemNameTextBoxes[23] = dic["bHomeCalibration_Xco"];
            dv.OPCItemNameTextBoxes[24] = dic["bJogNeg_Xco"];
            dv.OPCItemNameTextBoxes[25] = dic["bJogPos_Xco"];

            //[gIntp.Input.Axes[0].Param]
            dv.OPCItemNameTextBoxes[26] = dic["rDistance_Xpa"];
            dv.OPCItemNameTextBoxes[27] = dic["nHomeMode_Xpa"];
            dv.OPCItemNameTextBoxes[28] = dic["nHomeOrder_Xpa"];
            dv.OPCItemNameTextBoxes[29] = dic["nHomeType_Xpa"];
            dv.OPCItemNameTextBoxes[30] = dic["rHomeVelocity_Xpa"];
            dv.OPCItemNameTextBoxes[31] = dic["rHomeAcceleration_Xpa"];
            dv.OPCItemNameTextBoxes[32] = dic["rHomeDeceleration_Xpa"];
            dv.OPCItemNameTextBoxes[33] = dic["rHomeDistance_Xpa"];
            dv.OPCItemNameTextBoxes[34] = dic["rHomePosition_Xpa"];
            dv.OPCItemNameTextBoxes[35] = dic["nHomeSensorPosType_Xpa"];
            dv.OPCItemNameTextBoxes[36] = dic["rPosition_Xpa"];

            //[gIntp.Input.Axes[1].Command]
            dv.OPCItemNameTextBoxes[37] = dic["bAbsolute_Rco"];
            dv.OPCItemNameTextBoxes[38] = dic["bAdditiveNeg_Rco"];
            dv.OPCItemNameTextBoxes[39] = dic["bAdditivePos_Rco"];
            dv.OPCItemNameTextBoxes[40] = dic["bCyclicPos_Rco"];
            dv.OPCItemNameTextBoxes[41] = dic["bHome_Rco"];
            dv.OPCItemNameTextBoxes[42] = dic["bHomeCalibration_Rco"];
            dv.OPCItemNameTextBoxes[43] = dic["bJogNeg_Rco"];
            dv.OPCItemNameTextBoxes[44] = dic["bJogPos_Rco"];

            //[gIntp.Input.Axes[1].Param]
            dv.OPCItemNameTextBoxes[45] = dic["rDistance_Rpa"];
            dv.OPCItemNameTextBoxes[46] = dic["nHomeMode_Rpa"];
            dv.OPCItemNameTextBoxes[47] = dic["nHomeOrder_Rpa"];
            dv.OPCItemNameTextBoxes[48] = dic["nHomeType_Rpa"];
            dv.OPCItemNameTextBoxes[49] = dic["rHomeVelocity_Rpa"];
            dv.OPCItemNameTextBoxes[50] = dic["rHomeAcceleration_Rpa"];
            dv.OPCItemNameTextBoxes[51] = dic["rHomeDeceleration_Rpa"];
            dv.OPCItemNameTextBoxes[52] = dic["rHomeDistance_Rpa"];
            dv.OPCItemNameTextBoxes[53] = dic["rHomePosition_Rpa"];
            dv.OPCItemNameTextBoxes[54] = dic["nHomeSensorPosType_Rpa"];
            dv.OPCItemNameTextBoxes[55] = dic["rPosition_Rpa"];

            //[gIntp.Input.Axes[2].Command]
            dv.OPCItemNameTextBoxes[56] = dic["bAbsolute_Zco"];
            dv.OPCItemNameTextBoxes[57] = dic["bAdditiveNeg_Zco"];
            dv.OPCItemNameTextBoxes[58] = dic["bAdditivePos_Zco"];
            dv.OPCItemNameTextBoxes[59] = dic["bCyclicPos_Zco"];
            dv.OPCItemNameTextBoxes[60] = dic["bHome_Zco"];
            dv.OPCItemNameTextBoxes[61] = dic["bHomeCalibration_Zco"];
            dv.OPCItemNameTextBoxes[62] = dic["bJogNeg_Zco"];
            dv.OPCItemNameTextBoxes[63] = dic["bJogPos_Zco"];

            //[gIntp.Input.Axes[2].Param]
            dv.OPCItemNameTextBoxes[64] = dic["rDistance_Zpa"];
            dv.OPCItemNameTextBoxes[65] = dic["nHomeMode_Zpa"];
            dv.OPCItemNameTextBoxes[66] = dic["nHomeOrder_Zpa"];
            dv.OPCItemNameTextBoxes[67] = dic["nHomeType_Zpa"];
            dv.OPCItemNameTextBoxes[68] = dic["rHomeVelocity_Zpa"];
            dv.OPCItemNameTextBoxes[69] = dic["rHomeAcceleration_Zpa"];
            dv.OPCItemNameTextBoxes[70] = dic["rHomeDeceleration_Zpa"];
            dv.OPCItemNameTextBoxes[71] = dic["rHomeDistance_Zpa"];
            dv.OPCItemNameTextBoxes[72] = dic["rHomePosition_Zpa"];
            dv.OPCItemNameTextBoxes[73] = dic["nHomeSensorPosType_Zpa"];
            dv.OPCItemNameTextBoxes[74] = dic["rPosition_Zpa"];

            ////[gIntp.Input.Axes[3].Command]
            dv.OPCItemNameTextBoxes[75] = dic["bAbsolute_Sco"];
            dv.OPCItemNameTextBoxes[76] = dic["bAdditiveNeg_Sco"];
            dv.OPCItemNameTextBoxes[77] = dic["bAdditivePos_Sco"];
            dv.OPCItemNameTextBoxes[78] = dic["bCyclicPos_Sco"];
            dv.OPCItemNameTextBoxes[79] = dic["bHome_Sco"];
            dv.OPCItemNameTextBoxes[80] = dic["bHomeCalibration_Sco"];
            dv.OPCItemNameTextBoxes[81] = dic["bJogNeg_Sco"];
            dv.OPCItemNameTextBoxes[82] = dic["bJogPos_Sco"];

            //[gIntp.Input.Axes[3].Param]
            dv.OPCItemNameTextBoxes[83] = dic["rDistance_Spa"];
            dv.OPCItemNameTextBoxes[84] = dic["nHomeMode_Spa"];
            dv.OPCItemNameTextBoxes[85] = dic["nHomeOrder_Spa"];
            dv.OPCItemNameTextBoxes[86] = dic["nHomeType_Spa"];
            dv.OPCItemNameTextBoxes[87] = dic["rHomeVelocity_Spa"];
            dv.OPCItemNameTextBoxes[88] = dic["rHomeAcceleration_Spa"];
            dv.OPCItemNameTextBoxes[89] = dic["rHomeDeceleration_Spa"];
            dv.OPCItemNameTextBoxes[90] = dic["rHomeDistance_Spa"];
            dv.OPCItemNameTextBoxes[91] = dic["rHomePosition_Spa"];
            dv.OPCItemNameTextBoxes[92] = dic["nHomeSensorPosType_Spa"];
            dv.OPCItemNameTextBoxes[93] = dic["rPosition_Spa"];

            //[gIntp.Input.Alarm]
            dv.OPCItemNameTextBoxes[94] = dic["bAck"];
            dv.OPCItemNameTextBoxes[95] = dic["bAckAll"];

            //[gIntp.Input.Module.Digital]
            dv.OPCItemNameTextBoxes[96] = dic["DigitalMode"];

            //[gIntp.Input.Module.Digital.X0]
            dv.OPCItemNameTextBoxes[97] = dic["bEnableForce_X0"];
            dv.OPCItemNameTextBoxes[98] = dic["bForceValue[0]_X0"];
            dv.OPCItemNameTextBoxes[99] = dic["bForceValue[1]_X0"];
            dv.OPCItemNameTextBoxes[100] = dic["bForceValue[2]_X0"];
            dv.OPCItemNameTextBoxes[101] = dic["bForceValue[3]_X0"];
            dv.OPCItemNameTextBoxes[102] = dic["bForceValue[4]_X0"];
            dv.OPCItemNameTextBoxes[103] = dic["bForceValue[5]_X0"];
            dv.OPCItemNameTextBoxes[104] = dic["bForceValue[6]_X0"];
            dv.OPCItemNameTextBoxes[105] = dic["bForceValue[7]_X0"];
            dv.OPCItemNameTextBoxes[106] = dic["bForceValue[8]_X0"];
            dv.OPCItemNameTextBoxes[107] = dic["bForceValue[9]_X0"];
            dv.OPCItemNameTextBoxes[108] = dic["bForceValue[10]_X0"];
            dv.OPCItemNameTextBoxes[109] = dic["bForceValue[11]_X0"];
            dv.OPCItemNameTextBoxes[110] = dic["nBitForceValue_X0"];

            //[gIntp.Input.Module.Digital.X1]
            dv.OPCItemNameTextBoxes[111] = dic["bEnableForce_X1"];
            dv.OPCItemNameTextBoxes[112] = dic["bForceValue[0]_X1"];
            dv.OPCItemNameTextBoxes[113] = dic["bForceValue[1]_X1"];
            dv.OPCItemNameTextBoxes[114] = dic["bForceValue[2]_X1"];
            dv.OPCItemNameTextBoxes[115] = dic["bForceValue[3]_X1"];
            dv.OPCItemNameTextBoxes[116] = dic["bForceValue[4]_X1"];
            dv.OPCItemNameTextBoxes[117] = dic["bForceValue[5]_X1"];
            dv.OPCItemNameTextBoxes[118] = dic["bForceValue[6]_X1"];
            dv.OPCItemNameTextBoxes[119] = dic["bForceValue[7]_X1"];
            dv.OPCItemNameTextBoxes[120] = dic["bForceValue[8]_X1"];
            dv.OPCItemNameTextBoxes[121] = dic["bForceValue[9]_X1"];
            dv.OPCItemNameTextBoxes[122] = dic["bForceValue[10]_X1"];
            dv.OPCItemNameTextBoxes[123] = dic["bForceValue[11]_X1"];
            dv.OPCItemNameTextBoxes[124] = dic["nBitForceValue_X1"];

            //[gIntp.Input.Module.Digital.X2]
            dv.OPCItemNameTextBoxes[125] = dic["bEnableForce_X2"];
            dv.OPCItemNameTextBoxes[126] = dic["bForceValue[0]_X2"];
            dv.OPCItemNameTextBoxes[127] = dic["bForceValue[1]_X2"];
            dv.OPCItemNameTextBoxes[128] = dic["bForceValue[2]_X2"];
            dv.OPCItemNameTextBoxes[129] = dic["bForceValue[3]_X2"];
            dv.OPCItemNameTextBoxes[130] = dic["bForceValue[4]_X2"];
            dv.OPCItemNameTextBoxes[131] = dic["bForceValue[5]_X2"];
            dv.OPCItemNameTextBoxes[132] = dic["bForceValue[6]_X2"];
            dv.OPCItemNameTextBoxes[133] = dic["bForceValue[7]_X2"];
            dv.OPCItemNameTextBoxes[134] = dic["bForceValue[8]_X2"];
            dv.OPCItemNameTextBoxes[135] = dic["bForceValue[9]_X2"];
            dv.OPCItemNameTextBoxes[136] = dic["bForceValue[10]_X2"];
            dv.OPCItemNameTextBoxes[137] = dic["bForceValue[11]_X2"];
            dv.OPCItemNameTextBoxes[138] = dic["nBitForceValue_X2"];

            //[gIntp.Input.Module.Analog.XA0]
            dv.OPCItemNameTextBoxes[139] = dic["bEnableForce_XA0"];
            dv.OPCItemNameTextBoxes[140] = dic["nForceValue[0]_XA0"];
            dv.OPCItemNameTextBoxes[141] = dic["nForceValue[1]_XA0"];
            dv.OPCItemNameTextBoxes[142] = dic["nForceValue[2]_XA0"];
            dv.OPCItemNameTextBoxes[143] = dic["nForceValue[3]_XA0"];

            //[gIntp.Input.Module.Analog.XA1]
            dv.OPCItemNameTextBoxes[144] = dic["bEnableForce_XA1"];
            dv.OPCItemNameTextBoxes[145] = dic["nForceValue[0]_XA1"];
            dv.OPCItemNameTextBoxes[146] = dic["nForceValue[1]_XA1"];
            dv.OPCItemNameTextBoxes[147] = dic["nForceValue[2]_XA1"];
            dv.OPCItemNameTextBoxes[148] = dic["nForceValue[3]_XA1"];

            //[gIntp.Input.Module.Analog.XA2]
            dv.OPCItemNameTextBoxes[149] = dic["bEnableForce_XA2"];
            dv.OPCItemNameTextBoxes[150] = dic["nForceValue[0]_XA2"];
            dv.OPCItemNameTextBoxes[151] = dic["nForceValue[1]_XA2"];
            dv.OPCItemNameTextBoxes[152] = dic["nForceValue[2]_XA2"];
            dv.OPCItemNameTextBoxes[153] = dic["nForceValue[3]_XA2"];

            //[gIntp.Input.Module.Analog.XA3]
            dv.OPCItemNameTextBoxes[154] = dic["bEnableForce_XA3"];
            dv.OPCItemNameTextBoxes[155] = dic["nForceValue[0]_XA3"];
            dv.OPCItemNameTextBoxes[156] = dic["nForceValue[1]_XA3"];
            dv.OPCItemNameTextBoxes[157] = dic["nForceValue[2]_XA3"];
            dv.OPCItemNameTextBoxes[158] = dic["nForceValue[3]_XA3"];

            //[gIntp.Input.Module.Analog.XA4]
            dv.OPCItemNameTextBoxes[159] = dic["bEnableForce_XA4"];
            dv.OPCItemNameTextBoxes[160] = dic["nForceValue[0]_XA4"];
            dv.OPCItemNameTextBoxes[161] = dic["nForceValue[1]_XA4"];
            dv.OPCItemNameTextBoxes[162] = dic["nForceValue[2]_XA4"];
            dv.OPCItemNameTextBoxes[163] = dic["nForceValue[3]_XA4"];

            //[gIntp.Input.Module.Analog.XA5]
            dv.OPCItemNameTextBoxes[164] = dic["bEnableForce_XA5"];
            dv.OPCItemNameTextBoxes[165] = dic["nForceValue[0]_XA5"];
            dv.OPCItemNameTextBoxes[166] = dic["nForceValue[1]_XA5"];
            dv.OPCItemNameTextBoxes[167] = dic["nForceValue[2]_XA5"];
            dv.OPCItemNameTextBoxes[168] = dic["nForceValue[3]_XA5"];

            //[gIntp.Input.Module.Analog.XA6]
            dv.OPCItemNameTextBoxes[169] = dic["bEnableForce_XA6"];
            dv.OPCItemNameTextBoxes[170] = dic["nForceValue[0]_XA6"];
            dv.OPCItemNameTextBoxes[171] = dic["nForceValue[1]_XA6"];
            dv.OPCItemNameTextBoxes[172] = dic["nForceValue[2]_XA6"];
            dv.OPCItemNameTextBoxes[173] = dic["nForceValue[3]_XA6"];

            //[gIntp.Input.Module.Analog.XA7]
            dv.OPCItemNameTextBoxes[174] = dic["bEnableForce_XA7"];
            dv.OPCItemNameTextBoxes[175] = dic["nForceValue[0]_XA7"];
            dv.OPCItemNameTextBoxes[176] = dic["nForceValue[1]_XA7"];
            dv.OPCItemNameTextBoxes[177] = dic["nForceValue[2]_XA7"];
            dv.OPCItemNameTextBoxes[178] = dic["nForceValue[3]_XA7"];

            //[gIntp.Input.Module.Analog.XA8]
            dv.OPCItemNameTextBoxes[179] = dic["bEnableForce_XA8"];
            dv.OPCItemNameTextBoxes[180] = dic["nForceValue[0]_XA8"];
            dv.OPCItemNameTextBoxes[181] = dic["nForceValue[1]_XA8"];
            dv.OPCItemNameTextBoxes[182] = dic["nForceValue[2]_XA8"];
            dv.OPCItemNameTextBoxes[183] = dic["nForceValue[3]_XA8"];

            //[gIntp.Input.Module.Analog.XA9]
            dv.OPCItemNameTextBoxes[184] = dic["bEnableForce_XA9"];
            dv.OPCItemNameTextBoxes[185] = dic["nForceValue[0]_XA9"];
            dv.OPCItemNameTextBoxes[186] = dic["nForceValue[1]_XA9"];
            dv.OPCItemNameTextBoxes[187] = dic["nForceValue[2]_XA9"];
            dv.OPCItemNameTextBoxes[188] = dic["nForceValue[3]_XA9"];

            //[gIntp.Input.Module.Analog.XA10]
            dv.OPCItemNameTextBoxes[189] = dic["bEnableForce_XA10"];
            dv.OPCItemNameTextBoxes[190] = dic["nForceValue[0]_XA10"];
            dv.OPCItemNameTextBoxes[191] = dic["nForceValue[1]_XA10"];
            dv.OPCItemNameTextBoxes[192] = dic["nForceValue[2]_XA10"];
            dv.OPCItemNameTextBoxes[193] = dic["nForceValue[3]_XA10"];

            //[gIntp.Input.Module.Analog.XA11]
            dv.OPCItemNameTextBoxes[194] = dic["bEnableForce_XA11"];
            dv.OPCItemNameTextBoxes[195] = dic["nForceValue[0]_XA11"];
            dv.OPCItemNameTextBoxes[196] = dic["nForceValue[1]_XA11"];
            dv.OPCItemNameTextBoxes[197] = dic["nForceValue[2]_XA11"];
            dv.OPCItemNameTextBoxes[198] = dic["nForceValue[3]_XA11"];

            //[gIntp.Output.Alarm.Monitor.Information]
            dv.OPCItemNameTextBoxes[199] = dic["bResp_NO"];
            dv.OPCItemNameTextBoxes[200] = dic["bResp_OK"];
            dv.OPCItemNameTextBoxes[201] = dic["bResp_YES"];
            dv.OPCItemNameTextBoxes[202] = dic["Info_nId"];

            //[gIntp.Output.Alarm.Monitor.Machine]
            dv.OPCItemNameTextBoxes[203] = dic["Machine_nAxisId"];
            dv.OPCItemNameTextBoxes[204] = dic["Machine_nCurPosAlarm"];
            dv.OPCItemNameTextBoxes[205] = dic["Machine_nCurPosGroup"];
            dv.OPCItemNameTextBoxes[206] = dic["Machine_nId"];

            //[gIntp.Output.Alarm.Monitor.System]
            dv.OPCItemNameTextBoxes[207] = dic["System_bInError"];
            dv.OPCItemNameTextBoxes[208] = dic["System_dNumber"];
            dv.OPCItemNameTextBoxes[209] = dic["System_nCount"];
            dv.OPCItemNameTextBoxes[210] = dic["System_strLine1"];
            dv.OPCItemNameTextBoxes[211] = dic["System_strLine2"];
            dv.OPCItemNameTextBoxes[212] = dic["System_strLine3"];
            dv.OPCItemNameTextBoxes[213] = dic["System_strLine4"];
            dv.OPCItemNameTextBoxes[214] = dic["System_nAxisId"];

            //[gIntp.Output.State]
            dv.OPCItemNameTextBoxes[215] = dic["bAutoMode"];
            dv.OPCItemNameTextBoxes[216] = dic["bBoot"];
            dv.OPCItemNameTextBoxes[217] = dic["bDisabled"];
            dv.OPCItemNameTextBoxes[218] = dic["bErrorStop"];
            dv.OPCItemNameTextBoxes[219] = dic["bHomeMode"];
            dv.OPCItemNameTextBoxes[220] = dic["bManualMode"];
            dv.OPCItemNameTextBoxes[221] = dic["bModeChange"];
            dv.OPCItemNameTextBoxes[222] = dic["Enumeration"];

            //[gIntp.Output.Machine.State]
            dv.OPCItemNameTextBoxes[412] = dic["bDisable_se"];
            dv.OPCItemNameTextBoxes[413] = dic["bReady_se"];
            dv.OPCItemNameTextBoxes[414] = dic["bOperation_se"];
            dv.OPCItemNameTextBoxes[415] = dic["bStop_se"];
            dv.OPCItemNameTextBoxes[416] = dic["bPause_se"];
            dv.OPCItemNameTextBoxes[417] = dic["bError_se"];
            dv.OPCItemNameTextBoxes[418] = dic["Enumeration_se"];
            dv.OPCItemNameTextBoxes[419] = dic["SubEnumeration_se"];

            //[gIntp.Output.Machine.Monitor]
            dv.OPCItemNameTextBoxes[420] = dic["rPlateFormReleasePressure"];
            dv.OPCItemNameTextBoxes[421] = dic["rVacuumGenerator1Pressure"];
            dv.OPCItemNameTextBoxes[422] = dic["rVacuumGenerator2Pressure"];
            dv.OPCItemNameTextBoxes[423] = dic["rHoldClampPressure"];
            dv.OPCItemNameTextBoxes[424] = dic["rGasSupplyLine1Pressure"];
            dv.OPCItemNameTextBoxes[425] = dic["rFilterMainPressureSensor"];
            dv.OPCItemNameTextBoxes[426] = dic["rFilterSub1PressureSensor"];
            dv.OPCItemNameTextBoxes[427] = dic["rFilterSub2PressureSensor"];
            dv.OPCItemNameTextBoxes[428] = dic["rGasSupplyLine2Pressure"];
            dv.OPCItemNameTextBoxes[429] = dic["rPumpMainPressureSensor"];
            dv.OPCItemNameTextBoxes[430] = dic["rPumpSubPressureSensor"];
            dv.OPCItemNameTextBoxes[431] = dic["rMainAirLine1Pressure"];
            dv.OPCItemNameTextBoxes[432] = dic["rMainAirLine2Pressure"];
            dv.OPCItemNameTextBoxes[433] = dic["rVacuumGenerator1Flow"];
            dv.OPCItemNameTextBoxes[434] = dic["rVacuumGenerator2Flow"];
            dv.OPCItemNameTextBoxes[435] = dic["rHoldClampFlow"];
            dv.OPCItemNameTextBoxes[436] = dic["rGasSupplyLine1Flow"];
            dv.OPCItemNameTextBoxes[437] = dic["rGasSupplyLine2Flow"];
            dv.OPCItemNameTextBoxes[438] = dic["rPumpMainWindSpeedSensor"];
            dv.OPCItemNameTextBoxes[439] = dic["rPumpSubWindSpeedSensor"];
            dv.OPCItemNameTextBoxes[440] = dic["rChamberOxygenSensor"];
            dv.OPCItemNameTextBoxes[441] = dic["rPumpMainOxygenSensor"];
            dv.OPCItemNameTextBoxes[442] = dic["rPumpSubOxygenSensor"];
            dv.OPCItemNameTextBoxes[443] = dic["rPumpMainDewPointSensor"];
            dv.OPCItemNameTextBoxes[444] = dic["rPumpSubDewPointSensor"];
            dv.OPCItemNameTextBoxes[445] = dic["rPumpMainTempSensor"];
            dv.OPCItemNameTextBoxes[446] = dic["rPumpSubTempSensor"];
            dv.OPCItemNameTextBoxes[447] = dic["rPumpMainMotorCurrent"];
            dv.OPCItemNameTextBoxes[448] = dic["rPumpMainRelativePressure"];
            dv.OPCItemNameTextBoxes[449] = dic["rPumpSubMotorCurrent"];
            dv.OPCItemNameTextBoxes[450] = dic["rPumpSubRelativePressure"];

            //[gIntp.Output.Status]
            dv.OPCItemNameTextBoxes[223] = dic["bAxesDisabled"];
            dv.OPCItemNameTextBoxes[224] = dic["bAxesMoving"];
            dv.OPCItemNameTextBoxes[225] = dic["bAxesStandstill"];
            dv.OPCItemNameTextBoxes[226] = dic["nLifeCntPc"];
            dv.OPCItemNameTextBoxes[227] = dic["nLifeCntPlc"];

            //[gIntp.Output.Axes[0].Error]
            dv.OPCItemNameTextBoxes[228] = dic["bInError_X"];
            dv.OPCItemNameTextBoxes[229] = dic["dNumber_X"];
            dv.OPCItemNameTextBoxes[230] = dic["nCount_X"];
            dv.OPCItemNameTextBoxes[231] = dic["strLine1_X"];
            dv.OPCItemNameTextBoxes[232] = dic["strLine2_X"];
            dv.OPCItemNameTextBoxes[233] = dic["strLine3_X"];
            dv.OPCItemNameTextBoxes[234] = dic["strLine4_X"];

            //[gIntp.Output.Axes[1].Error]
            dv.OPCItemNameTextBoxes[235] = dic["bInError_R"];
            dv.OPCItemNameTextBoxes[236] = dic["dNumber_R"];
            dv.OPCItemNameTextBoxes[237] = dic["nCount_R"];
            dv.OPCItemNameTextBoxes[238] = dic["strLine1_R"];
            dv.OPCItemNameTextBoxes[239] = dic["strLine2_R"];
            dv.OPCItemNameTextBoxes[240] = dic["strLine3_R"];
            dv.OPCItemNameTextBoxes[241] = dic["strLine4_R"];

            //[gIntp.Output.Axes[2].Error]
            dv.OPCItemNameTextBoxes[242] = dic["bInError_Z"];
            dv.OPCItemNameTextBoxes[243] = dic["dNumber_Z"];
            dv.OPCItemNameTextBoxes[244] = dic["nCount_Z"];
            dv.OPCItemNameTextBoxes[245] = dic["strLine1_Z"];
            dv.OPCItemNameTextBoxes[246] = dic["strLine2_Z"];
            dv.OPCItemNameTextBoxes[247] = dic["strLine3_Z"];
            dv.OPCItemNameTextBoxes[248] = dic["strLine4_Z"];

            //[gIntp.Output.Axes[3].Error]
            dv.OPCItemNameTextBoxes[249] = dic["bInError_S"];
            dv.OPCItemNameTextBoxes[250] = dic["dNumber_S"];
            dv.OPCItemNameTextBoxes[251] = dic["nCount_S"];
            dv.OPCItemNameTextBoxes[252] = dic["strLine1_S"];
            dv.OPCItemNameTextBoxes[253] = dic["strLine2_S"];
            dv.OPCItemNameTextBoxes[254] = dic["strLine3_S"];
            dv.OPCItemNameTextBoxes[255] = dic["strLine4_S"];

            //[gIntp.Output.Axes[0].Monitor]
            dv.OPCItemNameTextBoxes[256] = dic["dPosition_nc_Xmo"];
            dv.OPCItemNameTextBoxes[257] = dic["rMaxAcceleration_Xmo"];
            dv.OPCItemNameTextBoxes[258] = dic["rMaxDeceleration_Xmo"];
            dv.OPCItemNameTextBoxes[259] = dic["rMaxVelocity_Xmo"];
            dv.OPCItemNameTextBoxes[260] = dic["rPosition_Xmo"];
            dv.OPCItemNameTextBoxes[261] = dic["rSoftLimitNeg_Xmo"];
            dv.OPCItemNameTextBoxes[262] = dic["rSoftLimitPos_Xmo"];
            dv.OPCItemNameTextBoxes[263] = dic["rTorque_Xmo"];
            dv.OPCItemNameTextBoxes[264] = dic["rVelocity_Xmo"];
            dv.OPCItemNameTextBoxes[265] = dic["rVelocity_nc_Xmo"];
            dv.OPCItemNameTextBoxes[451] = dic["rTemperature_Xmo"];
            dv.OPCItemNameTextBoxes[452] = dic["rCurrent_Xmo"];

            //[gIntp.Output.Axes[1].Monitor]
            dv.OPCItemNameTextBoxes[266] = dic["dPosition_nc_Rmo"];
            dv.OPCItemNameTextBoxes[267] = dic["rMaxAcceleration_Rmo"];
            dv.OPCItemNameTextBoxes[268] = dic["rMaxDeceleration_Rmo"];
            dv.OPCItemNameTextBoxes[269] = dic["rMaxVelocity_Rmo"];
            dv.OPCItemNameTextBoxes[270] = dic["rPosition_Rmo"];
            dv.OPCItemNameTextBoxes[271] = dic["rSoftLimitNeg_Rmo"];
            dv.OPCItemNameTextBoxes[272] = dic["rSoftLimitPos_Rmo"];
            dv.OPCItemNameTextBoxes[273] = dic["rTorque_Rmo"];
            dv.OPCItemNameTextBoxes[274] = dic["rVelocity_Rmo"];
            dv.OPCItemNameTextBoxes[275] = dic["rVelocity_nc_Rmo"];
            dv.OPCItemNameTextBoxes[453] = dic["rTemperature_Rmo"];
            dv.OPCItemNameTextBoxes[454] = dic["rCurrent_Rmo"];

            //[gIntp.Output.Axes[2].Monitor]
            dv.OPCItemNameTextBoxes[276] = dic["dPosition_nc_Ymo"];
            dv.OPCItemNameTextBoxes[277] = dic["rMaxAcceleration_Ymo"];
            dv.OPCItemNameTextBoxes[278] = dic["rMaxDeceleration_Ymo"];
            dv.OPCItemNameTextBoxes[279] = dic["rMaxVelocity_Ymo"];
            dv.OPCItemNameTextBoxes[280] = dic["rPosition_Ymo"];
            dv.OPCItemNameTextBoxes[281] = dic["rSoftLimitNeg_Ymo"];
            dv.OPCItemNameTextBoxes[282] = dic["rSoftLimitPos_Ymo"];
            dv.OPCItemNameTextBoxes[283] = dic["rTorque_Ymo"];
            dv.OPCItemNameTextBoxes[284] = dic["rVelocity_Ymo"];
            dv.OPCItemNameTextBoxes[285] = dic["rVelocity_nc_Ymo"];
            dv.OPCItemNameTextBoxes[455] = dic["rTemperature_Ymo"];
            dv.OPCItemNameTextBoxes[456] = dic["rCurrent_Ymo"];

            //[gIntp.Output.Axes[3].Monitor]
            dv.OPCItemNameTextBoxes[286] = dic["dPosition_nc_Smo"];
            dv.OPCItemNameTextBoxes[287] = dic["rMaxAcceleration_Smo"];
            dv.OPCItemNameTextBoxes[288] = dic["rMaxDeceleration_Smo"];
            dv.OPCItemNameTextBoxes[289] = dic["rMaxVelocity_Smo"];
            dv.OPCItemNameTextBoxes[290] = dic["rPosition_Smo"];
            dv.OPCItemNameTextBoxes[291] = dic["rSoftLimitNeg_Smo"];
            dv.OPCItemNameTextBoxes[292] = dic["rSoftLimitPos_Smo"];
            dv.OPCItemNameTextBoxes[293] = dic["rTorque_Smo"];
            dv.OPCItemNameTextBoxes[294] = dic["rVelocity_Smo"];
            dv.OPCItemNameTextBoxes[295] = dic["rVelocity_nc_Smo"];
            dv.OPCItemNameTextBoxes[457] = dic["rTemperature_Smo"];
            dv.OPCItemNameTextBoxes[458] = dic["rCurrent_Smo"];

            //[gIntp.Output.Module.Digital.Y0]
            dv.OPCItemNameTextBoxes[296] = dic["bEnableForce_Y0"];
            dv.OPCItemNameTextBoxes[297] = dic["bForceValue[0]_Y0"];
            dv.OPCItemNameTextBoxes[298] = dic["bForceValue[1]_Y0"];
            dv.OPCItemNameTextBoxes[299] = dic["bForceValue[2]_Y0"];
            dv.OPCItemNameTextBoxes[300] = dic["bForceValue[3]_Y0"];
            dv.OPCItemNameTextBoxes[301] = dic["bForceValue[4]_Y0"];
            dv.OPCItemNameTextBoxes[302] = dic["bForceValue[5]_Y0"];
            dv.OPCItemNameTextBoxes[303] = dic["bForceValue[6]_Y0"];
            dv.OPCItemNameTextBoxes[304] = dic["bForceValue[7]_Y0"];
            dv.OPCItemNameTextBoxes[305] = dic["bForceValue[8]_Y0"];
            dv.OPCItemNameTextBoxes[306] = dic["bForceValue[9]_Y0"];
            dv.OPCItemNameTextBoxes[307] = dic["bForceValue[10]_Y0"];
            dv.OPCItemNameTextBoxes[308] = dic["bForceValue[11]_Y0"];
            dv.OPCItemNameTextBoxes[309] = dic["nBitForceValue_Y0"];

            //[gIntp.Output.Module.Digital.Y1]
            dv.OPCItemNameTextBoxes[310] = dic["bEnableForce_Y1"];
            dv.OPCItemNameTextBoxes[311] = dic["bForceValue[0]_Y1"];
            dv.OPCItemNameTextBoxes[312] = dic["bForceValue[1]_Y1"];
            dv.OPCItemNameTextBoxes[313] = dic["bForceValue[2]_Y1"];
            dv.OPCItemNameTextBoxes[314] = dic["bForceValue[3]_Y1"];
            dv.OPCItemNameTextBoxes[315] = dic["bForceValue[4]_Y1"];
            dv.OPCItemNameTextBoxes[316] = dic["bForceValue[5]_Y1"];
            dv.OPCItemNameTextBoxes[317] = dic["bForceValue[6]_Y1"];
            dv.OPCItemNameTextBoxes[318] = dic["bForceValue[7]_Y1"];
            dv.OPCItemNameTextBoxes[319] = dic["bForceValue[8]_Y1"];
            dv.OPCItemNameTextBoxes[320] = dic["bForceValue[9]_Y1"];
            dv.OPCItemNameTextBoxes[321] = dic["bForceValue[10]_Y1"];
            dv.OPCItemNameTextBoxes[322] = dic["bForceValue[11]_Y1"];
            dv.OPCItemNameTextBoxes[323] = dic["nBitForceValue_Y1"];

            //[gIntp.Output.Module.Digital.Y2]
            dv.OPCItemNameTextBoxes[324] = dic["bEnableForce_Y2"];
            dv.OPCItemNameTextBoxes[325] = dic["bForceValue[0]_Y2"];
            dv.OPCItemNameTextBoxes[326] = dic["bForceValue[1]_Y2"];
            dv.OPCItemNameTextBoxes[327] = dic["bForceValue[2]_Y2"];
            dv.OPCItemNameTextBoxes[328] = dic["bForceValue[3]_Y2"];
            dv.OPCItemNameTextBoxes[329] = dic["bForceValue[4]_Y2"];
            dv.OPCItemNameTextBoxes[330] = dic["bForceValue[5]_Y2"];
            dv.OPCItemNameTextBoxes[331] = dic["bForceValue[6]_Y2"];
            dv.OPCItemNameTextBoxes[332] = dic["bForceValue[7]_Y2"];
            dv.OPCItemNameTextBoxes[333] = dic["bForceValue[8]_Y2"];
            dv.OPCItemNameTextBoxes[334] = dic["bForceValue[9]_Y2"];
            dv.OPCItemNameTextBoxes[335] = dic["bForceValue[10]_Y2"];
            dv.OPCItemNameTextBoxes[336] = dic["bForceValue[11]_Y2"];
            dv.OPCItemNameTextBoxes[337] = dic["nBitForceValue_Y2"];

            //[gIntp.Output.Module.Digital.Y3]
            dv.OPCItemNameTextBoxes[338] = dic["bEnableForce_Y3"];
            dv.OPCItemNameTextBoxes[339] = dic["bForceValue[0]_Y3"];
            dv.OPCItemNameTextBoxes[340] = dic["bForceValue[1]_Y3"];
            dv.OPCItemNameTextBoxes[341] = dic["bForceValue[2]_Y3"];
            dv.OPCItemNameTextBoxes[342] = dic["bForceValue[3]_Y3"];
            dv.OPCItemNameTextBoxes[343] = dic["bForceValue[4]_Y3"];
            dv.OPCItemNameTextBoxes[344] = dic["bForceValue[5]_Y3"];
            dv.OPCItemNameTextBoxes[345] = dic["bForceValue[6]_Y3"];
            dv.OPCItemNameTextBoxes[346] = dic["bForceValue[7]_Y3"];
            dv.OPCItemNameTextBoxes[347] = dic["bForceValue[8]_Y3"];
            dv.OPCItemNameTextBoxes[348] = dic["bForceValue[9]_Y3"];
            dv.OPCItemNameTextBoxes[349] = dic["bForceValue[10]_Y3"];
            dv.OPCItemNameTextBoxes[350] = dic["bForceValue[11]_Y3"];
            dv.OPCItemNameTextBoxes[351] = dic["nBitForceValue_Y3"];

            //[gIntp.Output.Module.Digital.Y4]
            dv.OPCItemNameTextBoxes[352] = dic["bEnableForce_Y4"];
            dv.OPCItemNameTextBoxes[353] = dic["bForceValue[0]_Y4"];
            dv.OPCItemNameTextBoxes[354] = dic["bForceValue[1]_Y4"];
            dv.OPCItemNameTextBoxes[355] = dic["bForceValue[2]_Y4"];
            dv.OPCItemNameTextBoxes[356] = dic["bForceValue[3]_Y4"];
            dv.OPCItemNameTextBoxes[357] = dic["bForceValue[4]_Y4"];
            dv.OPCItemNameTextBoxes[358] = dic["bForceValue[5]_Y4"];
            dv.OPCItemNameTextBoxes[359] = dic["bForceValue[6]_Y4"];
            dv.OPCItemNameTextBoxes[360] = dic["bForceValue[7]_Y4"];
            dv.OPCItemNameTextBoxes[361] = dic["bForceValue[8]_Y4"];
            dv.OPCItemNameTextBoxes[362] = dic["bForceValue[9]_Y4"];
            dv.OPCItemNameTextBoxes[363] = dic["bForceValue[10]_Y4"];
            dv.OPCItemNameTextBoxes[364] = dic["bForceValue[11]_Y4"];
            dv.OPCItemNameTextBoxes[365] = dic["nBitForceValue_Y4"];

            //[gIntp.Output.Module.Digital.Y5]
            dv.OPCItemNameTextBoxes[366] = dic["bEnableForce_Y5"];
            dv.OPCItemNameTextBoxes[367] = dic["bForceValue[0]_Y5"];
            dv.OPCItemNameTextBoxes[368] = dic["bForceValue[1]_Y5"];
            dv.OPCItemNameTextBoxes[369] = dic["bForceValue[2]_Y5"];
            dv.OPCItemNameTextBoxes[370] = dic["bForceValue[3]_Y5"];
            dv.OPCItemNameTextBoxes[371] = dic["bForceValue[4]_Y5"];
            dv.OPCItemNameTextBoxes[372] = dic["bForceValue[5]_Y5"];
            dv.OPCItemNameTextBoxes[373] = dic["bForceValue[6]_Y5"];
            dv.OPCItemNameTextBoxes[374] = dic["bForceValue[7]_Y5"];
            dv.OPCItemNameTextBoxes[375] = dic["bForceValue[8]_Y5"];
            dv.OPCItemNameTextBoxes[376] = dic["bForceValue[9]_Y5"];
            dv.OPCItemNameTextBoxes[377] = dic["bForceValue[10]_Y5"];
            dv.OPCItemNameTextBoxes[378] = dic["bForceValue[11]_Y5"];
            dv.OPCItemNameTextBoxes[379] = dic["nBitForceValue_Y5"];

            //[gxGrpPumpMan[0].Input.Command]
            dv.OPCItemNameTextBoxes[380] = dic["bStop_pump1"];
            dv.OPCItemNameTextBoxes[381] = dic["bTuning_pump1"];
            dv.OPCItemNameTextBoxes[382] = dic["bControl_pump1"];

            //[gxGrpPumpMan[1].Input.Command]
            dv.OPCItemNameTextBoxes[460] = dic["bStop_pump2"];
            dv.OPCItemNameTextBoxes[461] = dic["bTuning_pump2"];
            dv.OPCItemNameTextBoxes[462] = dic["bControl_pump2"];

            //[gxGrpPumpMan[0].Input.Parameter]
            dv.OPCItemNameTextBoxes[383] = dic["rSetValue_pump1"];

            //[gxGrpPumpMan[1].Input.Parameter]
            dv.OPCItemNameTextBoxes[463] = dic["rSetValue_pump2"];

            //[gxGrpPumpMan[0].Output.Status]
            dv.OPCItemNameTextBoxes[384] = dic["bRampOn_pump1"];
            dv.OPCItemNameTextBoxes[385] = dic["bFilterOn_pump1"];
            dv.OPCItemNameTextBoxes[386] = dic["bFilterParamOk_pump1"];
            dv.OPCItemNameTextBoxes[387] = dic["bRampParamOk_pump1"];
            dv.OPCItemNameTextBoxes[388] = dic["bPidParamOk_pump1"];
            dv.OPCItemNameTextBoxes[389] = dic["dErrorNumber_pump1"];

            //[gxGrpPumpMan[1].Output.Status]
            dv.OPCItemNameTextBoxes[464] = dic["bRampOn_pump2"];
            dv.OPCItemNameTextBoxes[465] = dic["bFilterOn_pump2"];
            dv.OPCItemNameTextBoxes[466] = dic["bFilterParamOk_pump2"];
            dv.OPCItemNameTextBoxes[467] = dic["bRampParamOk_pump2"];
            dv.OPCItemNameTextBoxes[468] = dic["bPidParamOk_pump2"];
            dv.OPCItemNameTextBoxes[469] = dic["dErrorNumber_pump2"];

            //[gxGrpPumpMan[0].Output.State]
            dv.OPCItemNameTextBoxes[482] = dic["bReady_pump1_se"];
            dv.OPCItemNameTextBoxes[483] = dic["bControl_pump1_se"];

            //[gxGrpPumpMan[1].Output.State]
            dv.OPCItemNameTextBoxes[484] = dic["bReady_pump2_se"];
            dv.OPCItemNameTextBoxes[485] = dic["bControl_pump2_se"];

            //[gxGrpPumpMan[0].Output.Monitor]
            dv.OPCItemNameTextBoxes[390] = dic["rActValue_pump1"];

            //[gxGrpPumpMan[1].Output.Monitor]
            dv.OPCItemNameTextBoxes[470] = dic["rActValue_pump2"];

            //[gxGrpChamberMan.Input.Command]
            dv.OPCItemNameTextBoxes[391] = dic["bValveOn_Chamber"];
            dv.OPCItemNameTextBoxes[392] = dic["bValveOff_Chamber"];
            dv.OPCItemNameTextBoxes[476] = dic["bLed1On_Chamber"];
            dv.OPCItemNameTextBoxes[477] = dic["bLed2On_Chamber"];

            //[gxGrpChamberMan.Input.Configuration]
            dv.OPCItemNameTextBoxes[393] = dic["rMaxOxygenConcentration_Chamber"];
            dv.OPCItemNameTextBoxes[394] = dic["rMinOxygenConcentration_Chamber"];
            dv.OPCItemNameTextBoxes[395] = dic["rMaxAirPressure_Chamber"];
            dv.OPCItemNameTextBoxes[396] = dic["rMaxGasFlow_Chamber"];
            dv.OPCItemNameTextBoxes[397] = dic["stGasType_Chamber"];

            //[gxGrpChamberMan.Output.Monitor]
            dv.OPCItemNameTextBoxes[398] = dic["rOxygenConcentration_Chamber"];
            dv.OPCItemNameTextBoxes[399] = dic["rAirPressure_Chamber"];
            dv.OPCItemNameTextBoxes[400] = dic["rGasFlow_Chamber"];

            //[gxGrpChamberMan.Output.State]
            dv.OPCItemNameTextBoxes[471] = dic["bReady_Chamber"];
            dv.OPCItemNameTextBoxes[472] = dic["bOperation_Chamber"];

            //[gxGrpChamberMan.Output.Status]
            dv.OPCItemNameTextBoxes[478] = dic["bAirPressureOk_Chamber"];
            dv.OPCItemNameTextBoxes[479] = dic["bGasPressureOk_Chamber"];
            dv.OPCItemNameTextBoxes[480] = dic["bDoorSensorFront_Chamber"];
            dv.OPCItemNameTextBoxes[481] = dic["bDoorSensorRear_Chamber"];

            //[gxGrpFilterMan.Input.Command]
            dv.OPCItemNameTextBoxes[401] = dic["bEnable_Filter"];

            //[gxGrpFilterMan.Input.Configuration]
            dv.OPCItemNameTextBoxes[402] = dic["stFilterType_Filter"];
            dv.OPCItemNameTextBoxes[403] = dic["rFilterPressureLimit[0]_Filter"];
            dv.OPCItemNameTextBoxes[404] = dic["rFilterPressureLimit[1]_Filter"];

            //[gxGrpFilterMan.Output.Monitor]
            dv.OPCItemNameTextBoxes[405] = dic["rMainPressureSensor_Filter"];
            dv.OPCItemNameTextBoxes[406] = dic["rFilterPressureSensor[0]_Filter"];
            dv.OPCItemNameTextBoxes[407] = dic["rFilterPressureSensor[1]_Filter"];

            //[gxGrpAutoMan.Input.Command]
            dv.OPCItemNameTextBoxes[473] = dic["bLaser"];

            //[gxGrpAutoMan.Input.Configuration]
            dv.OPCItemNameTextBoxes[474] = dic["stLaserMode"];
            #endregion

            #region Write(230)
            /*===============================Write==================================*/

            //[gIntp.Input.Machine.Command]
            dv.OPCItemNameWriteTextBoxes[0] = dic["bAutoParamApply"];
            dv.OPCItemNameWriteTextBoxes[1] = dic["bAutoPause"];
            dv.OPCItemNameWriteTextBoxes[2] = dic["bAutoStart"];
            dv.OPCItemNameWriteTextBoxes[3] = dic["bStop"];
            dv.OPCItemNameWriteTextBoxes[4] = dic["bHomeParamApply"];
            dv.OPCItemNameWriteTextBoxes[5] = dic["bInternalOperationOn"];
            dv.OPCItemNameWriteTextBoxes[6] = dic["bHomeAll"];
            dv.OPCItemNameWriteTextBoxes[7] = dic["bHomeStop"];
            dv.OPCItemNameWriteTextBoxes[8] = dic["bResetHome"];
            dv.OPCItemNameWriteTextBoxes[215] = dic["bDocking"];
            dv.OPCItemNameWriteTextBoxes[216] = dic["bUnDocking"];
            dv.OPCItemNameWriteTextBoxes[217] = dic["bDockingConfirm"];
            dv.OPCItemNameWriteTextBoxes[218] = dic["bUnDockingConfirm"];
            dv.OPCItemNameWriteTextBoxes[219] = dic["bPrintStart"];
            dv.OPCItemNameWriteTextBoxes[226] = dic["bPrintFinsh"];

            //[gIntp.Input.Machine.Param]
            dv.OPCItemNameWriteTextBoxes[9] = dic["rSpeedOverride"];
            dv.OPCItemNameWriteTextBoxes[10] = dic["rMovePosition"];
            dv.OPCItemNameWriteTextBoxes[11] = dic["rDockingPosition"];
            dv.OPCItemNameWriteTextBoxes[12] = dic["rUnDockingPosition"];
            dv.OPCItemNameWriteTextBoxes[13] = dic["rReadyPosition"];
            dv.OPCItemNameWriteTextBoxes[14] = dic["rPowderFeederAngle"];
            dv.OPCItemNameWriteTextBoxes[15] = dic["rPowderGetPosition"];
            dv.OPCItemNameWriteTextBoxes[16] = dic["rPrintDistance"];
            dv.OPCItemNameWriteTextBoxes[17] = dic["rRecoaterMovePosition"];

            //[gIntp.Input.Axes[0].Command]
            dv.OPCItemNameWriteTextBoxes[18] = dic["bAbsolute_Xco"];
            dv.OPCItemNameWriteTextBoxes[19] = dic["bAdditiveNeg_Xco"];
            dv.OPCItemNameWriteTextBoxes[20] = dic["bAdditivePos_Xco"];
            dv.OPCItemNameWriteTextBoxes[21] = dic["bCyclicPos_Xco"];
            dv.OPCItemNameWriteTextBoxes[22] = dic["bHome_Xco"];
            dv.OPCItemNameWriteTextBoxes[23] = dic["bHomeCalibration_Xco"];
            dv.OPCItemNameWriteTextBoxes[24] = dic["bJogNeg_Xco"];
            dv.OPCItemNameWriteTextBoxes[25] = dic["bJogPos_Xco"];

            //[gIntp.Input.Axes[0].Param]
            dv.OPCItemNameWriteTextBoxes[26] = dic["rDistance_Xpa"];
            dv.OPCItemNameWriteTextBoxes[27] = dic["nHomeMode_Xpa"];
            dv.OPCItemNameWriteTextBoxes[28] = dic["nHomeOrder_Xpa"];
            dv.OPCItemNameWriteTextBoxes[29] = dic["nHomeType_Xpa"];
            dv.OPCItemNameWriteTextBoxes[30] = dic["rHomeVelocity_Xpa"];
            dv.OPCItemNameWriteTextBoxes[31] = dic["rHomeAcceleration_Xpa"];
            dv.OPCItemNameWriteTextBoxes[32] = dic["rHomeDeceleration_Xpa"];
            dv.OPCItemNameWriteTextBoxes[33] = dic["rHomeDistance_Xpa"];
            dv.OPCItemNameWriteTextBoxes[34] = dic["rHomePosition_Xpa"];
            dv.OPCItemNameWriteTextBoxes[35] = dic["nHomeSensorPosType_Xpa"];
            dv.OPCItemNameWriteTextBoxes[36] = dic["rPosition_Xpa"];

            //[gIntp.Input.Axes[1].Command]
            dv.OPCItemNameWriteTextBoxes[37] = dic["bAbsolute_Rco"];
            dv.OPCItemNameWriteTextBoxes[38] = dic["bAdditiveNeg_Rco"];
            dv.OPCItemNameWriteTextBoxes[39] = dic["bAdditivePos_Rco"];
            dv.OPCItemNameWriteTextBoxes[40] = dic["bCyclicPos_Rco"];
            dv.OPCItemNameWriteTextBoxes[41] = dic["bHome_Rco"];
            dv.OPCItemNameWriteTextBoxes[42] = dic["bHomeCalibration_Rco"];
            dv.OPCItemNameWriteTextBoxes[43] = dic["bJogNeg_Rco"];
            dv.OPCItemNameWriteTextBoxes[44] = dic["bJogPos_Rco"];

            //[gIntp.Input.Axes[1].Param]
            dv.OPCItemNameWriteTextBoxes[45] = dic["rDistance_Rpa"];
            dv.OPCItemNameWriteTextBoxes[46] = dic["nHomeMode_Rpa"];
            dv.OPCItemNameWriteTextBoxes[47] = dic["nHomeOrder_Rpa"];
            dv.OPCItemNameWriteTextBoxes[48] = dic["nHomeType_Rpa"];
            dv.OPCItemNameWriteTextBoxes[49] = dic["rHomeVelocity_Rpa"];
            dv.OPCItemNameWriteTextBoxes[50] = dic["rHomeAcceleration_Rpa"];
            dv.OPCItemNameWriteTextBoxes[51] = dic["rHomeDeceleration_Rpa"];
            dv.OPCItemNameWriteTextBoxes[52] = dic["rHomeDistance_Rpa"];
            dv.OPCItemNameWriteTextBoxes[53] = dic["rHomePosition_Rpa"];
            dv.OPCItemNameWriteTextBoxes[54] = dic["nHomeSensorPosType_Rpa"];
            dv.OPCItemNameWriteTextBoxes[55] = dic["rPosition_Rpa"];

            //[gIntp.Input.Axes[2].Command]
            dv.OPCItemNameWriteTextBoxes[56] = dic["bAbsolute_Zco"];
            dv.OPCItemNameWriteTextBoxes[57] = dic["bAdditiveNeg_Zco"];
            dv.OPCItemNameWriteTextBoxes[58] = dic["bAdditivePos_Zco"];
            dv.OPCItemNameWriteTextBoxes[59] = dic["bCyclicPos_Zco"];
            dv.OPCItemNameWriteTextBoxes[60] = dic["bHome_Zco"];
            dv.OPCItemNameWriteTextBoxes[61] = dic["bHomeCalibration_Zco"];
            dv.OPCItemNameWriteTextBoxes[62] = dic["bJogNeg_Zco"];
            dv.OPCItemNameWriteTextBoxes[63] = dic["bJogPos_Zco"];

            //[gIntp.Input.Axes[2].Param]
            dv.OPCItemNameWriteTextBoxes[64] = dic["rDistance_Zpa"];
            dv.OPCItemNameWriteTextBoxes[65] = dic["nHomeMode_Zpa"];
            dv.OPCItemNameWriteTextBoxes[66] = dic["nHomeOrder_Zpa"];
            dv.OPCItemNameWriteTextBoxes[67] = dic["nHomeType_Zpa"];
            dv.OPCItemNameWriteTextBoxes[68] = dic["rHomeVelocity_Zpa"];
            dv.OPCItemNameWriteTextBoxes[69] = dic["rHomeAcceleration_Zpa"];
            dv.OPCItemNameWriteTextBoxes[70] = dic["rHomeDeceleration_Zpa"];
            dv.OPCItemNameWriteTextBoxes[71] = dic["rHomeDistance_Zpa"];
            dv.OPCItemNameWriteTextBoxes[72] = dic["rHomePosition_Zpa"];
            dv.OPCItemNameWriteTextBoxes[73] = dic["nHomeSensorPosType_Zpa"];
            dv.OPCItemNameWriteTextBoxes[74] = dic["rPosition_Zpa"];

            //[gIntp.Input.Axes[3].Command]
            dv.OPCItemNameWriteTextBoxes[75] = dic["bAbsolute_Sco"];
            dv.OPCItemNameWriteTextBoxes[76] = dic["bAdditiveNeg_Sco"];
            dv.OPCItemNameWriteTextBoxes[77] = dic["bAdditivePos_Sco"];
            dv.OPCItemNameWriteTextBoxes[78] = dic["bCyclicPos_Sco"];
            dv.OPCItemNameWriteTextBoxes[79] = dic["bHome_Sco"];
            dv.OPCItemNameWriteTextBoxes[80] = dic["bHomeCalibration_Sco"];
            dv.OPCItemNameWriteTextBoxes[81] = dic["bJogNeg_Sco"];
            dv.OPCItemNameWriteTextBoxes[82] = dic["bJogPos_Sco"];

            //[gIntp.Input.Axes[3].Param]
            dv.OPCItemNameWriteTextBoxes[83] = dic["rDistance_Spa"];
            dv.OPCItemNameWriteTextBoxes[84] = dic["nHomeMode_Spa"];
            dv.OPCItemNameWriteTextBoxes[85] = dic["nHomeOrder_Spa"];
            dv.OPCItemNameWriteTextBoxes[86] = dic["nHomeType_Spa"];
            dv.OPCItemNameWriteTextBoxes[87] = dic["rHomeVelocity_Spa"];
            dv.OPCItemNameWriteTextBoxes[88] = dic["rHomeAcceleration_Spa"];
            dv.OPCItemNameWriteTextBoxes[89] = dic["rHomeDeceleration_Spa"];
            dv.OPCItemNameWriteTextBoxes[90] = dic["rHomeDistance_Spa"];
            dv.OPCItemNameWriteTextBoxes[91] = dic["rHomePosition_Spa"];
            dv.OPCItemNameWriteTextBoxes[92] = dic["nHomeSensorPosType_Spa"];
            dv.OPCItemNameWriteTextBoxes[93] = dic["rPosition_Spa"];

            //[gIntp.Input.Alarm]
            dv.OPCItemNameWriteTextBoxes[94] = dic["bAck"];
            dv.OPCItemNameWriteTextBoxes[95] = dic["bAckAll"];

            //[gIntp.Input.Module.Digital]
            dv.OPCItemNameWriteTextBoxes[96] = dic["DigitalMode"];

            //[gIntp.Input.Module.Digital.X0]
            dv.OPCItemNameWriteTextBoxes[97] = dic["bEnableForce_X0"];
            dv.OPCItemNameWriteTextBoxes[98] = dic["bForceValue[0]_X0"];
            dv.OPCItemNameWriteTextBoxes[99] = dic["bForceValue[1]_X0"];
            dv.OPCItemNameWriteTextBoxes[100] = dic["bForceValue[2]_X0"];
            dv.OPCItemNameWriteTextBoxes[101] = dic["bForceValue[3]_X0"];
            dv.OPCItemNameWriteTextBoxes[102] = dic["bForceValue[4]_X0"];
            dv.OPCItemNameWriteTextBoxes[103] = dic["bForceValue[5]_X0"];
            dv.OPCItemNameWriteTextBoxes[104] = dic["bForceValue[6]_X0"];
            dv.OPCItemNameWriteTextBoxes[105] = dic["bForceValue[7]_X0"];
            dv.OPCItemNameWriteTextBoxes[106] = dic["bForceValue[8]_X0"];
            dv.OPCItemNameWriteTextBoxes[107] = dic["bForceValue[9]_X0"];
            dv.OPCItemNameWriteTextBoxes[108] = dic["bForceValue[10]_X0"];
            dv.OPCItemNameWriteTextBoxes[109] = dic["bForceValue[11]_X0"];
            dv.OPCItemNameWriteTextBoxes[110] = dic["nBitForceValue_X0"];

            //[gIntp.Input.Module.Digital.X1]
            dv.OPCItemNameWriteTextBoxes[111] = dic["bEnableForce_X1"];
            dv.OPCItemNameWriteTextBoxes[112] = dic["bForceValue[0]_X1"];
            dv.OPCItemNameWriteTextBoxes[113] = dic["bForceValue[1]_X1"];
            dv.OPCItemNameWriteTextBoxes[114] = dic["bForceValue[2]_X1"];
            dv.OPCItemNameWriteTextBoxes[115] = dic["bForceValue[3]_X1"];
            dv.OPCItemNameWriteTextBoxes[116] = dic["bForceValue[4]_X1"];
            dv.OPCItemNameWriteTextBoxes[117] = dic["bForceValue[5]_X1"];
            dv.OPCItemNameWriteTextBoxes[118] = dic["bForceValue[6]_X1"];
            dv.OPCItemNameWriteTextBoxes[119] = dic["bForceValue[7]_X1"];
            dv.OPCItemNameWriteTextBoxes[120] = dic["bForceValue[8]_X1"];
            dv.OPCItemNameWriteTextBoxes[121] = dic["bForceValue[9]_X1"];
            dv.OPCItemNameWriteTextBoxes[122] = dic["bForceValue[10]_X1"];
            dv.OPCItemNameWriteTextBoxes[123] = dic["bForceValue[11]_X1"];
            dv.OPCItemNameWriteTextBoxes[124] = dic["nBitForceValue_X1"];

            //[gIntp.Input.Module.Digital.X2]
            dv.OPCItemNameWriteTextBoxes[125] = dic["bEnableForce_X2"];
            dv.OPCItemNameWriteTextBoxes[126] = dic["bForceValue[0]_X2"];
            dv.OPCItemNameWriteTextBoxes[127] = dic["bForceValue[1]_X2"];
            dv.OPCItemNameWriteTextBoxes[128] = dic["bForceValue[2]_X2"];
            dv.OPCItemNameWriteTextBoxes[129] = dic["bForceValue[3]_X2"];
            dv.OPCItemNameWriteTextBoxes[130] = dic["bForceValue[4]_X2"];
            dv.OPCItemNameWriteTextBoxes[131] = dic["bForceValue[5]_X2"];
            dv.OPCItemNameWriteTextBoxes[132] = dic["bForceValue[6]_X2"];
            dv.OPCItemNameWriteTextBoxes[133] = dic["bForceValue[7]_X2"];
            dv.OPCItemNameWriteTextBoxes[134] = dic["bForceValue[8]_X2"];
            dv.OPCItemNameWriteTextBoxes[135] = dic["bForceValue[9]_X2"];
            dv.OPCItemNameWriteTextBoxes[136] = dic["bForceValue[10]_X2"];
            dv.OPCItemNameWriteTextBoxes[137] = dic["bForceValue[11]_X2"];
            dv.OPCItemNameWriteTextBoxes[138] = dic["nBitForceValue_X2"];

            //[gIntp.Input.Module.Analog.XA0]
            dv.OPCItemNameWriteTextBoxes[139] = dic["bEnableForce_XA0"];
            dv.OPCItemNameWriteTextBoxes[140] = dic["nForceValue[0]_XA0"];
            dv.OPCItemNameWriteTextBoxes[141] = dic["nForceValue[1]_XA0"];
            dv.OPCItemNameWriteTextBoxes[142] = dic["nForceValue[2]_XA0"];
            dv.OPCItemNameWriteTextBoxes[143] = dic["nForceValue[3]_XA0"];

            //[gIntp.Input.Module.Analog.XA1]
            dv.OPCItemNameWriteTextBoxes[144] = dic["bEnableForce_XA1"];
            dv.OPCItemNameWriteTextBoxes[145] = dic["nForceValue[0]_XA1"];
            dv.OPCItemNameWriteTextBoxes[146] = dic["nForceValue[1]_XA1"];
            dv.OPCItemNameWriteTextBoxes[147] = dic["nForceValue[2]_XA1"];
            dv.OPCItemNameWriteTextBoxes[148] = dic["nForceValue[3]_XA1"];

            //[gIntp.Input.Module.Analog.XA2]
            dv.OPCItemNameWriteTextBoxes[149] = dic["bEnableForce_XA2"];
            dv.OPCItemNameWriteTextBoxes[150] = dic["nForceValue[0]_XA2"];
            dv.OPCItemNameWriteTextBoxes[151] = dic["nForceValue[1]_XA2"];
            dv.OPCItemNameWriteTextBoxes[152] = dic["nForceValue[2]_XA2"];
            dv.OPCItemNameWriteTextBoxes[153] = dic["nForceValue[3]_XA2"];

            //[gIntp.Input.Module.Analog.XA3]
            dv.OPCItemNameWriteTextBoxes[154] = dic["bEnableForce_XA3"];
            dv.OPCItemNameWriteTextBoxes[155] = dic["nForceValue[0]_XA3"];
            dv.OPCItemNameWriteTextBoxes[156] = dic["nForceValue[1]_XA3"];
            dv.OPCItemNameWriteTextBoxes[157] = dic["nForceValue[2]_XA3"];
            dv.OPCItemNameWriteTextBoxes[158] = dic["nForceValue[3]_XA3"];

            //[gIntp.Input.Module.Analog.XA4]
            dv.OPCItemNameWriteTextBoxes[159] = dic["bEnableForce_XA4"];
            dv.OPCItemNameWriteTextBoxes[160] = dic["nForceValue[0]_XA4"];
            dv.OPCItemNameWriteTextBoxes[161] = dic["nForceValue[1]_XA4"];
            dv.OPCItemNameWriteTextBoxes[162] = dic["nForceValue[2]_XA4"];
            dv.OPCItemNameWriteTextBoxes[163] = dic["nForceValue[3]_XA4"];

            //[gIntp.Input.Module.Analog.XA5]
            dv.OPCItemNameWriteTextBoxes[164] = dic["bEnableForce_XA5"];
            dv.OPCItemNameWriteTextBoxes[165] = dic["nForceValue[0]_XA5"];
            dv.OPCItemNameWriteTextBoxes[166] = dic["nForceValue[1]_XA5"];
            dv.OPCItemNameWriteTextBoxes[167] = dic["nForceValue[2]_XA5"];
            dv.OPCItemNameWriteTextBoxes[168] = dic["nForceValue[3]_XA5"];

            //[gIntp.Input.Module.Analog.XA6]
            dv.OPCItemNameWriteTextBoxes[169] = dic["bEnableForce_XA6"];
            dv.OPCItemNameWriteTextBoxes[170] = dic["nForceValue[0]_XA6"];
            dv.OPCItemNameWriteTextBoxes[171] = dic["nForceValue[1]_XA6"];
            dv.OPCItemNameWriteTextBoxes[172] = dic["nForceValue[2]_XA6"];
            dv.OPCItemNameWriteTextBoxes[173] = dic["nForceValue[3]_XA6"];

            //[gIntp.Input.Module.Analog.XA7]
            dv.OPCItemNameWriteTextBoxes[174] = dic["bEnableForce_XA7"];
            dv.OPCItemNameWriteTextBoxes[175] = dic["nForceValue[0]_XA7"];
            dv.OPCItemNameWriteTextBoxes[176] = dic["nForceValue[1]_XA7"];
            dv.OPCItemNameWriteTextBoxes[177] = dic["nForceValue[2]_XA7"];
            dv.OPCItemNameWriteTextBoxes[178] = dic["nForceValue[3]_XA7"];

            //[gIntp.Input.Module.Analog.XA8]
            dv.OPCItemNameWriteTextBoxes[179] = dic["bEnableForce_XA8"];
            dv.OPCItemNameWriteTextBoxes[180] = dic["nForceValue[0]_XA8"];
            dv.OPCItemNameWriteTextBoxes[181] = dic["nForceValue[1]_XA8"];
            dv.OPCItemNameWriteTextBoxes[182] = dic["nForceValue[2]_XA8"];
            dv.OPCItemNameWriteTextBoxes[183] = dic["nForceValue[3]_XA8"];

            //[gIntp.Input.Module.Analog.XA9]
            dv.OPCItemNameWriteTextBoxes[184] = dic["bEnableForce_XA9"];
            dv.OPCItemNameWriteTextBoxes[185] = dic["nForceValue[0]_XA9"];
            dv.OPCItemNameWriteTextBoxes[186] = dic["nForceValue[1]_XA9"];
            dv.OPCItemNameWriteTextBoxes[187] = dic["nForceValue[2]_XA9"];
            dv.OPCItemNameWriteTextBoxes[188] = dic["nForceValue[3]_XA9"];

            //[gIntp.Input.Module.Analog.XA10]
            dv.OPCItemNameWriteTextBoxes[189] = dic["bEnableForce_XA10"];
            dv.OPCItemNameWriteTextBoxes[190] = dic["nForceValue[0]_XA10"];
            dv.OPCItemNameWriteTextBoxes[191] = dic["nForceValue[1]_XA10"];
            dv.OPCItemNameWriteTextBoxes[192] = dic["nForceValue[2]_XA10"];
            dv.OPCItemNameWriteTextBoxes[193] = dic["nForceValue[3]_XA10"];

            //[gIntp.Input.Module.Analog.XA11]
            dv.OPCItemNameWriteTextBoxes[194] = dic["bEnableForce_XA11"];
            dv.OPCItemNameWriteTextBoxes[195] = dic["nForceValue[0]_XA11"];
            dv.OPCItemNameWriteTextBoxes[196] = dic["nForceValue[1]_XA11"];
            dv.OPCItemNameWriteTextBoxes[197] = dic["nForceValue[2]_XA11"];
            dv.OPCItemNameWriteTextBoxes[198] = dic["nForceValue[3]_XA11"];

            //[gxGrpPumpMan[0].Input.Command]
            dv.OPCItemNameWriteTextBoxes[199] = dic["bStop_pump1"];
            dv.OPCItemNameWriteTextBoxes[200] = dic["bTuning_pump1"];
            dv.OPCItemNameWriteTextBoxes[201] = dic["bControl_pump1"];

            //[gxGrpPumpMan[1].Input.Command]
            dv.OPCItemNameWriteTextBoxes[220] = dic["bStop_pump2"];
            dv.OPCItemNameWriteTextBoxes[221] = dic["bTuning_pump2"];
            dv.OPCItemNameWriteTextBoxes[222] = dic["bControl_pump2"];

            //[gxGrpPumpMan[0].Input.Parameter]
            dv.OPCItemNameWriteTextBoxes[202] = dic["rSetValue_pump1"];

            //[gxGrpPumpMan[1].Input.Parameter]
            dv.OPCItemNameWriteTextBoxes[223] = dic["rSetValue_pump2"];

            //[gxGrpChamberMan.Input.Command]
            dv.OPCItemNameWriteTextBoxes[203] = dic["bValveOn_Chamber"];
            dv.OPCItemNameWriteTextBoxes[204] = dic["bValveOff_Chamber"];
            dv.OPCItemNameWriteTextBoxes[227] = dic["bLed1On_Chamber"];
            dv.OPCItemNameWriteTextBoxes[228] = dic["bLed2On_Chamber"];

            //[gxGrpChamberMan.Input.Configuration]
            dv.OPCItemNameWriteTextBoxes[205] = dic["rMaxOxygenConcentration_Chamber"];
            dv.OPCItemNameWriteTextBoxes[206] = dic["rMinOxygenConcentration_Chamber"];
            dv.OPCItemNameWriteTextBoxes[207] = dic["rMaxAirPressure_Chamber"];
            dv.OPCItemNameWriteTextBoxes[208] = dic["rMaxGasFlow_Chamber"];
            dv.OPCItemNameWriteTextBoxes[209] = dic["stGasType_Chamber"];

            //[gxGrpFilterMan.Input.Command]
            dv.OPCItemNameWriteTextBoxes[210] = dic["bEnable_Filter"];

            //[gxGrpFilterMan.Input.Configuration]
            dv.OPCItemNameWriteTextBoxes[211] = dic["stFilterType_Filter"];
            dv.OPCItemNameWriteTextBoxes[212] = dic["rFilterPressureLimit[0]_Filter"];
            dv.OPCItemNameWriteTextBoxes[213] = dic["rFilterPressureLimit[1]_Filter"];

            //[mOpcUaTag]
            dv.OPCItemNameWriteTextBoxes[214] = dic["strJobFile"];
            dv.OPCItemNameWriteTextBoxes[229] = dic["strScanSystem"];

            //[gxGrpAutoMan.Input.Command]
            dv.OPCItemNameWriteTextBoxes[224] = dic["bLaser"];

            //[gxGrpAutoMan.Input.Configuration]
            dv.OPCItemNameWriteTextBoxes[225] = dic["stLaserMode"];
            #endregion

            try
            {
                if (machineName.Contains("M500"))
                {
                    HMIM270 hmiM270 = new HMIM270(dv);

                    
                        hmiM270.Show();
                        this.Hide();
                        string profilePath = "C:\\Depert_Profile\\temp.dpf";
                        string strProfile = "[MACHINE]\n" + "machineName\n" + dv.HostName + /*"\n" + dv.FtpName +*/ "\n" + dv.UserName + "\n" + dv.Password;

                        using (StreamWriter sw = new StreamWriter(profilePath))
                        {
                            sw.WriteLine(strProfile);
                        }
                    
                }
                return dv;
            }
            catch (Exception e)
            {
                Debug.WriteLine("e : " + e.Message);
                MessageBox.Show(e.Message);
                return dv;

            }
        }
    }
}

