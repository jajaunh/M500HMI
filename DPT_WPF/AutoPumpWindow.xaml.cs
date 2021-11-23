
using OpcLabs.EasyOpc.UA;
using System.Windows;
using System.Windows.Input;

namespace DPT_WPF
{
    /// <summary>
    /// speedWindows.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AutoPumpWindow : Window
    {
        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        DefineValue d;

        

        public AutoPumpWindow()
        {
            InitializeComponent();
        }

        public void initalSetting(DefineValue dv)
        {
            InitializeComponent();
      

            this.DataContext = dv;
            d = dv;

            string Pump1_main = "0";
            string Pump1_remain = "0";

            if (Pump1_main != (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeAcceleration").ToString())
            {
                d.Pumpnum1 = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeAcceleration").ToString();
            }

            if (Pump1_remain != (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeDeceleration").ToString())
            {
                d.Pumpnum2 = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rHomeDeceleration").ToString();
            }
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
