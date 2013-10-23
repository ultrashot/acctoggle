using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace AccToggle
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainViewModel viewModel
        {
            get
            {
                return this.DataContext as MainViewModel;
            }
        }

        public MainPage()
        {
            InitializeComponent();
            InteropSvc.Interop.Initialize();
            if (InteropSvc.Interop.Instance.HasRootAccess() == false)
            {
                MessageBox.Show(LocalizedResources.NoRootAccess, LocalizedResources.Error, MessageBoxButton.OK);
                throw new Exception("Quit");
            }
            viewModel.OnEnabled += new EventHandler(viewModel_OnEnabled);
            if (viewModel.IsEnabled)
            {
                gridNotice.Opacity = 0;
            }
            else
            {
                DisableAccelerometer.Begin();
            }
            viewModel_OnEnabled(null, null);
        }

        private void SetToggleStateTranslation(ToggleSwitch ts)
        {
            ts.Content = (ts.IsChecked == true) ?
                            LocalizedResources.On :
                            LocalizedResources.Off;
        }

        void viewModel_OnEnabled(object sender, EventArgs e)
        {
            bool isEnabled = viewModel.IsEnabled;
            toggleState.IsChecked = isEnabled;
            if (isEnabled == false)
                DisableAccelerometer.Begin();
            else
                EnableAccelerometer.Begin();
            SetToggleStateTranslation(toggleState);
        }

        private void grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pageAbout.xaml", UriKind.Relative));
        }

    }
}