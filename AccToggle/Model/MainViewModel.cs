using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AccToggle
{
    public class MainViewModel : BaseViewModel
    {

        public event EventHandler OnEnabled;

        public bool IsEnabled
        {
            get
            {
                uint result = InteropSvc.Interop.Instance.GetPower("ACC1:", 1);
                return (result == 4) ? false : true;
            }
            set
            {
                InteropSvc.Interop.Instance.EnableUiOrientationChange(value);
                InteropSvc.Interop.Instance.SetPower("ACC1:", 1, (value == true) ? 0U : 4U);
                OnChange("IsEnabled");
                if (OnEnabled != null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(delegate()
                    {
                        OnEnabled(this, new EventArgs());
                    });
                }
            }
        }
    }
}
