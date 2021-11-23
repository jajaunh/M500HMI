
using OpcLabs.EasyOpc.UA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace DPT_WPF.ucCommon
{
    /// <summary>
    /// ucCommonMgLED.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonLED : UserControl
    {
        public ucCommonLED()
        {
            InitializeComponent();
        }

        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        DefineValue d;

        public ucCommonLED(DefineValue dv)
        {
           
            dv.LED_Left_Command = new DelegateCommand(this.LedLeft);
            dv.LED_Right_Command = new DelegateCommand(this.LedRight);
            
            d = dv;
        }

        public void LedLeft()
        {
            if((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed1On").ToString() == "False")
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed1On", true);
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed1On", false);
            }
        }

        public void LedRight()
        {
            if ((client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed2On").ToString() == "False")
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed2On", true);
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bLed2On", false);
            }
        }

    }
}
