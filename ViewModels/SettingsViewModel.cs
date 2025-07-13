using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiscoursePublisher.Models;
using DiscoursePublisher.Services;
using System;

namespace DiscoursePublisher.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly SettingsService _settingsService;

        [ObservableProperty]
        private string? _discourseUrl;

        [ObservableProperty]
        private string? _apiKey;

        [ObservableProperty]
        private string? _apiUsername;

        public Action? CloseAction { get; set; }

        public SettingsViewModel(SettingsService settingsService)
        {
            _settingsService = settingsService;
            var settings = _settingsService.LoadSettings();
            if (settings != null)
            {
                DiscourseUrl = settings.DiscourseUrl;
                ApiKey = settings.ApiKey;
                ApiUsername = settings.ApiUsername;
            }
        }

        [RelayCommand]
        private void Save()
        {
            var settings = new AppSettings
            {
                DiscourseUrl = DiscourseUrl ?? string.Empty,
                ApiKey = ApiKey ?? string.Empty,
                ApiUsername = ApiUsername ?? string.Empty
            };
            _settingsService.SaveSettings(settings);
            CloseAction?.Invoke();
        }
    }
}