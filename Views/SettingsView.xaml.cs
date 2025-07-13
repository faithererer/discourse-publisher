using DiscoursePublisher.ViewModels;

namespace DiscoursePublisher.Views
{
    public partial class SettingsView : HandyControl.Controls.Window
    {
        public SettingsView()
        {
            InitializeComponent();

            if (DataContext is SettingsViewModel vm)
            {
                vm.CloseAction = this.Close;
            }
        }
    }
}