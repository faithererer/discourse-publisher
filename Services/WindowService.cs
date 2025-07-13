using DiscoursePublisher.ViewModels;
using DiscoursePublisher.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DiscoursePublisher.Services
{
    public class WindowService : IWindowService
    {
        public void ShowMetaInfoWindow()
        {
            var metaInfoView = new MetaInfoQueryView
            {
                DataContext = App.Services!.GetService<MetaInfoQueryViewModel>()
            };
            metaInfoView.Show();
        }

        public void ShowSettingsWindow()
        {
            var settingsView = new SettingsView
            {
                DataContext = App.Services!.GetService<SettingsViewModel>()
            };
            settingsView.ShowDialog();
        }
    }
}