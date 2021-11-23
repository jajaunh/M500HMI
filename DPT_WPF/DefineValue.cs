using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using LiveCharts;

namespace DPT_WPF
{
    public partial class DefineValue : PropertyChangedBase
    {


        public string[] PrintWeekend { get; set; }
        public Func<double, string> DayofWeek { get; set; }
        public double AxisStep { get; set; }



        private PointCollection points = new PointCollection();
        public PointCollection Points
        {
            get { return points; }
            set
            {
                points = value;
                NotifyOfPropertyChange(() => this.Points);
            }
        }


        private double _axisMax;
        private double _axisMin;
        public double AxisUnit { get; set; }
        public DefineValue()
        {

        }

        private string strTimeStart;
        public string StrTimeStart
        {
            get { return strTimeStart; }
            set
            {
                strTimeStart = value;
                NotifyOfPropertyChange(() => this.StrTimeStart);
            }
        }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                NotifyOfPropertyChange(() => this.AxisMax);
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                NotifyOfPropertyChange(() => this.AxisMin);
            }
        }

        public Func<double, string> Formatter { get; set; }
        public Func<double, string> PrintingFormatter { get; set; }

        public Func<double, string> DateTimeFormatter { get; set; }
        public SeriesCollection ChamberOxySeries { get; set; }
        public SeriesCollection GloveOxySeries { get; set; }

        public SeriesCollection SubChamberOxySeries { get; set; }
        public SeriesCollection SubGloveOxySeries { get; set; }

        #region Monitoring

        private double _xPointer;
        private double _yPointer;
        private double _from;
        private double _to;

        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                NotifyOfPropertyChange(() => this.From);
            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                NotifyOfPropertyChange(() => this.To);
            }
        }

        public double XPointer
        {
            get { return _xPointer; }
            set
            {
                _xPointer = value;
                NotifyOfPropertyChange(() => this.XPointer);
            }
        }

        public double YPointer
        {
            get { return _yPointer; }
            set
            {
                _yPointer = value;
                NotifyOfPropertyChange(() => this.YPointer);
            }
        }

        //public Func<double, string> Formatter { get; set; }



        public SeriesCollection MonthlyMachineCollection { get; set; }
        private double dblMotorStepper0PostionFeedback;
        private double dblMotorStepper1PostionFeedback;
        private double dblMotorStepper2PostionFeedback;

        private string strMachineTime = "0";
        private string strFilterTime = "0";
        private string strPumpTime = "0";
        private string strScannerTime = "0";

        private string strCameraShot = "0";
        private string strAfterBlade = "0";

        private string strOxyCheck = "정상";

        public string StrOxyCheck
        {
            get { return strOxyCheck; }
            set
            {
                if (strOxyCheck != value)
                {
                    strOxyCheck = value;
                    NotifyOfPropertyChange(() => this.StrOxyCheck);
                }
            }
        }
        public string StrCameraShot
        {
            get { return strCameraShot; }
            set
            {
                if (strCameraShot != value)
                {
                    strCameraShot = value;
                    NotifyOfPropertyChange(() => this.StrCameraShot);
                }
            }
        }

        public string StrAfterBlade
        {
            get { return strAfterBlade; }
            set
            {
                if (strAfterBlade != value)
                {
                    strAfterBlade = value;
                    NotifyOfPropertyChange(() => this.StrAfterBlade);
                }
            }
        }
        public string StrScannerTime
        {
            get { return strScannerTime; }
            set
            {
                if (strScannerTime != value)
                {
                    strScannerTime = value;
                    NotifyOfPropertyChange(() => this.StrScannerTime);
                }
            }
        }
        public string StrMachineTime
        {
            get { return strMachineTime; }
            set
            {
                if (strMachineTime != value)
                {
                    strMachineTime = value;
                    NotifyOfPropertyChange(() => this.StrMachineTime);
                }
            }
        }
        public string StrFilterTime
        {
            get { return strFilterTime; }
            set
            {
                if (strFilterTime != value)
                {
                    strFilterTime = value;
                    NotifyOfPropertyChange(() => this.StrFilterTime);
                }
            }
        }
        public string StrPumpTime
        {
            get { return strPumpTime; }
            set
            {
                if (strPumpTime != value)
                {
                    strPumpTime = value;
                    NotifyOfPropertyChange(() => this.StrPumpTime);
                }
            }
        }

        public double DblMotorStepper0PostionFeedback
        {
            get { return dblMotorStepper0PostionFeedback; }
            set
            {
                if (dblMotorStepper0PostionFeedback != value)
                {
                    dblMotorStepper0PostionFeedback = value;
                    NotifyOfPropertyChange(() => this.DblMotorStepper0PostionFeedback);
                }
            }
        }
        public double DblMotorStepper1PostionFeedback
        {
            get { return dblMotorStepper1PostionFeedback; }
            set
            {
                if (dblMotorStepper1PostionFeedback != value)
                {
                    dblMotorStepper1PostionFeedback = value;
                    NotifyOfPropertyChange(() => this.DblMotorStepper1PostionFeedback);
                }
            }
        }
        public double DblMotorStepper2PostionFeedback
        {
            get { return dblMotorStepper2PostionFeedback; }
            set
            {
                if (dblMotorStepper2PostionFeedback != value)
                {
                    dblMotorStepper2PostionFeedback = value;
                    NotifyOfPropertyChange(() => this.DblMotorStepper2PostionFeedback);
                }
            }
        }
        #endregion


        #region  로그인 부분
        private string _ftpName = "";
        private string _hostNmae; //opc.tcp://192.168.255.221
        private string _userName; //daeguntech
        private string _password; //daeguntech 
        public string FtpName
        {
            get { return _ftpName; }
            set
            {
                if (_ftpName != value)
                {
                    _ftpName = value;
                    NotifyOfPropertyChange(() => this.FtpName);
                }
            }
        }
        public string HostName
        {
            get { return _hostNmae; }
            set
            {
                if (_hostNmae != value)
                {
                    _hostNmae = value;
                    NotifyOfPropertyChange(() => this.HostName);
                }
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    NotifyOfPropertyChange(() => this.UserName);
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    NotifyOfPropertyChange(() => this.Password);
                }
            }
        }

        #endregion

        #region//READ WRITE 갯수
        public string[] OPCItemNameTextBoxes;
        public string[] OPCItemValueTextBoxes;
        public string[] OPCItemQualityTextBoxes;
        public string[] OPCItemWriteValueTextBoxes;
        public string[] OPCItemNameWriteTextBoxes;
        private int _readCount;
        public int ReadCount
        {
            get { return _readCount; }
            set
            {
                if (_readCount != value)
                {
                    _readCount = value;
                    NotifyOfPropertyChange(() => this.ReadCount);
                }
            }
        }

        #endregion

        #region REAL TIME 실시간 값

        private string puChamberOxy = "21";
        public string PUChamberOxy
        {
            get { return puChamberOxy; }
            set
            {
                puChamberOxy = value;
                NotifyOfPropertyChange(() => this.PUChamberOxy);
            }
        }

        private string puChamberLaser = "21";
        public string PuChamberLaser
        {
            get { return puChamberLaser; }
            set
            {
                puChamberLaser = value;
                NotifyOfPropertyChange(() => this.PuChamberLaser);
            }
        }
        private string puGloveOxy = "21";
        public string PUGloveOxy
        {
            get { return puGloveOxy; }
            set
            {
                puGloveOxy = value;
                NotifyOfPropertyChange(() => this.PUGloveOxy);
            }
        }

        private string puChamberSubOxy = "21";
        public string PUChamberSubOxy
        {
            get { return puChamberSubOxy; }
            set
            {
                puChamberSubOxy = value;
                NotifyOfPropertyChange(() => this.PUChamberSubOxy);
            }
        }
        #endregion

        #region laser
        private string strLaser = "0";
        public string StrLaser
        {
            get { return strLaser; }
            set
            {
                if (strLaser != value)
                {
                    strLaser = value;
                    NotifyOfPropertyChange(() => this.StrLaser);
                }
            }
        }

        #endregion

        #region Temperature / Humity
        private double puHumity = 0;
        public double PuHumity
        {
            get { return puHumity; }
            set
            {
                if (puHumity != value)
                {
                    puHumity = value;
                    NotifyOfPropertyChange(() => this.PuHumity);
                }
            }
        }
        #endregion

        #region Printing
        private string strPrintStaus = "출력전";
        private string strPrintPercetage = "0%";
        private string strPrintEstTime = "";

        private int intPirntPercetage = 0;
        public int IntPirntPercetage
        {
            get { return intPirntPercetage; }
            set
            {
                if (intPirntPercetage != value)
                {
                    intPirntPercetage = value;
                    NotifyOfPropertyChange(() => this.IntPirntPercetage);
                }
            }
        }
        public string StrPrintStaus
        {
            get { return strPrintStaus; }
            set
            {
                if (strPrintStaus != value)
                {
                    strPrintStaus = value;
                    NotifyOfPropertyChange(() => this.StrPrintStaus);
                }
            }
        }
        public string StrPrintPercetage
        {
            get { return strPrintPercetage; }
            set
            {
                if (strPrintPercetage != value)
                {
                    strPrintPercetage = value;
                    NotifyOfPropertyChange(() => this.StrPrintPercetage);
                }
            }
        }

        public string StrPrintEstTime
        {
            get { return strPrintEstTime; }
            set
            {
                if (strPrintEstTime != value)
                {
                    strPrintEstTime = value;
                    NotifyOfPropertyChange(() => this.StrPrintEstTime);
                }
            }
        }
        #endregion


        private double dblLaserPower = 0;

        public double DblLaserPower
        {
            get { return dblLaserPower; }
            set
            {
                if (dblLaserPower != value)
                {
                    dblLaserPower = value;
                    NotifyOfPropertyChange(() => this.DblLaserPower);
                }
            }
        }

        private string pumpnum1 = "0";

        public string Pumpnum1
        {
            get { return pumpnum1; }
            set
            {
                if (pumpnum1 != value)
                {
                    pumpnum1 = value;
                    NotifyOfPropertyChange(() => this.Pumpnum1);
                }
            }
        }

        private string pumpnum2 = "0";

        public string Pumpnum2
        {
            get { return pumpnum2; }
            set
            {
                if (pumpnum2 != value)
                {
                    pumpnum2 = value;
                    NotifyOfPropertyChange(() => this.Pumpnum2);
                }
            }
        }



        private string strTemperature = "20℃";
        private string strTemperatureGlove = "20℃";
        private int intTemperature = 20;
        private int intTemperatureGlove = 20;


        private string strHumity = "0%";

        private string strHumityGlove = "0%";
        private int intHumity = 0;
        private int intHumityGlove = 0;
        private string strPressure = "0";
        private string strDewPoint = "0";
        private int intDewPoint = 0;
        private int intPressure = 0;

        private string strRoomPosition = "글로브 룸";

        public string StrTemperature
        {
            get { return strTemperature; }
            set
            {
                if (strTemperature != value)
                {
                    strTemperature = value;
                    NotifyOfPropertyChange(() => this.StrTemperature);
                }
            }
        }
        public string StrTemperatureGlove
        {
            get { return strTemperatureGlove; }
            set
            {
                if (strTemperatureGlove != value)
                {
                    strTemperatureGlove = value;
                    NotifyOfPropertyChange(() => this.StrTemperatureGlove);
                }
            }
        }
        public int IntTemperature
        {
            get { return intTemperature; }
            set
            {
                if (intTemperature != value)
                {
                    intTemperature = value;
                    NotifyOfPropertyChange(() => this.IntTemperature);
                }
            }
        }
        public int IntTemperatureGlove
        {
            get { return intTemperatureGlove; }
            set
            {
                if (intTemperatureGlove != value)
                {
                    intTemperatureGlove = value;
                    NotifyOfPropertyChange(() => this.IntTemperatureGlove);
                }
            }
        }
        public string StrHumity
        {
            get { return strHumity; }
            set
            {
                if (strHumity != value)
                {
                    strHumity = value;
                    NotifyOfPropertyChange(() => this.StrHumity);
                }
            }
        }


        public string StrDewPoint
        {
            get { return strHumity; }
            set
            {
                if (strDewPoint != value)
                {
                    strDewPoint = value;
                    NotifyOfPropertyChange(() => this.StrDewPoint);
                }
            }
        }
        public string StrPressure
        {
            get { return strHumity; }
            set
            {
                if (strPressure != value)
                {
                    strPressure = value;
                    NotifyOfPropertyChange(() => this.StrPressure);
                }
            }
        }
        public int IntDewPoint
        {
            get { return intHumity; }
            set
            {
                if (intDewPoint != value)
                {
                    intDewPoint = value;
                    NotifyOfPropertyChange(() => this.IntDewPoint);
                }
            }
        }
        public int IntPressure
        {
            get { return intHumity; }
            set
            {
                if (intPressure != value)
                {
                    intPressure = value;
                    NotifyOfPropertyChange(() => this.IntPressure);
                }
            }
        }
        public string StrHumityGlove
        {
            get { return strHumityGlove; }
            set
            {
                if (strHumityGlove != value)
                {
                    strHumityGlove = value;
                    NotifyOfPropertyChange(() => this.StrHumityGlove);
                }
            }
        }
        public int IntHumity
        {
            get { return intHumity; }
            set
            {
                if (intHumity != value)
                {
                    intHumity = value;
                    NotifyOfPropertyChange(() => this.IntHumity);
                }
            }
        }
        public int IntHumityGlvoe
        {
            get { return intHumityGlove; }
            set
            {
                if (intHumityGlove != value)
                {
                    intHumityGlove = value;
                    NotifyOfPropertyChange(() => this.IntHumityGlvoe);
                }
            }
        }

        public string StrRoomPosition
        {
            get { return strRoomPosition; }
            set
            {
                if (strRoomPosition != value)
                {
                    strRoomPosition = value;
                    NotifyOfPropertyChange(() => this.StrRoomPosition);
                }
            }
        }
        private int picFileCount = 0;
        public int PicFileCount
        {
            get { return picFileCount; }
            set
            {
                if (picFileCount != value)
                {
                    picFileCount = value;
                    NotifyOfPropertyChange(() => this.PicFileCount);
                }

            }
        }

        private IChartValues values;
        public IChartValues Values
        {
            get { return values; }
            set
            {
                if (values != value)
                {
                    values = value;
                    NotifyOfPropertyChange(() => this.Values);
                }

            }
        }

        private IChartValues minusvalues;
        public IChartValues Minusvalues
        {
            get { return minusvalues; }
            set
            {
                if (minusvalues != value)
                {
                    minusvalues = value;
                    NotifyOfPropertyChange(() => this.Minusvalues);
                }

            }
        }

        #region Heater set
        private int currentHeaterTemperature = 0;
        private string targetHeaterTemperature = "0";
        private int ctargetHeaterTemperature = 0;

        public int CurrentHeaterTemperature
        {
            get { return currentHeaterTemperature; }
            set
            {
                if (currentHeaterTemperature != value)
                {
                    currentHeaterTemperature = value;
                    NotifyOfPropertyChange(() => this.CurrentHeaterTemperature);
                }
            }
        }

        public string TargetHeaterTemperature
        {
            get { return targetHeaterTemperature; }
            set
            {
                if (targetHeaterTemperature != value)
                {
                    targetHeaterTemperature = value;
                    NotifyOfPropertyChange(() => this.TargetHeaterTemperature);
                }
            }
        }

        public int CtargetHeaterTemperature
        {
            get { return ctargetHeaterTemperature; }
            set
            {
                if (ctargetHeaterTemperature != value)
                {
                    ctargetHeaterTemperature = value;
                    NotifyOfPropertyChange(() => this.CtargetHeaterTemperature);
                }
            }
        }
        #endregion

        #region Heater 온도 세팅

        private int heaterSet1 = 0;
        private int heaterSet2 = 0;
        private int heaterSet3 = 1;


        public int HeaterSet1
        {
            get { return heaterSet1; }
            set
            {
                if (heaterSet1 != value)
                {
                    heaterSet1 = value;
                    NotifyOfPropertyChange(() => this.HeaterSet1);
                }
            }
        }
        public int HeaterSet2
        {
            get { return heaterSet2; }
            set
            {
                if (heaterSet2 != value)
                {
                    heaterSet2 = value;
                    NotifyOfPropertyChange(() => this.HeaterSet2);
                }
            }
        }
        public int HeaterSet3
        {
            get { return heaterSet3; }
            set
            {
                if (heaterSet3 != value)
                {
                    heaterSet3 = value;
                    NotifyOfPropertyChange(() => this.HeaterSet3);
                }
            }
        }

        #endregion


        #region powder number
        private int pnumber1 = 2;
        private int pnumber2 = 0;
        private int pnumber3 = 0;

        private int omaxnumber1 = 0;
        private int omaxnumber2 = 0;
        private int omaxnumber3 = 5;

        private int ominnumber1 = 0;
        private int ominnumber2 = 0;
        private int ominnumber3 = 1;

        public int Omaxnumber1
        {
            get { return omaxnumber1; }
            set
            {
                if (omaxnumber1 != value)
                {
                    omaxnumber1 = value;
                    NotifyOfPropertyChange(() => this.Omaxnumber1);
                }

            }
        }
        public int Omaxnumber2
        {
            get { return omaxnumber2; }
            set
            {
                if (omaxnumber2 != value)
                {
                    omaxnumber2 = value;
                    NotifyOfPropertyChange(() => this.Omaxnumber2);
                }

            }
        }
        public int Omaxnumber3
        {
            get { return omaxnumber3; }
            set
            {
                if (omaxnumber3 != value)
                {
                    omaxnumber3 = value;
                    NotifyOfPropertyChange(() => this.Omaxnumber3);
                }

            }
        }

        public int Ominnumber1
        {
            get { return ominnumber1; }
            set
            {
                if (ominnumber1 != value)
                {
                    ominnumber1 = value;
                    NotifyOfPropertyChange(() => this.Ominnumber1);
                }

            }
        }
        public int Ominnumber2
        {
            get { return ominnumber2; }
            set
            {
                if (ominnumber2 != value)
                {
                    ominnumber2 = value;
                    NotifyOfPropertyChange(() => this.Ominnumber2);
                }

            }
        }
        public int Ominnumber3
        {
            get { return ominnumber3; }
            set
            {
                if (ominnumber3 != value)
                {
                    ominnumber3 = value;
                    NotifyOfPropertyChange(() => this.Ominnumber3);
                }

            }
        }

        public int Pnumber1
        {
            get { return pnumber1; }
            set
            {
                if (pnumber1 != value)
                {
                    pnumber1 = value;
                    NotifyOfPropertyChange(() => this.Pnumber1);
                }

            }
        }
        public int Pnumber2
        {
            get { return pnumber2; }
            set
            {
                if (pnumber2 != value)
                {
                    pnumber2 = value;
                    NotifyOfPropertyChange(() => this.Pnumber2);
                }

            }
        }
        public int Pnumber3
        {
            get { return pnumber3; }
            set
            {
                if (pnumber3 != value)
                {
                    pnumber3 = value;
                    NotifyOfPropertyChange(() => this.Pnumber3);
                }

            }
        }
        
        #endregion

        private string dblPowderSupply1 = "0";
        public string DblPowderSupply1
        {
            get { return dblPowderSupply1; }
            set
            {
                if (dblPowderSupply1 != value)
                {
                    dblPowderSupply1 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply1);
                }
            }
        }
        private string dblPowderSupply2 = "0";
        public string DblPowderSupply2
        {
            get { return dblPowderSupply2; }
            set
            {
                if (dblPowderSupply2 != value)
                {
                    dblPowderSupply2 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply2);
                }
            }
        }
        private string dblPowderSupply3 = "0";
        public string DblPowderSupply3
        {
            get { return dblPowderSupply3; }
            set
            {
                if (dblPowderSupply3 != value)
                {
                    dblPowderSupply3 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply3);
                }
            }
        }
        private string dblPowderSupply4 = "0";
        public string DblPowderSupply4
        {
            get { return dblPowderSupply4; }
            set
            {
                if (dblPowderSupply4 != value)
                {
                    dblPowderSupply4 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply4);
                }
            }
        }
        public ICommand Setting3Command { get; set; }
        public ICommand CloseWindowsCommand { get; set; }

        public ICommand SelArCommand { get; set; }
        public ICommand SelAr1Command { get; set; }
        public ICommand SelN2Command { get; set; }
        public ICommand PumpUpCommand { get; set; }
        public ICommand PumpDownCommand { get; set; }
        public ICommand GasSupplyCommand { get; set; }
        public ICommand LaserControlCommand { get; set; }
        public ICommand LaserControlCommandOff { get; set; }
        public ICommand GuideBeamCommand { get; set; }
        public ICommand GuideBeamCommandOff { get; set; }

        public ICommand ErrorResetCommand { get; set; }

        public ImageSource Thumbnail { get; set; }

        #region ICommand
        public ICommand Down1HeatersetCommand { get; set; }
        public ICommand Down2HeatersetCommand { get; set; }
        public ICommand Down3HeatersetCommand { get; set; }

        public ICommand Up1HeatersetCommand { get; set; }
        public ICommand Up2HeatersetCommand { get; set; }
        public ICommand Up3HeatersetCommand { get; set; }

        public ICommand Up1PowderCommand { get; set; }
        public ICommand Down1PowderCommand { get; set; }
        public ICommand Up2PowderCommand { get; set; }
        public ICommand Down2PowderCommand { get; set; }
        public ICommand Up3PowderCommand { get; set; }
        public ICommand Down3PowderCommand { get; set; }

        public ICommand AirPressureCommand { get; set; }
        public ICommand AutoPumpCommand { get; set; }
        public ICommand SupplyRatioCommand { get; set; }
        public ICommand Up1OxyMaxCommand { get; set; }
        public ICommand Down1OxyMaxCommand { get; set; }
        public ICommand Up2OxyMaxCommand { get; set; }
        public ICommand Down2OxyMaxCommand { get; set; }
        public ICommand Up3OxyMaxCommand { get; set; }
        public ICommand Down3OxyMaxCommand { get; set; }

        public ICommand Up1OxyMinCommand { get; set; }
        public ICommand Down1OxyMinCommand { get; set; }
        public ICommand Up2OxyMinCommand { get; set; }
        public ICommand Down2OxyMinCommand { get; set; }
        public ICommand Up3OxyMinCommand { get; set; }
        public ICommand Down3OxyMinCommand { get; set; }

        //자동공급부 펌프
        public ICommand AutoSupplyCommand { get; set; }
        public ICommand Up1PumpCommand { get; set; }
        public ICommand Up2PumpCommand { get; set; }
        public ICommand Down1PumpCommand { get; set; }
        public ICommand Down2PumpCommand { get; set; }

        #endregion

        public ICommand LED_Left_Command { get; set; }
        public ICommand LED_Right_Command { get; set; }

        public ICommand StartCommand { get; set; }
        public ICommand PrintStartCommand { get; set; }
        public ICommand PrintPauseCommand { get; set; }
        public ICommand PrintResumeCommand { get; set; }
        public ICommand PrintStopCommand { get; set; }

        public ICommand RoomMoveCommand { get; set; }
        public ICommand RoomMoveOtherCommand { get; set; }
        public ICommand RoomMoveYesCommand { get; set; }

        public ICommand PrintDeleteCommand { get; set; }
        public ICommand PrintResetCommand { get; set; }
        public ICommand PrintFinishCommand { get; set; }
        public ICommand PrintFileArrayCommand { get; set; }
        public ICommand PrintFileDeleteCommand { get; set; }

        public ICommand TabClickCommand { get; set; }
        public ICommand OPCWriteCommand { get; set; }

        public ICommand TabRightCommand { get; set; }
        public ICommand TabLeftCommand { get; set; }
        public ICommand PowderSupplyWindowCommand1 { get; set; }
        public ICommand PowderSupplyWindowCommand2 { get; set; }
        public ICommand PowderSupplyWindowCommand3 { get; set; }
        public ICommand PowderSupplyCommand { get; set; }
        public ICommand OxygenMaxWindowCommand { get; set; }
        public ICommand OxygenMinWindowCommand { get; set; }
        public ICommand OxygenMaxApplyCommand { get; set; }
        public ICommand OxygenMinApplyCommand { get; set; }
        public ICommand HeaterApplyCommand { get; set; }
        public ICommand HeaterSetApplyCommand { get; set; }
        public ICommand HeaterMoveCommand { get; set; }

        public ICommand GasPopupCloseCommand { get; set; }

        //M500 추가작업
        public ICommand DockingCommand { get; set; }
        public ICommand UnDockingCommand { get; set; }
        public ICommand DockingConfirmCommand { get; set; }

        public ICommand MinOxyCommand { get; set; }
        public ICommand MaxOxyCommand { get; set; }

        public ICommand MainPumpPlusCommand { get; set; }
        public ICommand MainPumpMinusCommand { get; set; }
        public ICommand SubPumpPlusCommand { get; set; }
        public ICommand SubPumpMinusCommand { get; set; }

        public ICommand Filter1Command { get; set; }
        public ICommand Filter2Command { get; set; }
        public ICommand FilterEnableCommand { get; set; }

        public ICommand MainPumpControlCommand { get; set; }
        public ICommand MainPumpStopCommand { get; set; }
        public ICommand SubPumpControlCommand { get; set; }
        public ICommand SubPumpStopCommand { get; set; }

        public ICommand FrontDoorCommand { get; set; }
        public ICommand RearDoorCommand { get; set; }

        public ICommand AlramImageCommand { get; set; }

        public ICommand MotorStopCommand { get; set; }

        public ICommand Motor_X_HomeCommand { get; set; }
        public ICommand Motor_R_HomeCommand { get; set; }
        public ICommand Motor_Z_HomeCommand { get; set; }
        public ICommand Motor_S_HomeCommand { get; set; }

        public ICommand MotorDistance_X_Command { get; set; }
        public ICommand MotorDistance_R_Command { get; set; }
        public ICommand MotorDistance_Z_Command { get; set; }
        public ICommand MotorDistance_S_Command { get; set; }

        public ICommand AddNeg1Command { get; set; }
        public ICommand AddNeg2Command { get; set; }
        public ICommand AddNeg3Command { get; set; }
        public ICommand AddNeg4Command { get; set; }

        public ICommand AddPos1Command { get; set; }
        public ICommand AddPos2Command { get; set; }
        public ICommand AddPos3Command { get; set; }
        public ICommand AddPos4Command { get; set; }

        public ICommand JogNeg1Command { get; set; }
        public ICommand JogNeg2Command { get; set; }
        public ICommand JogNeg3Command { get; set; }
        public ICommand JogNeg4Command { get; set; }

        public ICommand JogPos1Command { get; set; }
        public ICommand JogPos2Command { get; set; }
        public ICommand JogPos3Command { get; set; }
        public ICommand JogPos4Command { get; set; }

        public ICommand OverrideDownCommand { get; set; }
        public ICommand OverrideUpCommand { get; set; }

        //Value 팝업창
        public ICommand Vel_XCommand { get; set; }
        public ICommand Vel_RCommand { get; set; }
        public ICommand Vel_ZCommand { get; set; }
        public ICommand Vel_SCommand { get; set; }

        public ICommand Acc_XCommand { get; set; }
        public ICommand Acc_RCommand { get; set; }
        public ICommand Acc_ZCommand { get; set; }
        public ICommand Acc_SCommand { get; set; }

        public ICommand Dec_XCommand { get; set; }
        public ICommand Dec_RCommand { get; set; }
        public ICommand Dec_ZCommand { get; set; }
        public ICommand Dec_SCommand { get; set; }

        public ICommand Dis_XCommand { get; set; }
        public ICommand Dis_RCommand { get; set; }
        public ICommand Dis_ZCommand { get; set; }
        public ICommand Dis_SCommand { get; set; }

        public ICommand TP_XCommand { get; set; }
        public ICommand TP_RCommand { get; set; }
        public ICommand TP_ZCommand { get; set; }
        public ICommand TP_SCommand { get; set; }

        public ICommand Dir_XCommand { get; set; }
        public ICommand Dir_RCommand { get; set; }
        public ICommand Dir_ZCommand { get; set; }
        public ICommand Dir_SCommand { get; set; }

        public ICommand Order_XCommand { get; set; }
        public ICommand Order_RCommand { get; set; }
        public ICommand Order_ZCommand { get; set; }
        public ICommand Order_SCommand { get; set; }

        public ICommand HomingSaveCommand { get; set; }
        public ICommand HomingStartCommand { get; set; }
        public ICommand HomingStopCommand { get; set; }
        public ICommand HomingResetCommand { get; set; }

        

        private int RandomNumber(int MaxNumber, int MinNumber)
        {
            Random r = new Random(System.DateTime.Now.Millisecond);

            if (MinNumber > MaxNumber)
            {
                int t = MinNumber;
                MinNumber = MaxNumber;
                MaxNumber = t;
            }

            return r.Next(MinNumber, MaxNumber);
        }

    }
}
