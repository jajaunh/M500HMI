
using OpcLabs.EasyOpc.UA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// motorError.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class motorError : Window
    {
        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        int errorCheck = 0;
        DefineValue d;       
        HMIM270 hmi270;

        public motorError()
        {
            InitializeComponent();
        }
        public void initalSetting(DefineValue dv, int errorName, HMIM270 hmi270n)
        {
            InitializeComponent();
           

                if (errorName == 1)
                {
                errorCheck = 1;
                    errorTitle.Content = "Machine Error";
                errorMessage1.Content = "";
                errorMessage2.Content = "";
                errorMessage3.Content = "";
                errorMessage4.Content = "";
                errorCount.Content = "";
                errorNumber.Content = "";
                erOk.Content = "OK";
                allErOk.Content = "All OK";

            }
                else if (errorName == 2)
                {
                errorCheck = 2;
                    errorTitle.Content = "System Error";
                    errorCount.Content = "카운트 : " + (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.nCount").ToString();
                    errorNumber.Content = "넘버 : " + (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.dNumber").ToString();
                    errorMessage1.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.Text.strLine1").ToString();
                    errorMessage2.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.Text.strLine2").ToString();
                    errorMessage3.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.Text.strLine3").ToString();
                    errorMessage4.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Output.Alarm.Monitor.System.ErrorInfo.Text.strLine4").ToString();
                erOk.Content = "Reset";
                allErOk.Content = "All Reset";
                }
                else if (errorName == 3)
                {
                int infoNumber = 0;
                infoNumber = Convert.ToInt32(dv.OPCItemValueTextBoxes[202]);
                errorCheck = 3;
                    errorTitle.Content = "Information";
                errorMessage1.Content = "";
                if (infoNumber == 10)
                {
                    errorMessage2.Content = "서보 모터의 호밍이 완료되었습니다.";
                }
                else if (infoNumber == 11)
                {
                    errorMessage2.Content = "서보 모터의 호밍 순서 데이터가 0입니다.";
                }
                else if (infoNumber == 23)
                {
                    errorMessage2.Content = "데이터가 변경 되었습니다!";
                }
                else if (infoNumber == 30)
                {
                    errorMessage2.Content = "서보 모터가 호밍이 되어 있지 않습니다.";
                }
                else if (infoNumber == 32)
                {
                    errorMessage2.Content = "장비가 운행중입니다! 해당 기능은 장비를 멈춘 후 사용 해주십시오.";
                }
                else if (infoNumber == 33)
                {
                    errorMessage2.Content = "오토모드 운행이 완료되었습니다.";
                }
                else if (infoNumber == 34)
                {
                    errorMessage2.Content = "기계가 준비되었습니다. Job file과 기계 상태를 확인하고 동작 시키십시오.";
                }
                else if (infoNumber == 35)
                {
                    errorMessage2.Content = "프린트가 완료되었습니다. 기계 상태를 확인하고 도킹 해제를 시키십시오.";
                }
                else if (infoNumber == 40)
                {
                    errorMessage2.Content = "도킹시 불활성 가스 압력이 낮습니다.(N2 or Ar 가스 주입 확인 필요)";
                }
                else if (infoNumber == 64)
                {
                    errorMessage2.Content = "실행중지! 동작 영역을 벗어났습니다.";
                }
                else if (infoNumber == 65)
                {
                    errorMessage2.Content = "실행중지! 동작 영역을 벗어났습니다.";
                }
                else if (infoNumber == 66)
                {
                    errorMessage2.Content = "실행중지! 동작 영역 OFF한 후 실행하십시오.";
                }
                else if (infoNumber == 67)
                {
                    errorMessage2.Content = "실행중지! 동일한 축으로 설정 되었습니다.";
                }
                else if (infoNumber == 68)
                {
                    errorMessage2.Content = "실행중지! 이 포인트에서 축 변경을 할 수 없습니다.";
                }
                else
                {
                    errorMessage2.Content = "";
                }
                errorMessage3.Content = "";
                errorMessage4.Content = "";
                errorCount.Content = "";
                errorNumber.Content = "";
                erOk.Content = "OK";
                allErOk.Content = "All OK";
            }

            d = dv;
            hmi270 = hmi270n;
            
        }
        private void gBtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            hmi270.alramPoint = 1;
            this.Close();
        }

        private void erOk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (errorCheck==1)
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Alarm.bAck", true);
            }
            else if(errorCheck==2)
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Alarm.bAck", true);
            }
            else if(errorCheck==3)
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Alarm.bAck", true);
            }
            Thread.Sleep(2000);
            hmi270.alramPoint = 0;
            this.Close();
        }

        private void allErOk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gIntp.Input.Alarm.bAckAll", true);
            Thread.Sleep(2000);
            hmi270.alramPoint = 0;
            this.Close();
        }
    }
}
