using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using LiveCharts.Helpers;
using Microsoft.Win32;
using System.ComponentModel;
using DPT_WPF.ucCommon;
using System.Windows.Media.Animation;
using System.Diagnostics;
using DPT_WPF.ucM270;
using System.Timers;
using OpcLabs.EasyOpc.UA;

namespace DPT_WPF
{
    /// <summary>
    /// HMIM270.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HMIM270 : Window
    {
        #region pre-Definition
        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        DefineValue d;

        //(client).ReadValue(m500opcuri, ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeVelocity).ToString());
        //client.WriteValue(m500opcuri, "ns=2;s=MCP.User Channels.Supervisor.\"Supervisor.Message Request\"", "{\"message\":\"Job.Pause\"}");
        
        //Windows
        motorError me = new motorError();
        powderSupplyWindow psupply = new powderSupplyWindow();
        AutoPumpWindow apw = new AutoPumpWindow();
        ValueSelect vs = new ValueSelect();

        private double tempPowderSupply = 0;
        private double bteSeconds = 0;
        private double bteClock = 0;
        private string bteClockTest = "00h:00m:00s";
        private string tempLaser = "False";
        private string tempGuideBeam = "False";
        private double totalFileSize = 0;
        private bool _timerPrintingRunning = false;
        private int pCheckNumber;

        public string tempPrintingProfile = "";
        public int alramPoint = 0;
        //흘러가는 시간
        DateTime ps_StartTime;
        string psCurrentTime = string.Empty;

        //출력예상 시간 
        System.Timers.Timer bteTimer = new System.Timers.Timer();

        private double timeChnage = 0;
        private readonly ChartValues<GanttPoint> _values;

        List<string> labels = new List<string>();
        private BackgroundWorker linethread = new BackgroundWorker();

        #endregion

        public HMIM270()
        {
            InitializeComponent();
        }

        public HMIM270(DefineValue dv)
        {
            InitializeComponent();
            _timerPrintingRunning = false;


            d = dv;
            this.DataContext = dv;

            cd_FileLoading cdFileLoading = new cd_FileLoading();

            string[] strfile = cdFileLoading.GetFileList(d.FtpName);

            for (int i = 0; i < strfile.Length; i++)
            {
                strfile[i] = strfile[i].Substring(0, strfile[i].Length-1);
                    };
            
            lvFile.ItemsSource = strfile;

            // 이벤트 선언부

            ucLogin uclogin = new ucLogin(dv);
            ucCommonLED ucled = new ucCommonLED(dv);
            ucCommonGAS uccommongas = new ucCommonGAS(dv);
            ucCommonPump uccomminpump = new ucCommonPump(dv);
            ucCommonM270TemperHumity ucccommonTemper = new ucCommonM270TemperHumity(dv);
            ucCommonMotor ucmotor = new ucCommonMotor(dv);

            /*  M500 */
            dv.StartCommand = new DelegateCommand(this.Start);
            dv.PrintStartCommand = new DelegateCommand(this.PrintStart);
            dv.PrintStopCommand = new DelegateCommand(this.PrintStop);
            dv.PrintPauseCommand = new DelegateCommand(this.PrintPause);
            dv.PrintResumeCommand = new DelegateCommand(this.PrintResume);
            dv.PrintFinishCommand = new DelegateCommand(this.PrintFinish);

            dv.DockingCommand = new DelegateCommand(this.DockingBtn);
            dv.UnDockingCommand = new DelegateCommand(this.UnDockingBtn);
            dv.DockingConfirmCommand = new DelegateCommand(this.DockingConfirm);
            dv.AlramImageCommand = new DelegateCommand(this.AlramImageBtn);

            dv.MotorStopCommand = new DelegateCommand(this.MotorStop);

            dv.Vel_XCommand = new DelegateCommand(this.SelectVel_X);
            dv.Vel_RCommand = new DelegateCommand(this.SelectVel_R);
            dv.Vel_ZCommand = new DelegateCommand(this.SelectVel_Z);
            dv.Vel_SCommand = new DelegateCommand(this.SelectVel_S);

            dv.Acc_XCommand = new DelegateCommand(this.SelectAcc_X);
            dv.Acc_RCommand = new DelegateCommand(this.SelectAcc_R);
            dv.Acc_ZCommand = new DelegateCommand(this.SelectAcc_Z);
            dv.Acc_SCommand = new DelegateCommand(this.SelectAcc_S);

            dv.Dec_XCommand = new DelegateCommand(this.SelectDec_X);
            dv.Dec_RCommand = new DelegateCommand(this.SelectDec_R);
            dv.Dec_ZCommand = new DelegateCommand(this.SelectDec_Z);
            dv.Dec_SCommand = new DelegateCommand(this.SelectDec_S);

            dv.Dis_XCommand = new DelegateCommand(this.SelectDis_X);
            dv.Dis_RCommand = new DelegateCommand(this.SelectDis_R);
            dv.Dis_ZCommand = new DelegateCommand(this.SelectDis_Z);
            dv.Dis_SCommand = new DelegateCommand(this.SelectDis_S);

            dv.TP_XCommand = new DelegateCommand(this.SelectTP_X);
            dv.TP_RCommand = new DelegateCommand(this.SelectTP_R);
            dv.TP_ZCommand = new DelegateCommand(this.SelectTP_Z);
            dv.TP_SCommand = new DelegateCommand(this.SelectTP_S);

            dv.Dir_XCommand = new DelegateCommand(this.SelectDir_X);
            dv.Dir_RCommand = new DelegateCommand(this.SelectDir_R);
            dv.Dir_ZCommand = new DelegateCommand(this.SelectDir_Z);
            dv.Dir_SCommand = new DelegateCommand(this.SelectDir_S);

            dv.Order_XCommand = new DelegateCommand(this.SelectOrder_X);
            dv.Order_RCommand = new DelegateCommand(this.SelectOrder_R);
            dv.Order_ZCommand = new DelegateCommand(this.SelectOrder_Z);
            dv.Order_SCommand = new DelegateCommand(this.SelectOrder_S);

            dv.HomingSaveCommand = new DelegateCommand(this.HomingSave);
            dv.HomingStartCommand = new DelegateCommand(this.HomingStart);
            dv.HomingStopCommand = new DelegateCommand(this.HomingStop);
            dv.HomingResetCommand = new DelegateCommand(this.HomingReset);

            dv.OverrideDownCommand = new DelegateCommand(this.OverrideDown);
            dv.OverrideUpCommand = new DelegateCommand(this.OverrideUp);

            /*///////*/
            dv.Up1PowderCommand = new DelegateCommand(PUp1);
            dv.Up2PowderCommand = new DelegateCommand(this.PUp2);
            dv.Up3PowderCommand = new DelegateCommand(this.PUp3);
            dv.Down1PowderCommand = new DelegateCommand(this.PDown1);
            dv.Down2PowderCommand = new DelegateCommand(this.PDown2);
            dv.Down3PowderCommand = new DelegateCommand(this.PDown3);
            dv.AirPressureCommand = new DelegateCommand(this.AirPressureSwitch);
            dv.AutoSupplyCommand = new DelegateCommand(this.AutoSupplySwitch);
            dv.SupplyRatioCommand = new DelegateCommand(this.PowderSupplyWindow4);

            dv.CloseWindowsCommand = new DelegateCommand(this.CloseWindows);

            dv.PowderSupplyWindowCommand1 = new DelegateCommand(this.PowderSupplyWindow1);
            dv.PowderSupplyWindowCommand2 = new DelegateCommand(this.PowderSupplyWindow2);
            dv.PowderSupplyWindowCommand3 = new DelegateCommand(this.PowderSupplyWindow3);

            dv.PrintResetCommand = new DelegateCommand(this.PrintReset);
            
            dv.PrintFileDeleteCommand = new DelegateCommand(this.PrintFileDelete);
            dv.PrintFileArrayCommand = new DelegateCommand(this.PrintFileArray);

            dv.GuideBeamCommand = new DelegateCommand(this.GuideBeam);
            dv.GuideBeamCommandOff = new DelegateCommand(this.GuideBeamOff);
            dv.LaserControlCommand = new DelegateCommand(this.LaserControl);
            dv.LaserControlCommandOff = new DelegateCommand(this.LaserControlOff);

            dv.PowderSupplyCommand = new DelegateCommand(this.PowderSupplyRatio);

            dv.AutoPumpCommand = new DelegateCommand(this.AutoPumpCommand);
            dv.Up1PumpCommand = new DelegateCommand(this.PumpUp1);
            dv.Down1PumpCommand = new DelegateCommand(this.PumpDown1);
            dv.Up2PumpCommand = new DelegateCommand(this.PumpUp2);
            dv.Down2PumpCommand = new DelegateCommand(this.PumpDown2);

            
            var now = DateTime.Now;

            _values = new ChartValues<GanttPoint>
            {
                 new GanttPoint(now.Ticks, now.Ticks),
            };

            DateTime datesXStatr = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

            dv.PrintWeekend = labels.ToArray();
            //axisPrinting.Unit = TimeSpan.TicksPerDay;
            dv.AxisStep = TimeSpan.FromDays(1).Ticks;
            dv.DayofWeek = values => new DateTime((long)values).ToString("MMM월dd일HH시");

            dv.Formatter = value => new DateTime((long)value).ToString("dd MMM");
            dv.PrintingFormatter = x => x.ToString("N2") + "일";
            dv.XPointer = 0;
            dv.YPointer = 0;

            // ChamberOxy 차트
            dv.ChamberOxySeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4]))
                    }
                }
            };
           
            linethread.WorkerReportsProgress = true;

            // 작업 취소 여부
            linethread.WorkerSupportsCancellation = true;

            // 작업 쓰레드 
            linethread.DoWork += new DoWorkEventHandler(linethread_DoWork);

            // 진행률 변경
            linethread.ProgressChanged += new ProgressChangedEventHandler(linethread_ProgressChanged);

            // 작업 완료
            linethread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(linethread_RunWorkerCompleted);

            // 0.5초마다 장비에서 데이터를 받아 업데이트 하는 부분
            Task.Run(() =>
            {
                while (true)
                {
                    if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[1].Error.Text.strLine3").ToString() != null)
                    {
                        try
                        {
                            Thread.Sleep(500);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                //500alramImage
                                if (alramPoint == 1 && ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.Machine.nId").ToString() != "0" || (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.bInError").ToString() == "True" || (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.Information.nId").ToString() != "0"))
                                {
                                    alramImage.Visibility = Visibility.Visible;
                                }
                                else if (alramPoint == 0)
                                {
                                    alramImage.Visibility = Visibility.Collapsed;
                                }
                                if (alramPoint == 0 && (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.Machine.nId").ToString() != "0")
                                {
                                    alramPoint = 2;
                                    ErrorWindow1();
                                }
                                if (alramPoint == 0 && (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.bInError").ToString() == "True")
                                {
                                    alramPoint = 2;
                                    ErrorWindow2();
                                }
                                if (alramPoint == 0 && (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.Information.nId").ToString() != "0")
                                {
                                    alramPoint = 2;
                                    ErrorWindow3();
                                }


                                if (bteClock > 0)
                                {
                                    lblPsTime.Content = bteClockTest;
                                }
                                else
                                {
                                    lblPsTime.Content = "00h:00m:00s";
                                }

                                #region Sensor                     
                                PlatePressure1.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPlateFormReleasePressure").ToString()), 2);
                                VacuumPressure1.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rVacuumGenerator1Pressure").ToString()), 2);
                                VacuumPressure2.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rVacuumGenerator2Pressure").ToString()), 2);
                                PlatePressure2.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rHoldClampPressure").ToString()), 2);
                                GasSupplyLine1Pressure.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rGasSupplyLine1Pressure").ToString()), 2);
                                FilterMainPressureSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rFilterMainPressureSensor").ToString()), 2);
                                FilterSub1PressureSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rFilterSub1PressureSensor").ToString()), 2);
                                FilterSub2PressureSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rFilterSub2PressureSensor").ToString()), 2);
                                GasSupplyLine2Pressure.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rGasSupplyLine2Pressure").ToString()), 2);
                                PumpMainPressureSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpMainPressureSensor").ToString()), 2);
                                PumpSubPressureSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpSubPressureSensor").ToString()), 2);
                                MainAirLine1Pressure.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rMainAirLine1Pressure").ToString()), 2);
                                MainAirLine2Pressure.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rMainAirLine2Pressure").ToString()), 2);
                                VacuumGenerator1Flow.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rVacuumGenerator1Flow").ToString()), 2);
                                VacuumGenerator2Flow.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rVacuumGenerator2Flow").ToString()), 2);
                                HoldClampFlow.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rHoldClampFlow").ToString()), 2);
                                GasSupplyLine1Flow.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rGasSupplyLine1Flow").ToString()), 2);
                                GasSupplyLine2Flow.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rGasSupplyLine2Flow").ToString()), 2);
                                PumpMainWindSpeedSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpMainWindSpeedSensor").ToString()), 2);
                                PumpSubWindSpeedSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpSubWindSpeedSensor").ToString()), 2);
                                ChamberOxygenSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rChamberOxygenSensor").ToString()), 2);
                                PumpMainOxygenSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpMainOxygenSensor").ToString()), 2);
                                PumpSubOxygenSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpSubOxygenSensor").ToString()), 2);
                                PumpMainDewPointSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpMainDewPointSensor").ToString()), 2);
                                PumpSubDewPointSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpSubDewPointSensor").ToString()), 2);
                                PumpMainTempSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpMainTempSensor").ToString()), 2);
                                PumpSubTempSensor.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpSubTempSensor").ToString()), 2);
                                PumpMainMotorCurrent.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpMainMotorCurrent").ToString()), 2);
                                PumpMainRelativePressure.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpMainRelativePressure").ToString()), 2);
                                PumpSubMotorCurrent.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpSubMotorCurrent").ToString()), 2);
                                PumpSubRelativePressure.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.Monitor.rPumpSubRelativePressure").ToString()), 2);
                                #endregion

                                #region Motor RealTime
                                _ucCommonMotor.Motor_X_Position.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[0].Monitor.rPosition").ToString()), 2);
                                _ucCommonMotor.Motor_R_Position.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[1].Monitor.rPosition").ToString()), 2);
                                _ucCommonMotor.Motor_Z_Position.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[2].Monitor.rPosition").ToString()), 2);
                                _ucCommonMotor.Motor_S_Position.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[3].Monitor.rPosition").ToString()), 2);

                                _ucCommonMotor.motor_X_Speed.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[0].Monitor.rVelocity").ToString()), 2);
                                _ucCommonMotor.motor_R_Speed.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[1].Monitor.rVelocity").ToString()), 2);
                                _ucCommonMotor.motor_Z_Speed.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[2].Monitor.rVelocity").ToString()), 2);
                                _ucCommonMotor.motor_S_Speed.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Axes[3].Monitor.rVelocity").ToString()), 2);

                                _ucCommonMotor.Distance_X.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.nDistance").ToString()), 2);
                                _ucCommonMotor.Distance_R.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.nDistance").ToString()), 2);
                                _ucCommonMotor.Distance_Z.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.nDistance").ToString()), 2);
                                _ucCommonMotor.Distance_S.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.nDistance").ToString()), 2);
                                #endregion

                                #region Jog RealTime
                                //X
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bJogNeg").ToString() == "True")
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg1.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg1.Source = new BitmapImage(uriJog);
                                }

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bJogPos").ToString() == "True") 
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos1.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos1.Source = new BitmapImage(uriJog);
                                }

                                //R
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bJogNeg").ToString() == "True") 
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg2.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg2.Source = new BitmapImage(uriJog);
                                }

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bJogPos").ToString() == "True")
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos2.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos2.Source = new BitmapImage(uriJog);
                                }

                                //Z
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogNeg").ToString() == "True")
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg3.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg3.Source = new BitmapImage(uriJog);
                                }

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogPos").ToString() == "True")
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos3.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos3.Source = new BitmapImage(uriJog);
                                }

                                //S
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogNeg").ToString() == "True")
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg4.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgLeftArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogNeg4.Source = new BitmapImage(uriJog);
                                }

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogPos").ToString() == "True")
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrow.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos4.Source = new BitmapImage(uriJog);
                                }
                                else
                                {
                                    var uriJog = new Uri(@"/imgM500/imgRightArrowDisable.png", UriKind.Relative);
                                    _ucCommonMotor.btnimgJogPos4.Source = new BitmapImage(uriJog);
                                }
                                #endregion

                                #region Motor Override
                                
                                OverrideValue.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Param.rSpeedOverride").ToString() + " %";

                                #endregion

                                #region Gas

                                _ucCommonGAS.AirFlow.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Output.Monitor.rAirPressure").ToString()), 2);
                                _ucCommonGAS.GasFlow.Content = Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Output.Monitor.rGasFlow").ToString()), 2);

                                _ucCommonGAS.MaxOxyValue.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.rMaxOxygenConcentration").ToString();
                                _ucCommonGAS.MinOxyValue.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.rMinOxygenConcentration").ToString();

                                //GasSupply
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Output.State.bReady").ToString() == "True")
                                {
                                    var uriGasSupply = new Uri(@"/ImgTab/2_gas/gas botten.png", UriKind.Relative);
                                    _ucCommonGAS.imgGasSupply.Source = new BitmapImage(uriGasSupply);
                                }
                                else if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Output.State.bOperation").ToString() == "True")
                                {
                                    var uriGasSupply = new Uri(@"/ImgTab/2_gas/gas bottenOn4.png", UriKind.Relative);
                                    _ucCommonGAS.imgGasSupply.Source = new BitmapImage(uriGasSupply);
                                }

                                //N2 Ar 선택
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.stGasType").ToString() == "1")
                                {
                                    var uriSourceN2On = new Uri(@"/ImgTab/2_gas/on.png", UriKind.Relative);
                                    var uriSourceArOff = new Uri(@"/ImgTab/2_gas/off.png", UriKind.Relative);
                                    _ucCommonGAS.btnImgN2.Source = new BitmapImage(uriSourceN2On);
                                    _ucCommonGAS.btnImgAr.Source = new BitmapImage(uriSourceArOff);
                                }
                                else if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.stGasType").ToString() == "2")
                                {
                                    var uriSourceArOn = new Uri(@"/ImgTab/2_gas/on.png", UriKind.Relative);
                                    var uriSourceN2Off = new Uri(@"/ImgTab/2_gas/off.png", UriKind.Relative);

                                    _ucCommonGAS.btnImgAr.Source = new BitmapImage(uriSourceArOn);
                                    _ucCommonGAS.btnImgN2.Source = new BitmapImage(uriSourceN2Off);
                                }
                                else
                                {
                                    var uriSourceArOff = new Uri(@"/ImgTab/2_gas/off.png", UriKind.Relative);
                                    var uriSourceN2Off = new Uri(@"/ImgTab/2_gas/off.png", UriKind.Relative);

                                    _ucCommonGAS.btnImgAr.Source = new BitmapImage(uriSourceArOff);
                                    _ucCommonGAS.btnImgN2.Source = new BitmapImage(uriSourceN2Off);
                                }
                                #endregion

                                #region Pump, 뭐지 482 위로 없는뎅
                                _ucPump.MainPumpActValue.Content = (Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Output.Monitor.rActValue").ToString()), 2) + "M/S");
                                _ucPump.SubPumpActValue.Content = (Math.Round(Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Output.Monitor.rActValue").ToString()), 2) + "M/S");

                                _ucPump.MainPumpSetValue.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Parameter.rSetValue").ToString();
                                _ucPump.SubPumpSetValue.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Parameter.rSetValue").ToString();

                                if (d.OPCItemValueTextBoxes[482] == "True")
                                {
                                    _ucPump.BtnMainPumpControl.Visibility = Visibility.Visible;
                                    _ucPump.BtnMainPumpStop.Visibility = Visibility.Collapsed;
                                }
                                else if(d.OPCItemValueTextBoxes[483] == "True")
                                {
                                    _ucPump.BtnMainPumpControl.Visibility = Visibility.Collapsed;
                                    _ucPump.BtnMainPumpStop.Visibility = Visibility.Visible;
                                }

                                if(d.OPCItemValueTextBoxes[484] == "True")
                                {
                                    _ucPump.BtnSubPumpControl.Visibility = Visibility.Visible;
                                    _ucPump.BtnSubPumpStop.Visibility = Visibility.Collapsed;
                                }
                                else if(d.OPCItemValueTextBoxes[485] == "True")
                                {
                                    _ucPump.BtnSubPumpControl.Visibility = Visibility.Collapsed;
                                    _ucPump.BtnSubPumpStop.Visibility = Visibility.Visible;
                                }
                                #endregion

                                #region Printer

                                //start
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bReady").ToString() == "True" || (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bStop").ToString() == "True")
                                {
                                    imgStart.Visibility = Visibility.Visible;
                                    imgPrintStart.Visibility = Visibility.Collapsed;
                                    imgPrintPause.Visibility = Visibility.Collapsed;
                                    imgPrintResume.Visibility = Visibility.Collapsed;
                                    imgPrintFinish.Visibility = Visibility.Collapsed;
                                    imgPrintStop.Visibility = Visibility.Collapsed;
                                }
                                
                                else if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bOperation").ToString() == "True")
                                {
                                    //printStart
                                    if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.SubEnumeration").ToString() == "9")
                                    {
                                        imgStart.Visibility = Visibility.Collapsed;
                                        imgPrintStart.Visibility = Visibility.Visible;
                                        imgPrintPause.Visibility = Visibility.Collapsed;
                                        imgPrintResume.Visibility = Visibility.Collapsed;
                                        imgPrintFinish.Visibility = Visibility.Collapsed;
                                        imgPrintStop.Visibility = Visibility.Visible;
                                    }
                                    //printFinish
                                    else if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.SubEnumeration").ToString() == "20")
                                    {
                                        imgStart.Visibility = Visibility.Collapsed;
                                        imgPrintStart.Visibility = Visibility.Collapsed;
                                        imgPrintPause.Visibility = Visibility.Collapsed;
                                        imgPrintResume.Visibility = Visibility.Collapsed;
                                        imgPrintFinish.Visibility = Visibility.Visible;
                                        imgPrintStop.Visibility = Visibility.Visible;
                                    }
                                    //pause
                                    else
                                    {
                                        imgStart.Visibility = Visibility.Collapsed;
                                        imgPrintStart.Visibility = Visibility.Collapsed;
                                        imgPrintPause.Visibility = Visibility.Visible;
                                        imgPrintResume.Visibility = Visibility.Collapsed;
                                        imgPrintFinish.Visibility = Visibility.Collapsed;
                                        imgPrintStop.Visibility = Visibility.Visible;
                                    }
                                }
                                //resume
                                else if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bPause").ToString() == "True")
                                {
                                    imgStart.Visibility = Visibility.Collapsed;
                                    imgPrintStart.Visibility = Visibility.Collapsed;
                                    imgPrintPause.Visibility = Visibility.Collapsed;
                                    imgPrintResume.Visibility = Visibility.Visible;
                                    imgPrintFinish.Visibility = Visibility.Collapsed;
                                    imgPrintStop.Visibility = Visibility.Visible;
                                }

                                #endregion

                                #region LED

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed1On").ToString() == "True")
                                {
                                    _ucLED.imgLightL1.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                    _ucLED.imgLightL2.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                    _ucLED.imgLightL3.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                }
                                else
                                {
                                    _ucLED.imgLightL1.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                    _ucLED.imgLightL2.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                    _ucLED.imgLightL3.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                                }

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed2On").ToString() == "True")
                                {
                                    _ucLED.imgLightR1.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                    _ucLED.imgLightR2.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                    _ucLED.imgLightR3.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                }
                                else
                                {
                                    _ucLED.imgLightR1.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                    _ucLED.imgLightR2.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                    _ucLED.imgLightR3.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                                }
                                #endregion

                                #region Door 센서 없음
                                /*
                                //Front
                                if (d.OPCItemValueTextBoxes[480] == "True")
                                {
                                    var uriDoorFront = new Uri(@"/ImgTab/1_home/chamber door/lock_on.png", UriKind.Relative);
                                    DoorSensor.imgFrontDoor.Source = new BitmapImage(uriDoorFront);
                                }
                                else
                                {
                                    var uriDoorFront = new Uri(@"/ImgTab/1_home/chamber door/unlock_on.png", UriKind.Relative);
                                    DoorSensor.imgFrontDoor.Source = new BitmapImage(uriDoorFront);
                                }

                                //Rear
                                if (d.OPCItemValueTextBoxes[481] == "True")
                                {
                                    var uriDoorRear = new Uri(@"/ImgTab/1_home/chamber door/lock_on.png", UriKind.Relative);
                                    DoorSensor.imgRearDoor.Source = new BitmapImage(uriDoorRear);
                                }
                                else
                                {
                                    var uriDoorRear = new Uri(@"/ImgTab/1_home/chamber door/unlock_on.png", UriKind.Relative);
                                    DoorSensor.imgRearDoor.Source = new BitmapImage(uriDoorRear);
                                }
                                */
                                #endregion 

                                #region Homing 값 로드
                                /* X */
                                Vel_X.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeVelocity").ToString();
                                Acc_X.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeAcceleration").ToString();
                                Dec_X.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeDeceleration").ToString();
                                Dis_X.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeDistance").ToString();
                                TP_X.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomePosition").ToString();
                                Dir_X.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.nHomeMode").ToString();
                                Order_X.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.nHomeOrder").ToString();

                                /* R */
                                Vel_R.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeVelocity").ToString();
                                Acc_R.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeAcceleration").ToString();
                                Dec_R.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeDeceleration").ToString();
                                Dis_R.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeDistance").ToString();
                                TP_R.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomePosition").ToString();
                                Dir_R.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.nHomeMode").ToString();
                                Order_R.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.nHomeOrder").ToString();

                                /* Z */
                                Vel_Z.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeVelocity").ToString();
                                Acc_Z.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeAcceleration").ToString();
                                Dec_Z.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeDeceleration").ToString();
                                Dis_Z.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeDistance").ToString();
                                TP_Z.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomePosition").ToString();
                                Dir_Z.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.nHomeMode").ToString();
                                Order_Z.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.nHomeOrder").ToString();

                                /* S */
                                Vel_S.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeVelocity").ToString();
                                Acc_S.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeAcceleration").ToString();
                                Dec_S.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeDeceleration").ToString();
                                Dis_S.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeDistance").ToString();
                                TP_S.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomePosition").ToString();
                                Dir_S.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.nHomeMode").ToString();
                                Order_S.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.nHomeOrder").ToString();
                                #endregion

                                #region M270-Temperature,Humity
                                //double tempTemper = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[31]));
                                //d.IntTemperature = Convert.ToInt32(tempTemper);
                                //d.StrTemperature = Convert.ToString(d.IntTemperature) + "°C";

                                //double tempHumidity = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[85]));
                                //d.IntHumity = Convert.ToInt32(tempHumidity);
                                //d.StrHumity = Convert.ToString(d.IntHumity) + "°C";

                                //double tempDewPoint = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[70]));
                                //d.IntDewPoint = Convert.ToInt32(tempDewPoint);
                                //d.StrDewPoint = Convert.ToString(d.IntHumity) + "°C";

                                //double tempPressure = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[71]));
                                //d.IntPressure = Convert.ToInt32(tempPressure);
                                //d.StrPressure = Convert.ToString(d.IntPressure);
                                #endregion

                                #region M270-LASER
                                //if (d.OPCItemValueTextBoxes[6] != tempLaser)
                                //{
                                //    _ucM270Laser.IMGlaser_on.Visibility = Visibility.Collapsed;
                                //    _ucM270Laser.IMGlaser_Power.Visibility = Visibility.Collapsed;

                                //    tempLaser = d.OPCItemValueTextBoxes[6];

                                //    Visibility laser_Visibility = Visibility.Collapsed;

                                //    Uri uriSourceLaser = new Uri(@"/imgTab/1_home/laser/power_btn.png", UriKind.Relative);
                                //    if (tempLaser == "False")
                                //    {
                                //        laser_Visibility = Visibility.Collapsed;
                                //        uriSourceLaser = new Uri(@"/imgTab/1_home/laser/power_btn.png", UriKind.Relative);
                                //    }
                                //    else
                                //    {
                                //        laser_Visibility = Visibility.Visible;
                                //        uriSourceLaser = new Uri(@"/imgTab/1_home/laser/power on_btn.png", UriKind.Relative);
                                //    }

                                //    _ucM270Laser.IMGlaser_on.Visibility = laser_Visibility;
                                //    _ucM270Laser.IMGlaser_Power.Visibility = laser_Visibility;

                                //    btn_laser_power.Source = new BitmapImage(uriSourceLaser);
                                //}


                                //if (d.OPCItemValueTextBoxes[8] != tempGuideBeam)
                                //{
                                //    _ucM270Laser.IMGlaser_on.Visibility = Visibility.Collapsed;
                                //    _ucM270Laser.IMGlaser_Power.Visibility = Visibility.Collapsed;

                                //    tempGuideBeam = d.OPCItemValueTextBoxes[8];

                                //    Visibility laser_Visibility = Visibility.Collapsed;

                                //    Uri uriSourceGuideBeam = new Uri(@"/imgTab/1_home/laser/laser_btn.png", UriKind.Relative);
                                //    if (tempGuideBeam == "False")
                                //    {
                                //        laser_Visibility = Visibility.Collapsed;
                                //        uriSourceGuideBeam = new Uri(@"/imgTab/1_home/laser/laser_btn.png", UriKind.Relative);
                                //    }
                                //    else
                                //    {
                                //        laser_Visibility = Visibility.Visible;
                                //        uriSourceGuideBeam = new Uri(@"/imgTab/1_home/laser/laser on_btn.png", UriKind.Relative);

                                //    }

                                //    _ucM270Laser.IMGlaser_on.Visibility = laser_Visibility;
                                //    _ucM270Laser.IMGlaser_Power.Visibility = laser_Visibility;

                                //    btn_Guide_Beam.Source = new BitmapImage(uriSourceGuideBeam);

                                //}

                                #endregion

                                ////레이저 4개
                                #region 레이저 실시간 파워

                                ////Front_Left
                                //if (d.OPCItemValueTextBoxes[6] == "True")
                                //{
                                //    double OPCItemValueTextBoxes = Convert.ToDouble(dv.OPCItemValueTextBoxes[7]);
                                //    double W = 0;

                                //    double[] Rise = {1.5, 1.7, 1.9, 2.1, 2.3, 2.5, 2.7, 2.9, 3.1, 3.3, 3.5, 3.7, 3.9, 4.1, 4.3, 4.5, 4.7, 4.9, 5.1, 5.3, 5.5, 5.7, 5.9, 6.1,
                                //        6.3, 6.5, 6.7, 6.9, 7.1, 7.3, 7.5, 7.7, 7.9, 8.1, 8.3, 8.5, 8.7, 8.9, 9.1, 9.3, 9.5, 9.7, 9.9, 10};

                                //    double[] arr_X = {50, 49.5, 49.5, 50, 50, 52.5, 50, 49.5, 49.5, 50.5, 49.5, 51, 50, 49, 50, 49, 50, 49.5, 50, 49.5, 50, 48.5, 50, 49.5, 48.5, 48, 49.5, 47, 48,
                                //     48.5, 46, 48, 46.5, 47, 46.5, 46, 46.5, 47.5, 49, 40.5, 30};

                                //    double[] arr_Y = {-40.6, -39.75, -39.75, -40.8, -40.8, -47.05, -40.3, -38.85, -38.85, -42.15, -38.65, -44.2, -40.3, -36.2, -40.5, -36, -40.7, -38.25, -40.8, -38.15, -40.9,
                                //     -32.35, -41.2, -38.15, -35, -38.25, -31.55, -28.1, -38.75, -20.5, -28, -31.85, -12.1, -28.3, -15.85, -20.1, -15.75, -11.3, -15.85, -25.15, -39.4, 43.05, 147};

                                //    if (OPCItemValueTextBoxes >= 1.5 && OPCItemValueTextBoxes < 10)
                                //    {
                                //        for (int i = 0; i < Rise.Length; i++)
                                //        {
                                //            if (OPCItemValueTextBoxes >= Rise[i] && OPCItemValueTextBoxes < Rise[i + 1])
                                //            {
                                //                W = (OPCItemValueTextBoxes * arr_X[i]) + arr_Y[i];

                                //                W = Math.Round(W, 2);
                                //                FrontLeft_Laser_PowerW.Text = W.ToString("N1") + "W";
                                //            }
                                //        }
                                //    }
                                //    else if (OPCItemValueTextBoxes == 10)
                                //    {

                                //        FrontLeft_Laser_PowerW.Text = "447W";
                                //    }
                                //    else
                                //    {
                                //        FrontLeft_Laser_PowerW.Text = "0W";
                                //    }
                                //}
                                //else
                                //{
                                //    FrontLeft_Laser_PowerW.Text = "0W";
                                //}

                                ////Front_Right
                                //if (d.OPCItemValueTextBoxes[30] == "True")
                                //{
                                //    double OPCItemValueTextBoxes = Convert.ToDouble(dv.OPCItemValueTextBoxes[31]);
                                //    double W = 0;

                                //    double[] Rise = {1.5, 1.7, 1.9, 2.1, 2.3, 2.5, 2.7, 2.9, 3.1, 3.3, 3.5, 3.7, 3.9, 4.1, 4.3, 4.5, 4.7, 4.9, 5.1, 5.3, 5.5, 5.7, 5.9, 6.1,
                                //        6.3, 6.5, 6.7, 6.9, 7.1, 7.3, 7.5, 7.7, 7.9, 8.1, 8.3, 8.5, 8.7, 8.9, 9.1, 9.3, 9.5, 9.7, 9.9, 10};

                                //    double[] arr_X = {50, 49.5, 49.5, 50, 50, 52.5, 50, 49.5, 49.5, 50.5, 49.5, 51, 50, 49, 50, 49, 50, 49.5, 50, 49.5, 50, 48.5, 50, 49.5, 48.5, 48, 49.5, 47, 48,
                                //     48.5, 46, 48, 46.5, 47, 46.5, 46, 46.5, 47.5, 49, 40.5, 30};

                                //    double[] arr_Y = {-40.6, -39.75, -39.75, -40.8, -40.8, -47.05, -40.3, -38.85, -38.85, -42.15, -38.65, -44.2, -40.3, -36.2, -40.5, -36, -40.7, -38.25, -40.8, -38.15, -40.9,
                                //     -32.35, -41.2, -38.15, -35, -38.25, -31.55, -28.1, -38.75, -20.5, -28, -31.85, -12.1, -28.3, -15.85, -20.1, -15.75, -11.3, -15.85, -25.15, -39.4, 43.05, 147};

                                //    if (OPCItemValueTextBoxes >= 1.5 && OPCItemValueTextBoxes < 10)
                                //    {
                                //        for (int i = 0; i < Rise.Length; i++)
                                //        {
                                //            if (OPCItemValueTextBoxes >= Rise[i] && OPCItemValueTextBoxes < Rise[i + 1])
                                //            {
                                //                W = (OPCItemValueTextBoxes * arr_X[i]) + arr_Y[i];

                                //                W = Math.Round(W, 2);
                                //                FrontLeft_Laser_PowerW.Text = W.ToString("N1") + "W";
                                //            }
                                //        }
                                //    }
                                //    else if (OPCItemValueTextBoxes == 10)
                                //    {

                                //        FrontLeft_Laser_PowerW.Text = "447W";
                                //    }
                                //    else
                                //    {
                                //        FrontLeft_Laser_PowerW.Text = "0W";
                                //    }
                                //}
                                //else
                                //{
                                //    FrontLeft_Laser_PowerW.Text = "0W";
                                //}

                                ////Back_Left
                                //if (d.OPCItemValueTextBoxes[34] == "True")
                                //{
                                //    double OPCItemValueTextBoxes = Convert.ToDouble(dv.OPCItemValueTextBoxes[35]);
                                //    double W = 0;

                                //    double[] Rise = {1.5, 1.7, 1.9, 2.1, 2.3, 2.5, 2.7, 2.9, 3.1, 3.3, 3.5, 3.7, 3.9, 4.1, 4.3, 4.5, 4.7, 4.9, 5.1, 5.3, 5.5, 5.7, 5.9, 6.1,
                                //        6.3, 6.5, 6.7, 6.9, 7.1, 7.3, 7.5, 7.7, 7.9, 8.1, 8.3, 8.5, 8.7, 8.9, 9.1, 9.3, 9.5, 9.7, 9.9, 10};

                                //    double[] arr_X = {50, 49.5, 49.5, 50, 50, 52.5, 50, 49.5, 49.5, 50.5, 49.5, 51, 50, 49, 50, 49, 50, 49.5, 50, 49.5, 50, 48.5, 50, 49.5, 48.5, 48, 49.5, 47, 48,
                                //     48.5, 46, 48, 46.5, 47, 46.5, 46, 46.5, 47.5, 49, 40.5, 30};

                                //    double[] arr_Y = {-40.6, -39.75, -39.75, -40.8, -40.8, -47.05, -40.3, -38.85, -38.85, -42.15, -38.65, -44.2, -40.3, -36.2, -40.5, -36, -40.7, -38.25, -40.8, -38.15, -40.9,
                                //     -32.35, -41.2, -38.15, -35, -38.25, -31.55, -28.1, -38.75, -20.5, -28, -31.85, -12.1, -28.3, -15.85, -20.1, -15.75, -11.3, -15.85, -25.15, -39.4, 43.05, 147};

                                //    if (OPCItemValueTextBoxes >= 1.5 && OPCItemValueTextBoxes < 10)
                                //    {
                                //        for (int i = 0; i < Rise.Length; i++)
                                //        {
                                //            if (OPCItemValueTextBoxes >= Rise[i] && OPCItemValueTextBoxes < Rise[i + 1])
                                //            {
                                //                W = (OPCItemValueTextBoxes * arr_X[i]) + arr_Y[i];

                                //                W = Math.Round(W, 2);
                                //                FrontLeft_Laser_PowerW.Text = W.ToString("N1") + "W";
                                //            }
                                //        }
                                //    }
                                //    else if (OPCItemValueTextBoxes == 10)
                                //    {

                                //        FrontLeft_Laser_PowerW.Text = "447W";
                                //    }
                                //    else
                                //    {
                                //        FrontLeft_Laser_PowerW.Text = "0W";
                                //    }
                                //}
                                //else
                                //{
                                //    FrontLeft_Laser_PowerW.Text = "0W";
                                //}

                                ////Back_Right
                                //if (d.OPCItemValueTextBoxes[45] == "True")
                                //{
                                //    double OPCItemValueTextBoxes = Convert.ToDouble(dv.OPCItemValueTextBoxes[46]);
                                //    double W = 0;

                                //    double[] Rise = {1.5, 1.7, 1.9, 2.1, 2.3, 2.5, 2.7, 2.9, 3.1, 3.3, 3.5, 3.7, 3.9, 4.1, 4.3, 4.5, 4.7, 4.9, 5.1, 5.3, 5.5, 5.7, 5.9, 6.1,
                                //        6.3, 6.5, 6.7, 6.9, 7.1, 7.3, 7.5, 7.7, 7.9, 8.1, 8.3, 8.5, 8.7, 8.9, 9.1, 9.3, 9.5, 9.7, 9.9, 10};

                                //    double[] arr_X = {50, 49.5, 49.5, 50, 50, 52.5, 50, 49.5, 49.5, 50.5, 49.5, 51, 50, 49, 50, 49, 50, 49.5, 50, 49.5, 50, 48.5, 50, 49.5, 48.5, 48, 49.5, 47, 48,
                                //     48.5, 46, 48, 46.5, 47, 46.5, 46, 46.5, 47.5, 49, 40.5, 30};

                                //    double[] arr_Y = {-40.6, -39.75, -39.75, -40.8, -40.8, -47.05, -40.3, -38.85, -38.85, -42.15, -38.65, -44.2, -40.3, -36.2, -40.5, -36, -40.7, -38.25, -40.8, -38.15, -40.9,
                                //     -32.35, -41.2, -38.15, -35, -38.25, -31.55, -28.1, -38.75, -20.5, -28, -31.85, -12.1, -28.3, -15.85, -20.1, -15.75, -11.3, -15.85, -25.15, -39.4, 43.05, 147};

                                //    if (OPCItemValueTextBoxes >= 1.5 && OPCItemValueTextBoxes < 10)
                                //    {
                                //        for (int i = 0; i < Rise.Length; i++)
                                //        {
                                //            if (OPCItemValueTextBoxes >= Rise[i] && OPCItemValueTextBoxes < Rise[i + 1])
                                //            {
                                //                W = (OPCItemValueTextBoxes * arr_X[i]) + arr_Y[i];

                                //                W = Math.Round(W, 2);
                                //                FrontLeft_Laser_PowerW.Text = W.ToString("N1") + "W";
                                //            }
                                //        }
                                //    }
                                //    else if (OPCItemValueTextBoxes == 10)
                                //    {

                                //        FrontLeft_Laser_PowerW.Text = "447W";
                                //    }
                                //    else
                                //    {
                                //        FrontLeft_Laser_PowerW.Text = "0W";
                                //    }
                                //}
                                //else
                                //{
                                //    FrontLeft_Laser_PowerW.Text = "0W";
                                //}
                                #endregion

                                #region M270-BuildTimeEst
                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bHomeCalibration").ToString() == "True")
                                {
                                    timeChnage = 0;
                                }
                                else
                                {
                                    timeChnage = Convert.ToDouble(d.OPCItemValueTextBoxes[87]);
                                }

                                TimeSpan currentTime = TimeSpan.FromSeconds(timeChnage);
                                int stimehour = 0;
                                string sstrTimehour = "0";
                                if (currentTime.Days >= 1)
                                {
                                    stimehour = (currentTime.Days * 24) + currentTime.Hours;
                                    sstrTimehour = Convert.ToString(stimehour);
                                }
                                else
                                {
                                    sstrTimehour = currentTime.Hours.ToString("00");
                                }
                                psCurrentTime = sstrTimehour + "h " + ":" + " " +
                                currentTime.Minutes.ToString("00") + "m " + ":" + " " +
                                currentTime.Seconds.ToString("00") + "s ";
                                lbltime.Text = psCurrentTime;

                                if (Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeVelocity").ToString()) != timeChnage && ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bHomeCalibration").ToString() == "4" || (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bHomeCalibration").ToString() == "5"))
                                {
                                    if (_timerPrintingRunning == false)
                                    {
                                        _timerPrintingRunning = true;
                                        ps_StartTime = DateTime.Now;
                                    }
                                }
                                #endregion

                                #region PowderSupply, 수정필요, 뭐지
                                if (tempPowderSupply != Convert.ToDouble(dv.OPCItemValueTextBoxes[86]))
                                {
                                    tempPowderSupply = Convert.ToDouble(dv.OPCItemValueTextBoxes[86]);
                                    dv.DblPowderSupply4 = Convert.ToString(tempPowderSupply);
                                }

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bAdditivePos").ToString() == "True")//파우더 분말공급통 상단 센서가 켜졌을 때
                                {
                                    powder_Light02.Visibility = Visibility.Visible;
                                    pSupplyTop.Visibility = Visibility.Visible;
                                    powder_Light02_black.Visibility = Visibility.Collapsed;
                                    pSupplyTop_black.Visibility = Visibility.Collapsed;

                                }
                                else if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bAdditivePos").ToString() == "False")
                                {
                                    powder_Light02.Visibility = Visibility.Collapsed;
                                    pSupplyTop.Visibility = Visibility.Collapsed;
                                    powder_Light02_black.Visibility = Visibility.Visible;
                                    pSupplyTop_black.Visibility = Visibility.Visible;

                                }

                                if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bCyclicPos").ToString() == "True")//파우더 분말공급통 하단 센서가 켜졌을 때
                                {
                                    powder_Light01.Visibility = Visibility.Visible;
                                    pSupplyUnder.Visibility = Visibility.Visible;
                                    powder_Light01_black.Visibility = Visibility.Collapsed;
                                    pSupplyUnder_black.Visibility = Visibility.Collapsed;
                                }
                                else if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bCyclicPos").ToString() == "False")
                                {
                                    powder_Light01.Visibility = Visibility.Collapsed;
                                    pSupplyUnder.Visibility = Visibility.Collapsed;
                                    powder_Light01_black.Visibility = Visibility.Visible;
                                    pSupplyUnder_black.Visibility = Visibility.Visible;
                                }
                                #endregion
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            });
        }

        //=====================================================================================

        private void AirPressureSwitch() //조그 맞나?
        {

            if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogNeg").ToString() == "False")
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogNeg", true);
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogNeg", false);
            }
        }

        private void AutoSupplySwitch() // 디스턴스 맞나?
        {

            if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.nDistance").ToString() == "False")
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bCyclicPos", true);
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bCyclicPos", false);
            }
        }

        #region M270-LineBackgroundworker, 작업 완료, 진행중 확인필요

        private void linethread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            lvFile.IsEnabled = true;

            progress.Visibility = Visibility.Collapsed;
            aniLoading.Visibility = Visibility.Collapsed;

            showlayer = 0;
            txtCurrentLayer.Text = Convert.ToString(showlayer);

            polyInline.Data = null;
            StringBuilder strbuilder = new StringBuilder();
            StringBuilder strbuilder3 = new StringBuilder();
            int showlayercount = 0;
            for (int i = 0; i < allFile.Count; i++)
            {
                strbuilder3.Append(_stringBuilder3[showlayercount]);
                strbuilder.Append(_stringBuilder[showlayercount]);
                showlayercount = showlayercount + allmodelLayer;
            }
            polyOutline.Data = Geometry.Parse("" + strbuilder3);
            polyInline.Data = Geometry.Parse("" + strbuilder);

            slLayer1.Maximum = Convert.ToInt32(allmodelLayer);
            slLayer1.Minimum = 0;
            txtTotalLayer.Text = Convert.ToString(allmodelLayer);

            int tempNumber = lvFile.SelectedIndex;
            lvFile.SelectedIndex = tempNumber;
            fileSize = 0;
        }

        private void linethread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int value = e.ProgressPercentage;

            // 변경 값으로 갱신
            progress.Value = value;

        }

        private void linethread_DoWork(object sender, DoWorkEventArgs e)
        {
            
            BackgroundWorker worker = sender as BackgroundWorker;
            double tempFileSize = 0;
            _facts.Clear();
            allFile.Clear();
            filePath = @"C:\\MYD_METAL\\" + adada + "\\" + adada + ".job";
            List<GeometryGroup> geoGroup = new List<GeometryGroup>();

            int lastCount = filePath.LastIndexOf("\\");
            sss = filePath.Substring(0, lastCount);

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sss);
            System.IO.FileInfo[] fi = di.GetFiles("*.bin");
            if (fi.Length == 0)
            {

            }
            else
            {
                string s = "";

                for (int i = 0; i < fi.Length; i++)
                {
                    s = fi[i].Name.ToString();
                    allFile.Add(s);
                    string attachFile = sss + "\\" + s;

                    Stream inStream = new FileStream(attachFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var reader = new BinaryReader(inStream);

                    UInt32 nHeader = ReadUInt32(reader);
                    UInt32 nHeaderSize = ReadUInt32(reader);
                    UInt32 nFixedHeader = ReadUInt32(reader);
                    UInt32 nFixedHeaderSize = ReadUInt32(reader);
                    this.Header = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(Convert.ToInt32(nFixedHeaderSize))).Trim();

                    UInt32 nVersionInfo = ReadUInt32(reader);
                    UInt32 nVersionSize = ReadUInt32(reader);
                    UInt32 nVersionMajor = ReadUInt32(reader);
                    UInt32 nVersionMinor = ReadUInt32(reader);

                    UInt32 nVersionName = ReadUInt32(reader);
                    UInt32 nVersionNameSize = ReadUInt32(reader);
                    this.Header = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(Convert.ToInt32(nVersionNameSize))).Trim();

                    UInt32 nAllLayer = ReadUInt32(reader);
                    UInt32 nAllLayerSize = ReadUInt32(reader);
                    //await GetDataAll(reader1);
                    while (true)
                    {
                        if (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                        }
                        else
                        {
                            break;
                        }

                        UInt32 tempNum = ReadUInt32(reader);

                        // 레이어 시작
                        // 21 - 15 00 00 00 
                        if (tempNum == 21)
                        {
                            UInt32 nLayerInfoSize = ReadUInt32(reader); //79 00 00 00
                        }


                        else if (tempNum == 210)
                        {
                            //Facts f = new Facts();
                            //UInt32 nlayer = ReadUInt32(reader); //d2 00 00 00
                            UInt32 nlayerSize = ReadUInt32(reader); //04 00 00 00
                            float nlayerHeight = ReadFloat(reader); //0a d7 a3 bc
                            layerthickness = nlayerHeight;
                            StringBuilder strbuilder = new StringBuilder();
                            StringBuilder strbuilder3 = new StringBuilder();
                            int transValue = 0;

                            while (true)
                            {

                                if (reader.BaseStream.Position != reader.BaseStream.Length)
                                {

                                }
                                else
                                {
                                    break;
                                }

                                UInt32 check0 = ReadUInt32(reader);

                                //d4
                                if (check0 == 212)
                                {

                                    UInt32 datablockSize = ReadUInt32(reader);

                                    //2120
                                    UInt32 udatablockType = ReadUInt32(reader);
                                    UInt32 udatablockTypeSize = ReadUInt32(reader);
                                    var udatablockTypeNumber = reader.ReadBytes(1);
                                    dataNumber = Convert.ToString(udatablockTypeNumber[0]);

                                    // 2121 - 49 08 00 00
                                    UInt32 part_identi = ReadUInt32(reader);
                                    var part_identiSize = ReadUInt32(reader);
                                    var part_identiNumber = ReadUInt32(reader);

                                    // 2122 - 4a 08 00 00
                                    UInt32 com_identi = ReadUInt32(reader);
                                    var com_identiSize = ReadUInt32(reader);
                                    var com_identiNumber = ReadUInt32(reader);

                                    // 2123 - 4b 08 00 00
                                    UInt32 pro_identi = ReadUInt32(reader);
                                    var pro_identiSize = ReadUInt32(reader);
                                    var pro_identiNumber = ReadUInt32(reader);

                                    // 2124 - 4c 08 00 00
                                    UInt32 pro_identii = ReadUInt32(reader);


                                    if (dataNumber == "1") //내부
                                    {
                                        uint ucountSize = ReadUInt32(reader) / 8;
                                        //int uucount = Convert.ToInt32(ucountSize);

                                        for (int h = 0; h < ucountSize; h++)
                                        {
                                            float pointX = ReadFloat(reader);
                                            float pointY = ReadFloat(reader);

                                            if (transValue == 1)
                                            {

                                                strbuilder.Append("M " + Convert.ToString(beforeX1 * 8.5) + "," + Convert.ToString(beforeY1 * 8.5) + " " + Convert.ToString(pointX * 8.5) + "," + Convert.ToString(pointY * 8.5));
                                                transValue = 0;
                                            }
                                            else
                                            {
                                                beforeX1 = pointX;
                                                beforeY1 = pointY;
                                                transValue = 1;
                                            }

                                        }
                                    }
                                    else if (dataNumber == "3") //외부
                                    {

                                        uint ucountSize = ReadUInt32(reader);

                                        while (true)
                                        {
                                            uint ucount = ReadUInt32(reader);

                                            tempvalue2 = 3;
                                            if (ucount == 0)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                strbuilder3.Append("M ");

                                                for (int d = 0; d < ucount; d++)
                                                {
                                                    float pointX = ReadFloat(reader);
                                                    float pointY = ReadFloat(reader);
                                                    strbuilder3.Append(" " + Convert.ToString(pointX * 8.5) + "," + Convert.ToString(pointY * 8.5) + " ");
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("여기는 데이터블럭이 없습니다.");
                                    }
                                }
                                else//(check0==)
                                {
                                    uint ucountSize = ReadUInt32(reader);
                                    //_facts.Add(f);
                                    break;
                                }
                            }

                            worker.ReportProgress(Convert.ToInt32(reader.BaseStream.Position + tempFileSize));
                            _stringBuilder.Add(strbuilder);
                            _stringBuilder3.Add(strbuilder3);
                            layercount = layercount + 1;
                        }

                        else
                        {
                            uint ucont = ReadUInt32(reader);
                            MessageBox.Show("여기는 데이터가 없습니다.");
                            break;
                        }
                    }
                    allmodelLayer = layercount;
                    layercount = 0;
                    allmodelingCount++;

                    inStream.Close();
                    tempFileSize += _totalFileSize[i];
                    Console.WriteLine("파일내용 정리 완료" + Convert.ToString(i));
                }
            }
        }

        #endregion

        //errorcheckWindow
        private void ErrorWindow1()
        {
            me = new motorError();
            me.initalSetting(d, 1, this);
            me.ShowDialog();
            me.Topmost = true;
        }
        private void ErrorWindow2()
        {
            me = new motorError();
            me.initalSetting(d, 2, this);
            me.ShowDialog();
            me.Topmost = true;
        }
        private void ErrorWindow3()
        {
            me = new motorError();
            me.initalSetting(d, 3, this);
            me.ShowDialog();
            me.Topmost = true;
        }
        //500 Alram Image
        private void AlramImageBtn()
        {
            if (alramPoint == 1 && (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.Machine.nId").ToString() != "0")
            {
                ErrorWindow1();
            }
            if (alramPoint == 1 && (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.bInError").ToString() == "True")
            {
                ErrorWindow2();
            }
            if (alramPoint == 1 && (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.Information.nId").ToString() != "0")
            {
                ErrorWindow3();
            }
        }
        private void PowderSupplyRatio() // bool 이 드가있음. 왜지?
        {
            string bHome_Zco = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2);
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bHome", bHome_Zco);
            psupply.Close();
        }


        private void PowderSupplyWindow1()
        {
            string tempPowder = Convert.ToString(d.DblPowderSupply1);
            int dbltempPowder = Convert.ToInt32(tempPowder);
            pCheckNumber = 1;

            if (dbltempPowder < 10)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = 0;
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(0, 1));
            }
            else if (dbltempPowder < 100)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }
            else if (dbltempPowder < 1000)
            {
                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(3, 1));
            }

            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, 1);
            psupply.Show();
            psupply.Topmost = true;
        }

        private void PowderSupplyWindow2()
        {
            string tempPowder = Convert.ToString(d.DblPowderSupply2);
            int dbltempPowder = Convert.ToInt32(tempPowder);
            pCheckNumber = 2;

            if (dbltempPowder < 10)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = 0;
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(0, 1));
            }
            else if (dbltempPowder < 100)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }
            else if (dbltempPowder < 1000)
            {
                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(3, 1));
            }

            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, 2);
            psupply.Show();
            psupply.Topmost = true;
        }

        private void PowderSupplyWindow3()
        {
            string tempPowder = Convert.ToString(d.DblPowderSupply3);
            int dbltempPowder = Convert.ToInt32(tempPowder);
            pCheckNumber = 3;

            if (dbltempPowder < 10)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = 0;
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(0, 1));
            }
            else if (dbltempPowder < 100)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }
            else if (dbltempPowder < 1000)
            {
                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(3, 1));
            }

            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, 3);
            psupply.Show();
            psupply.Topmost = true;

        }

        private void PowderSupplyWindow4()//파우더 공급비율을 눌렀을때
        {
            string tempPowder = string.Format("{0:0.0}", double.Parse(d.DblPowderSupply4));
            double dbltempPowder = Convert.ToDouble(tempPowder);
            pCheckNumber = 4;

            if (dbltempPowder < 1)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));

            }
            else if (dbltempPowder < 10)
            {

                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }

            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, 4);
            psupply.ShowDialog();

        }

        #region 5. Printing

        private void Start()
        {
            if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bReady").ToString() == "True")
            {
                try
                {
                    DispatcherTimerSample();

                    _timerPrintingRunning = false;
                    string strTime = DateTime.Now.ToString("yy.MM.dd HH-mm-ss");

                    if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bReady").ToString() == "True")
                    {

                        (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bAutoStart", true);
                        bteTimer.Start();

                        //imgStart.Visibility = Visibility.Collapsed;
                        //imgPrintStart.Visibility = Visibility.Visible;
                        //imgPrintStop.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Start : " + e.Message);
                }
                
            }
            else
            {
                //메시지박스 -> 이미지박스 변경 필요
                MessageBox.Show("Docking 확인 부탁드립니다");
            }
        }

        private void PrintStart()
        {
            if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.SubEnumeration").ToString() == "9")
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bPrintStart", true);

                //imgPrintStart.Visibility = Visibility.Collapsed;
                //imgPrintPause.Visibility = Visibility.Visible;
                //_ucCommonM270Motor.Visibility = Visibility.Collapsed;
            }
            
        }

        private void PrintPause()
        {
            if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bOperation").ToString() == "True")
            {
                bteTimer.Stop();
                _timerPrintingRunning = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bAutoPause", true);

                //imgPrintResume.Visibility = Visibility.Visible;
                //imgPrintPause.Visibility = Visibility.Collapsed;
                //_ucCommonM270Motor.Visibility = Visibility.Visible;
            }
        }

        private void PrintResume()
        {
            if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.bPause").ToString() == "True")
            {
                bteTimer.Start();
                _timerPrintingRunning = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bAutoPause", true);

                //imgPrintResume.Visibility = Visibility.Collapsed;
                //imgPrintPause.Visibility = Visibility.Visible;
                //_ucCommonM270Motor.Visibility = Visibility.Collapsed;
            }
        }

        private void PrintStop()
        {
            bteTimer.Stop();
            _timerPrintingRunning = false;

            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bStop", true);

            //imgPrintPause.Visibility = Visibility.Collapsed;
            //imgPrintStop.Visibility = Visibility.Collapsed;
            //imgPrintResume.Visibility = Visibility.Collapsed;
            //imgStart.Visibility = Visibility.Visible;
            //_ucCommonM270Motor.Visibility = Visibility.Visible;
        }

        private void PrintFinish()
        {
            if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Machine.State.SubEnumeration").ToString() == "20")
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bPrintFinsh", true);

                //imgStart.Visibility = Visibility.Visible;
                //imgPrintPause.Visibility = Visibility.Collapsed;
                //imgPrintStop.Visibility = Visibility.Collapsed;
                //imgPrintResume.Visibility = Visibility.Collapsed;
                //_ucCommonM270Motor.Visibility = Visibility.Visible;
            }
        }

        private void PrintFileDelete()
        {
            try
            {
                string tempFile = lvFile.SelectedItem.ToString();
                string _ftpUri = d.FtpName + "Job Files/" + tempFile;
                Uri ftpUri = new Uri(_ftpUri);

                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpUri);
                req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                req.Timeout = 120000;
                req.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse resFtp = (FtpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(resFtp.GetResponseStream(), System.Text.Encoding.Default, true);
                string strData = reader.ReadToEnd();
                string[] filesinDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                resFtp.Close();

                for (int i = 0; i < filesinDirectory.Count(); i++)
                {
                    string tempUri = d.FtpName + "Job Files/" + filesinDirectory[i];

                    FtpWebRequest _req = (FtpWebRequest)WebRequest.Create(tempUri);
                    _req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                    _req.Timeout = 120000;
                    _req.Method = WebRequestMethods.Ftp.DeleteFile;

                    FtpWebResponse _resFtp = (FtpWebResponse)_req.GetResponse();
                    _resFtp.Close();
                }

                string tempUriFolder = d.FtpName + "Job Files/" + tempFile;

                FtpWebRequest _reqFolder = (FtpWebRequest)WebRequest.Create(tempUriFolder);
                _reqFolder.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                _reqFolder.Timeout = 120000;
                _reqFolder.Method = WebRequestMethods.Ftp.RemoveDirectory;

                FtpWebResponse _resFtpFolder = (FtpWebResponse)_reqFolder.GetResponse();
                _resFtpFolder.Close();
                DownloadFile();

                lvFile.SelectedIndex = 0;

            }
            catch (Exception exa)
            {
                MessageBox.Show(exa.Message);
            }
        }

        DateTime StartTime;
        DispatcherTimer timer;
        private void DispatcherTimerSample()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            StartTime = DateTime.Now;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //lblTime.Content = "0";

            TimeSpan elapsed = DateTime.Now - StartTime;

            // Start with the days if greater than 0.
            string text = "";
            if (elapsed.Days > 0)
                text += elapsed.Days.ToString() + ".";

            // Convert milliseconds into tenths of seconds.
            int tenths = elapsed.Milliseconds / 100;

            // Compose the rest of the elapsed time.
            text +=
                elapsed.Hours.ToString("00") + "h " + ":" + " " +
                elapsed.Minutes.ToString("00") + "m " + ":" + " " +
                elapsed.Seconds.ToString("00") + "s ";
        }
        #endregion

        #region Powder 공급 배율
        private void PUp1()
        {
            d.Pnumber1++;
            if (d.Pnumber1 == 10)
            {
                d.Pnumber1 = 0;
            }

            funPResult();
        }

        private void PUp2()
        {
            d.Pnumber2++;
            if (d.Pnumber2 == 10)
            {
                d.Pnumber2 = 0;
            }

            funPResult();
        }

        private void PUp3()
        {
            d.Pnumber3++;
            if (d.Pnumber3 == 10)
            {
                d.Pnumber3 = 0;
            }

            funPResult();
        }

        private void PDown1()
        {
            d.Pnumber1--;
            if (d.Pnumber1 == -1)
            {
                d.Pnumber1 = 9;
            }

            funPResult();
        }
        private void PDown2()
        {
            d.Pnumber2--;
            if (d.Pnumber2 == -1)
            {
                d.Pnumber2 = 9;
            }

            funPResult();
        }
        private void PDown3()
        {
            d.Pnumber3--;
            if (d.Pnumber3 == -1)
            {
                d.Pnumber3 = 9;
            }

            funPResult();
        }

        private void funPResult()//파우더값이 변할때
        {
            if (pCheckNumber == 1)
            {
                d.DblPowderSupply1 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);

            }
            else if (pCheckNumber == 2)
            {
                d.DblPowderSupply2 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);

            }
            else if (pCheckNumber == 3)
            {
                d.DblPowderSupply3 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);

            }
            else if (pCheckNumber == 4)
            {
                d.DblPowderSupply4 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);

            }
        }
        #endregion


        #region 8. M270-CLOSE
        private void CloseWindows()
        {
            Login _login = new Login();
            _login.Close();
            this.Close();
        }
        #endregion

        #region 9 ~ 12 M270-FILE_SELECT
        int binFileCount = 0;
        int binDownCount = 0;
        private int f_download = 0;
        private int showlayer = 0;
        private long fileSize = 0;
        List<string> allFile = new List<string>();
        int allmodelingCount = 0;
        int allmodelLayer = 0;
        string printStr = "";
        public string adada = "";

        #region 9. M270-PRINTING-FILEARRAY
        private void PrintFileArray()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 10. M270-PRINTING-FILERESET
        private void PrintReset()
        {
            lvFile.SelectedIndex = 0;

            Uri ftpUri = new Uri(d.FtpName);
            WebClient wc = new WebClient();

            cd_FileLoading cdFileLoading = new cd_FileLoading();
            wc.Credentials = new NetworkCredential("ftpuser", "ftpuser");
            string[] strfile = cdFileLoading.GetFileList(d.FtpName);

            lvFile.ItemsSource = strfile;
        }
        #endregion

        #region 11. M270-PRINTING-FILESELET
        private bool bDelete = false;
        List<System.Windows.Shapes.Path> _path = new List<System.Windows.Shapes.Path>();
        private List<double> _totalFileSize = new List<double>();
        List<StringBuilder> _stringBuilder = new List<StringBuilder>();
        List<StringBuilder> _stringBuilder3 = new List<StringBuilder>();
        string[] strfile;
        int beforeIndex;

        private void lvFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (aniLoading.Visibility != Visibility.Visible)
            {
                if (bDelete == false)
                {
                    aniLoading.Visibility = Visibility.Visible;

                    _path.Clear();
                    binFileCount = 0;
                    binDownCount = 0;
                    f_download = 0;
                    allFile.Clear();

                    lvFile.IsEnabled = true;
                    _totalFileSize.Clear();
                    _stringBuilder.Clear();
                    _stringBuilder3.Clear();

                    int index = lvFile.SelectedIndex;
                    beforeIndex = lvFile.SelectedIndex;
                    string tempFile = lvFile.SelectedItem.ToString();
                    allmodelingCount = 0;
                    string strJobFile = tempFile + "\\"+ tempFile + ".job";
                    (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Param.strJobFile", strJobFile);
                    //d.OPCItemWriteValueTextBoxes[229] = "CAE33531-0D70-4FF2-A461-363D292376B7";
                    //d.opcWrite("OPCItemSyncWrite229", daServerMgt);
                    Thread.Sleep(500);
                    (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bAutoParamApply", true);
                    showlayer = 0;
                    slLayer1.Value = 0;

                    string sDirPath = @"C:\\MYD_METAL\\" + tempFile;
                    string path = @"C:\\MYD_METAL\\";
                    Directory.Delete(path, true);
                    DirectoryInfo di = new DirectoryInfo(sDirPath);
                    printStr = tempFile;
                    if (di.Exists == false)
                    {
                        di.Create();
                        progress.Visibility = Visibility.Visible;
                        aniLoading.logingText.Text = "Calculating...";
                        cd_FileLoading cdFileLoading = new cd_FileLoading();
                        strfile = cdFileLoading.GetFileList1(tempFile, d.FtpName);
                        
                        binFileCount = strfile.Length - 2;
                        for (int i = 0; i < strfile.Length; i++)
                        {
                            if (strfile[i].Contains(".bin"))
                            {
                                string tempPath = tempFile + '/' + strfile[i];
                                string sDirPath1 = "C:/MYD_METAL/" + tempFile;
                                Download("Job Files/" + tempFile, strfile[i], sDirPath1, progress, true);
                                //loadingtheFile(tempFile);
                                allFile.Add(strfile[i]);
                                string attachFile = sDirPath1 + strfile[i];
                                adada = tempFile;
                            }
                        }
                        
                    }
                    else
                    {
                        
                        long length = Directory.GetFiles(sDirPath, "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));
                        aniLoading.logingText.Text = "Loading...";
                        progress.Visibility = Visibility.Visible;
                        double tempsize = DirSize(di);
                        progress.Maximum = tempsize;
                        adada = tempFile;
                        linethread.RunWorkerAsync();
                    }
                }
                else
                {

                }
            }
            else
            {
                lvFile.SelectedIndex = beforeIndex;
            }
        }

        private double DirSize(DirectoryInfo d)
        {
            double size = 0;

            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
                _totalFileSize.Add(size);
            }
            return size;
        }
        #endregion

        #region 12. M270-PRINTING-PROGRESSBAR
        private async void Download(string ftpDirectoryName, string downFileName, string localPath, ProgressBar progressBar, bool showCompleted)
        {
            try
            {
                binDownCount++;

                Uri ftpUri = new Uri(d.FtpName + ftpDirectoryName + "/" + downFileName);

                // 파일 사이즈
                FtpWebRequest reqFtp = (FtpWebRequest)WebRequest.Create(ftpUri);
                reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFtp.Credentials = new NetworkCredential("ftpuser", "ftpuser");

                WebResponse resFtp = await (Task<WebResponse>)reqFtp.GetResponseAsync();
                fileSize = resFtp.ContentLength;
                totalFileSize += fileSize;
                _totalFileSize.Add(fileSize);
                progress.Maximum = totalFileSize;

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                    request.DownloadProgressChanged += request_DownloadProgressChanged;

                    // 다운로드가 완료 된 후 메시지 보이기
                    if (showCompleted)
                    {
                        //loadingtheFile(adada);
                        request.DownloadFileCompleted += request_DownloadFileCompleted;
                        resFtp.Close();
                    }
                    // 다운로드 시작
                    request.DownloadFileAsync(ftpUri, @localPath + "/" + downFileName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        void request_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Value = Convert.ToInt32(Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(fileSize) * 100);
        }

        void request_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            f_download++;
            
            if (f_download == binFileCount)
            {
                
                progress.Maximum = totalFileSize;
                aniLoading.logingText.Text = "Loading...";
                linethread.RunWorkerAsync();
                Console.WriteLine(totalFileSize);

            }

        }
        #endregion

        #region 13. M270-PRINTING-OTHER
        public void testFunction()
        {
            var mmm = loadingtheFile(adada);
            testtest();
        }

        private void testtest()
        {
            loading();
            slLayer1.Maximum = Convert.ToInt32(allmodelLayer);
            txtTotalLayer.Text = Convert.ToString(allmodelLayer);
            lvFile.IsEnabled = true;

        }

        private int loadingtheFile(string _tempFile)
        {
            _facts.Clear();
            allFile.Clear();

            filePath = @"C:\\MYD_METAL\\" + _tempFile + "\\" + _tempFile + ".job";

            int lastCount = filePath.LastIndexOf("\\");
            sss = filePath.Substring(0, lastCount);

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sss);
            System.IO.FileInfo[] fi = di.GetFiles("*.bin");
            if (fi.Length == 0)
            {

            }
            else
            {
                string s = "";
                for (int i = 0; i < fi.Length; i++)
                {
                    s = fi[i].Name.ToString();
                    allFile.Add(s);
                    string attachFile = sss + "\\" + s;

                    Stream inStream = new FileStream(attachFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var reader1 = new BinaryReader(inStream);

                    GetData(reader1);
                    inStream.Close();
                }
            }

            int d = 0;
            return d;
        }
        #endregion

        #endregion

        #region 15. M270-LAYERCHANGE
        private void slLayer1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {

                showlayer = Convert.ToInt32(slLayer1.Value);
                txtCurrentLayer.Text = Convert.ToString(showlayer);
                if (d.StrPrintStaus != "출력전")
                {
                    polyInline.Data = null;
                    StringBuilder strbuilder = new StringBuilder();
                    StringBuilder strbuilder3 = new StringBuilder();
                    int showlayercount = showlayer;
                    for (int i = 0; i < allFile.Count; i++)
                    {
                        strbuilder3.Append(_stringBuilder3[showlayercount]);
                        strbuilder.Append(_stringBuilder[showlayercount]);
                        showlayercount = showlayercount + allmodelLayer;
                    }
                    polyInline.Data = Geometry.Parse("" + strbuilder);
                    polyOutline.Data = Geometry.Parse("" + strbuilder3);
                }
                else
                {
                    polyInline.Data = null;
                    StringBuilder strbuilder3 = new StringBuilder();
                    int showlayercount = showlayer;
                    for (int i = 0; i < allFile.Count; i++)
                    {
                        strbuilder3.Append(_stringBuilder3[showlayercount]);

                        showlayercount = showlayercount + allmodelLayer;
                    }

                    polyOutline.Data = Geometry.Parse("" + strbuilder3);
                }

            }
            catch (Exception e4)
            {
                MessageBox.Show(e4.Message);
            }
        }
        

        private void DownloadFile()
        {
            lvFile.SelectedIndex = 0;

            Uri ftpUri = new Uri(d.FtpName);
            WebClient wc = new WebClient();

            wc.Credentials = new NetworkCredential("ftpuser", "ftpuser");

            cd_FileLoading cdFileLoading = new cd_FileLoading();
            string[] strfile = cdFileLoading.GetFileList(d.FtpName);

            lvFile.ItemsSource = strfile;
        }
        #endregion

        public void realTimeMonitor(string ch1, string ch2, string ch3, string ch4, string ch5, string ch6)
        {
            int count = 0;

            if (ch1 == "True")
            {
                count++;
            }

            if (ch2 == "True")
            {
                count++;
            }

            if (ch3 == "True")
            {
                count++;
            }

            if (ch4 == "True")
            {
                count++;
            }

            if (ch5 == "True")
            {
                count++;
            }

            if (ch6 == "True")
            {
                count++;
            }
        }

        #region 19. 실시간 변화 값 및 모니터링 쪽 이벤트들

        public void RoomImageSetting()
        {
            Visibility visiLeft = Visibility.Visible;
            Visibility visiRight = Visibility.Collapsed;
            Uri uriSourceTabBackground = new Uri(@"/imgMG/imgMachine/mg_printer_left.png", UriKind.Relative); ;

            if (d.OPCItemValueTextBoxes[48] == "0") //챔버룸
            {
                d.StrRoomPosition = "챔버 룸";
                uriSourceTabBackground = new Uri(@"/imgMG/imgMachine/mg_printer_right.png", UriKind.Relative);

                visiLeft = Visibility.Collapsed;
                visiRight = Visibility.Visible;

            }
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            var vm = (DefineValue)DataContext;
            var chart = (LiveCharts.Wpf.CartesianChart)sender;

            var mouseCoordinate = e.GetPosition(chart);
            var p = chart.ConvertToChartValues(mouseCoordinate);

            vm.YPointer = p.Y;

            var series = chart.Series[0];
            var closetsPoint = series.ClosestPointTo(p.X, AxisOrientation.X);

            vm.XPointer = closetsPoint.X;
        }

        private void slLayer1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (d.StrPrintStaus == "출력전")
                {
                    StringBuilder strbuilder = new StringBuilder();
                    int showlayercount = showlayer;
                    for (int i = 0; i < allFile.Count; i++)
                    {
                        strbuilder.Append(_stringBuilder[showlayercount]);
                        showlayercount = showlayercount + allmodelLayer;
                    }

                    polyInline.Data = Geometry.Parse("" + strbuilder);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 자동공급부
        private void AutoPumpCommand()
        {
            apw = new AutoPumpWindow();
            apw.initalSetting(d);
            apw.ShowDialog();
        }

        public void PumpUp1()
        {
            if (double.Parse(apw.Mainpump.Content.ToString()) < 10)
            {
                double pump_main = double.Parse(apw.Mainpump.Content.ToString()) + 1;
                if (pump_main > 10) { pump_main = 10; }
                string bHomeCalibration_Zco = Convert.ToString(pump_main);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bHomeCalibration", bHomeCalibration_Zco);
                apw.Mainpump.Content = bHomeCalibration_Zco;
            }
        }

        public void PumpDown1()
        {
            if (double.Parse(apw.Mainpump.Content.ToString()) > 0)
            {
                double pump_main = double.Parse(apw.Mainpump.Content.ToString()) - 1;
                if (pump_main < 0) { pump_main = 0; }
                string bHomeCalibration_Zco = Convert.ToString(pump_main);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bHomeCalibration", bHomeCalibration_Zco);
                apw.Mainpump.Content = bHomeCalibration_Zco;
            }
        }

        public void PumpUp2()
        {
            if (double.Parse(apw.Remainpump.Content.ToString()) < 10)
            {
                double pump_remain = double.Parse(apw.Remainpump.Content.ToString()) + 1;
                if (pump_remain > 10) { pump_remain = 10; }
                string bJogNeg_Zco = Convert.ToString(pump_remain);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogNeg", bJogNeg_Zco);
                apw.Remainpump.Content = bJogNeg_Zco;
            }
        }

        public void PumpDown2()
        {
            if (double.Parse(apw.Remainpump.Content.ToString()) > 0)
            {
                double pump_remain = double.Parse(apw.Remainpump.Content.ToString()) - 1;
                if (pump_remain < 0) { pump_remain = 0; }
                string bJogNeg_Zco = Convert.ToString(pump_remain);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogNeg", bJogNeg_Zco);
                apw.Remainpump.Content = bJogNeg_Zco;
            }
        }
        #endregion

        private void GuideBeam()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpAutoMan.Input.Command.stLaserMode", 1);
            Thread.Sleep(1000);
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpAutoMan.Input.Command.bLaser", true);
        }

        private void GuideBeamOff()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpAutoMan.Input.Command.bLaser", false);
        }

        private void LaserControl()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpAutoMan.Input.Command.stLaserMode", 0);
            Thread.Sleep(1000);
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpAutoMan.Input.Command.bLaser", true);
        }

        private void LaserControlOff()
        {

            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpAutoMan.Input.Command.bLaser", false);

        }

        private void DockingBtn()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bDocking", true);
        }

        private void UnDockingBtn()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bUnDocking", true);
        }

        private void DockingConfirm()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bDockingConfirm", true);

        }

        #region ValueSelect

        /*호밍 값 입력*/
        private void SelectVel_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Vel_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Vel_X.Content = Vel_X.Content;
            }
            else
            {
                string rHomeVelocity_Xpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeVelocity", rHomeVelocity_Xpa);
            }
        }

        private void SelectVel_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Vel_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Vel_R.Content = Vel_R.Content;
            }
            else
            {
                string rHomeVelocity_Rpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeVelocity", rHomeVelocity_Rpa);
            }
        }

        private void SelectVel_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Vel_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Vel_Z.Content = Vel_Z.Content;
            }
            else
            {
                string rHomeVelocity_Zpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeVelocity", rHomeVelocity_Zpa);
            }
        }

        private void SelectVel_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Vel_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Vel_S.Content = Vel_S.Content;
            }
            else
            {
                string rHomeVelocity_Spa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeVelocity", rHomeVelocity_Spa);
            }
        }

        private void SelectAcc_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Acc_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Acc_X.Content = Acc_X.Content;
            }
            else
            {
                string rHomeAcceleration_Xpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeVelocity", rHomeAcceleration_Xpa);
            }
        }

        private void SelectAcc_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Acc_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Acc_R.Content = Acc_R.Content;
            }
            else
            {
                string rHomeAcceleration_Rpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeAcceleration", rHomeAcceleration_Rpa);
            }
        }

        private void SelectAcc_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Acc_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Acc_Z.Content = Acc_Z.Content;
            }
            else
            {
                string rHomeVelocity_Zpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeVelocity", rHomeVelocity_Zpa);
            }
        }

        private void SelectAcc_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Acc_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Acc_S.Content = Acc_S.Content;
            }
            else
            {
                string rHomeAcceleration_Spa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeAcceleration", rHomeAcceleration_Spa);
            }
        }

        private void SelectDec_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dec_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dec_X.Content = Dec_X.Content;
            }
            else
            {
                string rHomeDeceleration_Xpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeDeceleration", rHomeDeceleration_Xpa);
            }
        }

        private void SelectDec_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dec_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dec_R.Content = Dec_R.Content;
            }
            else
            {
                string rHomeDeceleration_Rpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeDeceleration", rHomeDeceleration_Rpa);
            }
        }

        private void SelectDec_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dec_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dec_Z.Content = Dec_Z.Content;
            }
            else
            {
                string rHomeDeceleration_Zpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeDeceleration", rHomeDeceleration_Zpa);
            }
        }

        private void SelectDec_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dec_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dec_S.Content = Dec_S.Content;
            }
            else
            {
                string rHomeDeceleration_Spa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeDeceleration", rHomeDeceleration_Spa);
            }
        }

        private void SelectDis_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dis_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dis_X.Content = Dis_X.Content;
            }
            else
            {
                string rHomeDistance_Xpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomeDistance", rHomeDistance_Xpa);
            }
        }

        private void SelectDis_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dis_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dis_R.Content = Dis_R.Content;
            }
            else
            {
                string rHomeDistance_Rpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomeDistance", rHomeDistance_Rpa);
            }
        }

        private void SelectDis_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dis_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dis_Z.Content = Dis_Z.Content;
            }
            else
            {
                string rHomeDistance_Zpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomeDistance", rHomeDistance_Zpa);
            }
        }

        private void SelectDis_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dis_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dis_S.Content = Dis_S.Content;
            }
            else
            {
                string rHomeDistance_Spa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeDistance", rHomeDistance_Spa);
            }
        }

        private void SelectTP_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "TP_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                TP_X.Content = TP_X.Content;
            }
            else
            {
                string rHomePosition_Xpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rHomePosition", rHomePosition_Xpa);
            }
        }

        private void SelectTP_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "TP_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                TP_R.Content = TP_R.Content;
            }
            else
            {
                string rHomePosition_Rpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rHomePosition", rHomePosition_Rpa);
            }
        }

        private void SelectTP_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "TP_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                TP_Z.Content = TP_Z.Content;
            }
            else
            {
                string rHomePosition_Zpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rHomePosition", rHomePosition_Zpa);
            }
        }

        private void SelectTP_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "TP_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                TP_S.Content = TP_S.Content;
            }
            else
            {
                string rHomePosition_Spa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomePosition", rHomePosition_Spa);
            }
        }

        private void SelectDir_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dir_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dir_X.Content = Dir_X.Content;
            }
            else
            {
                string nHomeMode_Xpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.nHomeMode", nHomeMode_Xpa);
            }
        }

        private void SelectDir_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dir_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dir_R.Content = Dir_R.Content;
            }
            else
            {
                string nHomeMode_Rpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.nHomeMode", nHomeMode_Rpa);
            }
        }

        private void SelectDir_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dir_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dir_Z.Content = Dir_Z.Content;
            }
            else
            {
                string nHomeMode_Zpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.nHomeMode", nHomeMode_Zpa);
            }
        }

        private void SelectDir_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Dir_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Dir_S.Content = Dir_S.Content;
            }
            else
            {
                string nHomeMode_Spa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.nHomeMode", nHomeMode_Spa);
            }
        }

        private void SelectOrder_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Order_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Order_X.Content = Order_X.Content;
            }
            else
            {
                string nHomeOrder_Xpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.nHomeOrder", nHomeOrder_Xpa);
            }
        }

        private void SelectOrder_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Order_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Order_R.Content = Order_R.Content;
            }
            else
            {
                string nHomeOrder_Rpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.nHomeOrder", nHomeOrder_Rpa);
            }
        }

        private void SelectOrder_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Order_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Order_Z.Content = Order_Z.Content;
            }
            else
            {
                string nHomeOrder_Zpa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.nHomeOrder", nHomeOrder_Zpa);
            }
        }

        private void SelectOrder_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Order_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Order_S.Content = Order_S.Content;
            }
            else
            {
                string nHomeOrder_Spa = Convert.ToString(vs.GetItemValue);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.nHomeOrder", nHomeOrder_Spa);
            }
        }

        /*호밍 관련 기능*/
        private void HomingSave()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bHomeParamApply", true);
        }

        private void HomingStart()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bHomeAll", true);
        }

        private void HomingStop()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bHomeStop", true);
        }

        private void HomingReset()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bResetHome", true);
        }
        #endregion

        #region Override Up / Down      
        private void OverrideUp()
        {
            string rSpeedOverride = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Param.rSpeedOverride").ToString();

            int isOverride = Convert.ToInt32(rSpeedOverride);
            
            if(isOverride < 100)
            {
                isOverride += 5; 
            }
            else if(isOverride >= 100)
            {
                isOverride = 100;
            }

            string rSpeedOverride2 = Convert.ToString(isOverride);
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Param.rSpeedOverride", rSpeedOverride2);
        }

        private void OverrideDown()
        {
            string rSpeedOverride = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Param.rSpeedOverride").ToString();

            int isOverride = Convert.ToInt32(rSpeedOverride);

            if (isOverride > 5)
            {
                isOverride -= 5;
            }
            else if (isOverride <= 5)
            {
                isOverride = 5;
            }

            string rSpeedOverride2 = Convert.ToString(isOverride);
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Param.rSpeedOverride", rSpeedOverride2);
        }

        #endregion

        private void MotorStop()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Machine.Command.bStop", true);
        }
    }
}
