using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiscoursePublisher.Models;
using DiscoursePublisher.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace DiscoursePublisher.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PublishAllCommand))]
        private ObservableCollection<Post> _posts = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PublishAllCommand))]
        private bool _isNotPublishing = true;

        [ObservableProperty]
        private double _publishProgress;

        [ObservableProperty]
        private ObservableCollection<string> _publishLogs = new();

        private readonly DiscourseApiService _discourseApiService;
        private readonly IWindowService _windowService;

        public MainWindowViewModel(DiscourseApiService discourseApiService, IWindowService windowService)
        {
            _discourseApiService = discourseApiService;
            _windowService = windowService;
        }

        [RelayCommand]
        private async Task ImportPostsAsync()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "支持的文件 (*.json)|*.json|All files (*.*)|*.*",
                Title = "请选择要导入的帖子文件"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var importer = ImporterFactory.GetImporter(openFileDialog.FileName);
                if (importer == null)
                {
                    MessageBox.Show("不支持的文件格式。", "导入失败", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    var importedPosts = await importer.ImportAsync(openFileDialog.FileName);
                    Posts = new ObservableCollection<Post>(importedPosts);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"导入文件时发生错误: {ex.Message}", "导入失败", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanPublish))]
        private async Task PublishAllAsync()
        {
            IsNotPublishing = false;
            PublishLogs.Clear();
            PublishProgress = 0;

            var pendingPosts = Posts.Where(p => p.Status == "Pending").ToList();
            int total = pendingPosts.Count;
            int processed = 0;

            foreach (var post in pendingPosts)
            {
                if (post.CategoryId == 0)
                {
                    post.Status = "Skipped";
                    PublishLogs.Add($"[已跳过]: {post.Title} - 类别ID不能为0。");
                    processed++;
                    PublishProgress = (double)processed / total * 100;
                    continue;
                }

                try
                {
                    PublishLogs.Add($"[正在发布]: {post.Title}");
                    using var response = await _discourseApiService.CreatePostAsync(post.Title, post.Content, post.CategoryId, post.Tags.ToArray());
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic? jsonResponse = JsonConvert.DeserializeObject(responseBody);
                        post.Status = "Published";
                        PublishLogs.Add($"  [成功]: {post.Title} (ID: {jsonResponse?.id})");
                    }
                    else
                    {
                        post.Status = "Failed";
                        PublishLogs.Add($"  [失败]: {post.Title} - {response.ReasonPhrase} - {responseBody}");
                    }
                }
                catch (System.Exception ex)
                {
                    post.Status = $"Failed";
                    PublishLogs.Add($"  [异常]: {post.Title} - {ex.Message}");
                }
                finally
                {
                    processed++;
                    PublishProgress = (double)processed / total * 100;
                }
            }

            IsNotPublishing = true;
            PublishLogs.Add("--- 发布流程结束 ---");
            HandyControl.Controls.MessageBox.Show("所有待处理的帖子都已处理完毕，请查看日志获取详细信息。", "发布完成");
        }

        private bool CanPublish()
        {
            return IsNotPublishing && Posts.Any(p => p.Status == "Pending");
        }

        [RelayCommand]
        private void ShowMetaInfo()
        {
            _windowService.ShowMetaInfoWindow();
        }

        [RelayCommand]
        private void ShowSettings()
        {
            _windowService.ShowSettingsWindow();
        }
    }
}