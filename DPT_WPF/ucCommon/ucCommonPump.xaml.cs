
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using OpcLabs.EasyOpc.UA;

namespace DPT_WPF.ucCommon
{
    /// <summary>
    /// ucCommonPump.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonPump : UserControl
    {
        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        DefineValue d;
        

        public ucCommonPump()
        {
            InitializeComponent();
        }

        public ucCommonPump(DefineValue dv)
        {
            
            dv.MainPumpPlusCommand = new DelegateCommand(this.MainPumpPlus);
            dv.MainPumpMinusCommand = new DelegateCommand(this.MainPumpMinus);
            dv.SubPumpPlusCommand = new DelegateCommand(this.SubPumpPlus);
            dv.SubPumpMinusCommand = new DelegateCommand(this.SubPumpMinus);
            dv.MainPumpControlCommand = new DelegateCommand(this.MainPumpControl);
            dv.MainPumpStopCommand = new DelegateCommand(this.MainPumpStop);
            dv.SubPumpControlCommand = new DelegateCommand(this.SubPumpControl);
            dv.SubPumpStopCommand = new DelegateCommand(this.SubPumpStop);
            dv.Filter1Command = new DelegateCommand(this.Filter1);
            dv.Filter2Command = new DelegateCommand(this.Filter2);
            dv.FilterEnableCommand = new DelegateCommand(this.FilterEnable);

            d = dv;
        }

        public void MainPumpPlus()
        {
            double MainPumpPlus = Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Parameter.rSetValue").ToString());

            if (MainPumpPlus < 10)
            {
                MainPumpPlus += 0.1;

                string rSetValue_pump1 = Convert.ToString(MainPumpPlus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Parameter.rSetValue", rSetValue_pump1);
            }
            else if(MainPumpPlus >= 10)
            {
                MainPumpPlus = 10;

                string rSetValue_pump1 = Convert.ToString(MainPumpPlus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Parameter.rSetValue", rSetValue_pump1);
            }
        }

        public void MainPumpMinus()
        {
            double MainPumpMinus = Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Parameter.rSetValue").ToString());

            if (MainPumpMinus > 0)
            {
                MainPumpMinus -= 0.1;
                string rSetValue_pump1 = Convert.ToString(MainPumpMinus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Parameter.rSetValue", rSetValue_pump1);
            }
            else if (MainPumpMinus <= 0)
            {
                MainPumpMinus = 0;

                string rSetValue_pump1 = Convert.ToString(MainPumpMinus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Parameter.rSetValue", rSetValue_pump1);
            }
        }

        public void SubPumpPlus()
        {
            double SubPumpPlus = Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Parameter.rSetValue").ToString());

            if (SubPumpPlus < 10)
            {
                SubPumpPlus += 0.1;
                string rSetValue_pump2 = Convert.ToString(SubPumpPlus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Parameter.rSetValue", rSetValue_pump2);
            }
            else if (SubPumpPlus >= 10)
            {
                SubPumpPlus = 10;

                string rSetValue_pump2 = Convert.ToString(SubPumpPlus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Parameter.rSetValue", rSetValue_pump2);
            }
        }

        public void SubPumpMinus()
        {
            double SubPumpMinus = Convert.ToDouble((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Parameter.rSetValue").ToString());

            if (SubPumpMinus > 0)
            {
                SubPumpMinus -= 0.1;
                string rSetValue_pump2 = Convert.ToString(SubPumpMinus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Parameter.rSetValue", rSetValue_pump2);
            }
            else if (SubPumpMinus <= 0)
            {
                SubPumpMinus = 0;

                string rSetValue_pump2 = Convert.ToString(SubPumpMinus);
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Parameter.rSetValue", rSetValue_pump2);
            }
        }

        public void MainPumpControl()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Command.bControl", true);
        }

        public void MainPumpStop()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Command.bStop", true);
        }

        public void SubPumpControl()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[0].Input.Command.bStop", true);
        }
        
        public void SubPumpStop()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpPumpMan[1].Input.Command.bStop", true);
        }

        public void Filter1()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpFilterMan.Input.Configuration.stFilterType", 0);
        }

        public void Filter2()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpFilterMan.Input.Configuration.stFilterType", 1);
        }

        public void FilterEnable()
        {
            if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpFilterMan.Input.Command.bEnable").ToString() == "False")
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpFilterMan.Input.Command.bEnable", true);
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpFilterMan.Input.Command.bEnable", false);
            }
            
        }
    }
}
