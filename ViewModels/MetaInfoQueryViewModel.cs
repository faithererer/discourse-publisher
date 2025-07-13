using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiscoursePublisher.Models;
using DiscoursePublisher.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace DiscoursePublisher.ViewModels
{
    public partial class MetaInfoQueryViewModel : ObservableObject
    {
        private readonly DiscourseApiService _discourseApiService;

        [ObservableProperty]
        private ObservableCollection<Category> _categories = new();

        [ObservableProperty]
        private ObservableCollection<Tag> _tags = new();

        [ObservableProperty]
        private bool _isLoading;

        public MetaInfoQueryViewModel(DiscourseApiService discourseApiService)
        {
            _discourseApiService = discourseApiService;
            LoadMetaInfoCommand = new AsyncRelayCommand(LoadMetaInfoAsync);
        }

        public IAsyncRelayCommand LoadMetaInfoCommand { get; }

        private async Task LoadMetaInfoAsync()
        {
            IsLoading = true;
            try
            {
                var categories = await _discourseApiService.GetCategoriesAsync();
                Categories = new ObservableCollection<Category>(categories);

                var tags = await _discourseApiService.GetTagsAsync();
                Tags = new ObservableCollection<Tag>(tags);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"加载元信息时发生错误: {ex.Message}", "加载失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}