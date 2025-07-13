using HandyControl.Controls;
using System.Windows;

namespace DiscoursePublisher.Views
{
    /// <summary>
    /// Interaction logic for MetaInfoQueryView.xaml
    /// </summary>
    public partial class MetaInfoQueryView : HandyControl.Controls.Window
    {
        public MetaInfoQueryView()
        {
            InitializeComponent();
            Loaded += MetaInfoQueryView_Loaded;
        }

        private async void MetaInfoQueryView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MetaInfoQueryViewModel vm)
            {
                await vm.LoadMetaInfoCommand.ExecuteAsync(null);
            }
        }
    }
}