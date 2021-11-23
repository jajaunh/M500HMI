using System;
using System.Collections.Generic;
using System.Windows;

namespace DPT_WPF
{
    /// <summary>
    /// ValueSelect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ValueSelect : Window
    {
        public ValueSelect()
        {
            InitializeComponent();
        }

        string InItemValue = string.Empty;
        string OutItemValue = string.Empty;
        public bool closeCheck = false;

        public string SetItemValue
        {
            set { InItemValue = value; }
        }

        public string GetItemValue
        {
            get { return OutItemValue; }
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String selectedValue = "0";

                if (sender == ZeroBtn)
                {
                    selectedValue = "0";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == oneBtn)
                {
                    selectedValue = "1";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == twoBtn)
                {
                    selectedValue = "2";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == threeBtn)
                {
                    selectedValue = "3";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == fourBtn)
                {
                    selectedValue = "4";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == fiveBtn)
                {
                    selectedValue = "5";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == sixBtn)
                {
                    selectedValue = "6";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == sevenBtn)
                {
                    selectedValue = "7";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == eightBtn)
                {
                    selectedValue = "8";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

                if (sender == nineBtn)
                {
                    selectedValue = "9";

                    if (resultValue.Content.ToString() == "0")
                    {
                        resultValue.Content = selectedValue;
                    }
                    else
                    {
                        resultValue.Content += selectedValue;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dotBtn_Click(object sender, RoutedEventArgs e)
        {
            Boolean a;

            a = resultValue.Content.ToString().Contains(".");

            if (resultValue.Content.ToString() == "0")
            {
                resultValue.Content = "0.";
            }
            else
            {
                if (a == false)
                {
                    resultValue.Content += ".";
                }
            }
        }

        private void escBtn_Click(object sender, RoutedEventArgs e)
        {
            closeCheck = true;
            this.Close();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToString(resultValue.Content).Length == 1)
                {
                    resultValue.Content = "0";
                }
                else
                {
                    resultValue.Content = Convert.ToString(resultValue.Content).Substring(0, Convert.ToString(resultValue.Content).Length - 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            OutItemValue = resultValue.Content.ToString();
            this.Close();

        }

    }
}
