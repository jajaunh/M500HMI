
using OpcLabs.EasyOpc.UA;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DPT_WPF.ucCommon
{
    /// <summary>
    /// ucCommonGAS.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonGAS : UserControl
    {
        private readonly UAEndpointDescriptor m500opcuri = "opc.tcp://192.168.137.1:4840/";
        EasyUAClient client = new EasyUAClient();
        DefineValue d;
        
        ValueSelect vs = new ValueSelect();

        bool ValveCheck = false;
        bool GasCheck1 = false;
        bool GasCheck2 = false;

        public ucCommonGAS()
        {
            InitializeComponent();
        }

        public ucCommonGAS(DefineValue dv)
        {
            InitializeComponent();

          
            dv.SelArCommand = new DelegateCommand(this.SelAr);
            dv.SelN2Command = new DelegateCommand(this.SelN2);
            dv.GasSupplyCommand = new DelegateCommand(this.GasSupply);
            dv.MaxOxyCommand = new DelegateCommand(this.MaxOxy);
            dv.MinOxyCommand = new DelegateCommand(this.MinOxy);
            d = dv;
        }
        
        private void GasSupply()
        {
            if(ValveCheck == false)
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bValveOn", true);

                ValveCheck = true;
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Command.bValveOff", true);

                ValveCheck = false;
            }
            
        }

        private void SelAr()
        {
            if(GasCheck1 == false)
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.stGasType", 2);//아르곤가스 주입
                
                GasCheck1 = true;
                GasCheck2 = false;
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.stGasType", 0);//주입 중지

                GasCheck1 = false;
                GasCheck2 = false;
            }
        }

        private void SelN2()
        {
            if (GasCheck2 == false)
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.stGasType", 1);//질소 주입

                GasCheck1 = false;
                GasCheck2 = true;
            }
            else
            {
                (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.stGasType", 0);//주입 중지

                GasCheck1 = false;
                GasCheck2 = false;
            }
        }

        private void MaxOxy()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "MaxOxyValue";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                MaxOxyValue.Content = (client).ReadValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.rMaxOxygenConcentration").ToString()
;

            }
            else
            {
                MaxOxyValue.Content = vs.GetItemValue;

                double MaxOxy = Convert.ToDouble(MaxOxyValue.Content);

                if (MaxOxy > 0.1)
                {
                    MessageBox.Show("최댓값을 넘었습니다.");
                }
                else
                {
                    string rMaxOxygenConcentration_Chamber = Convert.ToString(MaxOxy);
                    (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.rMaxOxygenConcentration", rMaxOxygenConcentration_Chamber);
                }
            }
        }

        private void MinOxy()
        {
            vs = new ValueSelect();

            vs.SetItemValue = "MinOxyValue";
            vs.ShowDialog();

            if (vs.closeCheck == true)
            {
                MinOxyValue.Content = d.OPCItemValueTextBoxes[394];
            }
            else
            {
                MinOxyValue.Content = vs.GetItemValue;
                
                double MinOxy = Convert.ToDouble(MinOxyValue.Content);

                if (MinOxy < 0.05)
                {
                    MessageBox.Show("최솟값을 넘었습니다.");
                }
                else
                {
                    string rMinOxygenConcentration_Chamber = Convert.ToString(MinOxy);
                    (client).WriteValue(m500opcuri, "ns=6;s=::AsGlobalPV:gxGrpChamberMan.Input.Configuration.rMinOxygenConcentration", rMinOxygenConcentration_Chamber);
                }
            }
        }
    }
}
