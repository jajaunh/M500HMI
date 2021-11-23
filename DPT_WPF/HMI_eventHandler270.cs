
using System;
using System.Windows;
using System.Windows.Threading;

namespace DPT_WPF
{
    public partial class HMIM270
    {
        int activeServerSubscriptionHandle;
        int activeClientSubscriptionHandle;
        string SubscriptionUpdateRate = "1000";
        string SubscriptionDeadband = "0";
        bool SubscriptionActiveState = true;

        private bool IsSubscriptionUpdateRateValid()
        {
            // Validate value:
            bool isValid = false;
            int updateRate = -1;

            if (int.TryParse(SubscriptionUpdateRate, out updateRate))
            {
                if (updateRate >= 0 && updateRate <= int.MaxValue)
                {
                    isValid = true;
                }
            }

            // Issue error message:
            if (isValid == false)
            {
                MessageBox.Show("Please enter an update rate between 0 and " + int.MaxValue + " MS.");
            }

            // Set return value:
            return isValid;
        }
        private bool IsSubscriptionDeadbandValid()
        {
            // Validate value:
            bool isValid = false;
            int deadband = -1;

            if (int.TryParse(SubscriptionDeadband, out deadband))
            {
                if (deadband >= 0 && deadband <= 100)
                {
                    isValid = true;
                }
            }

            // Issue error message:
            if (isValid == false)
            {
                MessageBox.Show("Please enter an deadband value between 0 and 100.");
            }

            // Set return value:
            return isValid;
        }       
    }
}
