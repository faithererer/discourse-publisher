using DiscoursePublisher.Services;
using DiscoursePublisher.ViewModels;
using DiscoursePublisher.Views;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace DiscoursePublisher
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        public App()
        {
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            IServiceCollection services = new ServiceCollection();

            // Load .env file
            Env.Load();

            // Register services
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<SettingsService>();

            services.AddHttpClient<DiscourseApiService>((serviceProvider, client) =>
            {
                var settingsService = serviceProvider.GetRequiredService<SettingsService>();
                var settings = settingsService.LoadSettings();

                string? discourseUrl = settings?.DiscourseUrl;
                string? apiKey = settings?.ApiKey;
                string? apiUsername = settings?.ApiUsername;

                // Fallback to .env file for developers
                if (string.IsNullOrEmpty(discourseUrl)) discourseUrl = Env.GetString("DISCOURSE_URL");
                if (string.IsNullOrEmpty(apiKey)) apiKey = Env.GetString("API_KEY");
                if (string.IsNullOrEmpty(apiUsername)) apiUsername = Env.GetString("API_USERNAME");

                if (string.IsNullOrEmpty(discourseUrl) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUsername))
                {
                    MessageBox.Show("请通过“设置”菜单配置论坛信息，或为开发者在 .env 文件中提供配置。", "配置错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    // We cannot throw here as it's part of the DI setup.
                    // The service itself will handle the invalid state.
                    return;
                }

                client.BaseAddress = new Uri(discourseUrl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
                client.DefaultRequestHeaders.Add("Api-Username", apiUsername);
            });

            // Register ViewModels
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<MetaInfoQueryViewModel>();
            services.AddTransient<SettingsViewModel>();

            // Register Views
            services.AddSingleton<MainWindow>(sp => new MainWindow
            {
                DataContext = sp.GetRequiredService<MainWindowViewModel>()
            });

            Services = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = Services!.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = $"发生未处理的异常: \n\n{e.Exception}";
            try
            {
                System.IO.File.WriteAllText("crashlog.txt", errorMessage);
                MessageBox.Show("程序遇到严重错误，已将日志写入 crashlog.txt 文件。", "程序崩溃", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                // Failsafe
            }
            e.Handled = true;
        }
    }
}