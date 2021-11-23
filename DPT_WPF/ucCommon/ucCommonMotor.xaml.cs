using OpcLabs.EasyOpc.UA;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DPT_WPF.ucCommon
{
    /// <summary>
    /// ucCommonMotor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonMotor : UserControl
    {
        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        DefineValue d;
        ValueSelect vs = new ValueSelect();

        public ucCommonMotor()
        {
            InitializeComponent();
        }

        public ucCommonMotor(DefineValue dv)
        {
            InitializeComponent();

            dv.Motor_X_HomeCommand = new DelegateCommand(this.Motor_X_Home);
            dv.Motor_R_HomeCommand = new DelegateCommand(this.Motor_R_Home);
            dv.Motor_Z_HomeCommand = new DelegateCommand(this.Motor_Z_Home);
            dv.Motor_S_HomeCommand = new DelegateCommand(this.Motor_S_Home);

            dv.MotorDistance_X_Command = new DelegateCommand(this.SelectDistance_X);
            dv.MotorDistance_R_Command = new DelegateCommand(this.SelectDistance_R);
            dv.MotorDistance_Z_Command = new DelegateCommand(this.SelectDistance_Z);
            dv.MotorDistance_S_Command = new DelegateCommand(this.SelectDistance_S);

            dv.AddNeg1Command = new DelegateCommand(this.AddNeg_X);
            dv.AddNeg2Command = new DelegateCommand(this.AddNeg_R);
            dv.AddNeg3Command = new DelegateCommand(this.AddNeg_Z);
            dv.AddNeg4Command = new DelegateCommand(this.AddNeg_S);

            dv.AddPos1Command = new DelegateCommand(this.AddPos_X);
            dv.AddPos2Command = new DelegateCommand(this.AddPos_R);
            dv.AddPos3Command = new DelegateCommand(this.AddPos_Z);
            dv.AddPos4Command = new DelegateCommand(this.AddPos_S);

            dv.JogNeg1Command = new DelegateCommand(this.JogNeg_X);
            dv.JogNeg2Command = new DelegateCommand(this.JogNeg_R);
            dv.JogNeg3Command = new DelegateCommand(this.JogNeg_Z);
            dv.JogNeg4Command = new DelegateCommand(this.JogNeg_S);

            dv.JogPos1Command = new DelegateCommand(this.JogPos_X);
            dv.JogPos2Command = new DelegateCommand(this.JogPos_R);
            dv.JogPos3Command = new DelegateCommand(this.JogPos_Z);
            dv.JogPos4Command = new DelegateCommand(this.JogPos_S);

            d = dv;
        }

        #region Motor Distance
        public void SelectDistance_X()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Distance_X";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Distance_X.Content = Distance_X.Content;
            }
            else
            {
               Distance_X.Content = vs.GetItemValue;
            }

            String Dis = Convert.ToString(Distance_X.Content);

            string rDistance_Xpa = Dis;
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.nDistance", rDistance_Xpa);
        }

        public void SelectDistance_R()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Distance_R";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Distance_R.Content = Distance_R.Content;
            }
            else
            {
                Distance_R.Content = vs.GetItemValue;
            }

            String Dis = Convert.ToString(Distance_R.Content);

            string rDistance_Rpa = Dis;
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.nDistance", rDistance_Rpa);
        }

        public void SelectDistance_Z()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Distance_Z";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Distance_Z.Content = Distance_Z.Content;
            }
            else
            {
                Distance_Z.Content = vs.GetItemValue;
            }

            String Dis = Convert.ToString(Distance_Z.Content);

            string rDistance_Zpa = Dis;
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.nDistance", rDistance_Zpa);
        }

        public void SelectDistance_S()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "Distance_S";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                Distance_S.Content = Distance_S.Content;
            }
            else
            {
                Distance_S.Content = vs.GetItemValue;
            }

            String Dis = Convert.ToString(Distance_S.Content);

            string rDistance_Spa = Dis;
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.nDistance", rDistance_Spa);
        }
        #endregion

        #region AddNeg / AddPos

        private void AddNeg_X()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bAdditiveNeg", true);
        }

        private void AddPos_X()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bAdditivePos", true);
        }

        private void AddNeg_R()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bAdditiveNeg", true);
        }

        private void AddPos_R()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bAdditivePos", true);
        }

        private void AddNeg_Z()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bAdditiveNeg", true);
        }

        private void AddPos_Z()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bAdditivePos", true);
        }

        private void AddNeg_S()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bAdditiveNeg", true);
        }

        private void AddPos_S()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bAdditivePos", true);
        }

        #endregion

        #region JogNeg / JogPos

        private bool JogNegToggle_X = false;
        private bool JogPosToggle_X = false;

        private void JogNeg_X()
        {
            if (JogNegToggle_X == false)
            {
                JogNegToggle_X = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bJogNeg", true);
            }
            else
            {
                JogNegToggle_X = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bJogNeg", false);
            }
        }

        private void JogPos_X()
        {
            if (JogPosToggle_X == false)
            {
                JogPosToggle_X = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bJogPos", true);
            }
            else
            {
                JogPosToggle_X = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bJogPos", false);
            }
        }

        private bool JogNegToggle_R = false;
        private bool JogPosToggle_R = false;

        private void JogNeg_R()
        {
            if (JogNegToggle_R == false)
            {
                JogNegToggle_R = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bJogNeg", true);

                var uriSourceJogNeg = new Uri(@"/imgM500/imgLeftArrow.png", UriKind.Relative);
                btnimgJogNeg2.Source = new BitmapImage(uriSourceJogNeg);
            }
            else
            {
                JogNegToggle_R = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bJogNeg", false);

                var uriSourceJogNeg = new Uri(@"/imgM500/imgLeftArrowDisable.png", UriKind.Relative);
                btnimgJogNeg2.Source = new BitmapImage(uriSourceJogNeg);
            }
        }

        private void JogPos_R()
        {
            if (JogPosToggle_R == false)
            {
                JogPosToggle_R = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bJogPos", true);

                var uriSourceJogPos = new Uri(@"/imgM500/imgRightArrow.png", UriKind.Relative);
                btnimgJogPos2.Source = new BitmapImage(uriSourceJogPos);
            }
            else
            {
                JogPosToggle_R = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bJogPos", false);

                var uriSourceJogPos = new Uri(@"/imgM500/imgRightArrowDisable.png", UriKind.Relative);
                btnimgJogPos2.Source = new BitmapImage(uriSourceJogPos);
            }
        }

        private bool JogNegToggle_Z = false;
        private bool JogPosToggle_Z = false;

        private void JogNeg_Z()
        {
            if (JogNegToggle_Z == false)
            {
                JogNegToggle_Z = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogNeg", true);

                var uriSourceJogNeg = new Uri(@"/imgM500/imgLeftArrow.png", UriKind.Relative);
                btnimgJogNeg3.Source = new BitmapImage(uriSourceJogNeg);
            }
            else
            {
                JogNegToggle_Z = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogNeg", false);

                var uriSourceJogNeg = new Uri(@"/imgM500/imgLeftArrowDisable.png", UriKind.Relative);
                btnimgJogNeg3.Source = new BitmapImage(uriSourceJogNeg);
            }
        }

        private void JogPos_Z()
        {
            if (JogPosToggle_Z == false)
            {
                JogPosToggle_Z = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogPos", true);

                var uriSourceJogPos = new Uri(@"/imgM500/imgRightArrow.png", UriKind.Relative);
                btnimgJogPos3.Source = new BitmapImage(uriSourceJogPos);
            }
            else
            {
                JogPosToggle_Z = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bJogPos", false);

                var uriSourceJogPos = new Uri(@"/imgM500/imgRightArrowDisable.png", UriKind.Relative);
                btnimgJogPos3.Source = new BitmapImage(uriSourceJogPos);
            }
        }

        private bool JogNegToggle_S = false;
        private bool JogPosToggle_S = false;

        private void JogNeg_S()
        {
            if (JogNegToggle_S == false)
            {
                JogNegToggle_S = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogNeg", true);

                var uriSourceJogNeg = new Uri(@"/imgM500/imgLeftArrow.png", UriKind.Relative);
                btnimgJogNeg4.Source = new BitmapImage(uriSourceJogNeg);
            }
            else
            {
                JogNegToggle_S = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogNeg", false);

                var uriSourceJogNeg = new Uri(@"/imgM500/imgLeftArrowDisable.png", UriKind.Relative);
                btnimgJogNeg4.Source = new BitmapImage(uriSourceJogNeg);
            }
        }

        private void JogPos_S()
        {
            if (JogPosToggle_S == false)
            {
                JogPosToggle_S = true;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogPos", true);

                var uriSourceJogPos = new Uri(@"/imgM500/imgRightArrow.png", UriKind.Relative);
                btnimgJogPos4.Source = new BitmapImage(uriSourceJogPos);
            }
            else
            {
                JogPosToggle_S = false;

                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bJogPos", false);

                var uriSourceJogPos = new Uri(@"/imgM500/imgRightArrowDisable.png", UriKind.Relative);
                btnimgJogPos4.Source = new BitmapImage(uriSourceJogPos);
            }
        }
        #endregion

        #region M500 Motor Home

        private void Motor_X_Home()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Param.rPosition", 0);

            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[0].Command.bAbsolute", true);
        }

        private void Motor_R_Home()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Param.rPosition", 0);

            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[1].Command.bAbsolute", true);
        }

        private void Motor_Z_Home()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Param.rPosition", 0);

            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[2].Command.bAbsolute", true);
        }

        private void Motor_S_Home()
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Param.rPosition", 0);

            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Axes[3].Command.bAbsolute", true);
        }

        #endregion



    }
}
