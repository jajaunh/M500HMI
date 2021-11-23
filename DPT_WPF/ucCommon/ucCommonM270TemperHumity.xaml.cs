
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
    /// ucCommonTemperHumity.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonM270TemperHumity : UserControl
    {
        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        DefineValue d;
        

        public ucCommonM270TemperHumity()
        {
            InitializeComponent();
        }

        public ucCommonM270TemperHumity(DefineValue dv)
        {
            InitializeComponent();         
            this.DataContext = dv;

            d = dv;


        }
    }
}
