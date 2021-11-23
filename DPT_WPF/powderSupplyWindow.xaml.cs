
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
using System.Windows.Shapes;

namespace DPT_WPF
{
    /// <summary>
    /// powderSupplyWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class powderSupplyWindow : Window
    {
        private int checkNumber = 1;
        

        DefineValue d;
        public powderSupplyWindow()
        {
            InitializeComponent();
        }

        public void initalSetting(DefineValue dv, int cn)
        {

            InitializeComponent();
            checkNumber = cn;
            

            string tempPostion = "";


            if (checkNumber == 1)
            {
                

                
            }
            else if (checkNumber == 2)
            {
                

                
            }
            else if (checkNumber == 3)
            {
                
            }
            else if (checkNumber == 4)
            {

            }
            else
            {

            }

            this.DataContext = dv;
            d = dv;
        }
        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
