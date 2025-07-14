# Discourse Publisher

一个使用 WPF 和 .NET 8 构建的现代化桌面应用程序，用于批量向 Discourse 论坛发布帖子。

## ✨ 功能特性

- **批量发布**: 通过导入 JSON 文件，一次性发布多个帖子。
- **元信息查询**: 内置工具，可方便地查询论坛的所有分类 (Categories) 和标签 (Tags) 及其对应的 ID。
- **美观的 UI**: 使用 [HandyControl](https://handyorg.github.io/handycontrol/) UI 库，提供流畅、美观的用户体验。
- **配置持久化**: 自动保存您的论坛地址和 API 凭据，无需每次都输入。
- **可扩展的设计**: 采用 MVVM 架构和工厂模式，未来可轻松扩展以支持 Markdown、CSV 等更多导入格式。
- **详细的日志**: 在发布过程中提供实时的进度条和详细的成功/失败日志。
## 👀 界面截图
<img width="1000" height="600" alt="PixPin_2025-07-14_14-23-54" src="https://github.com/user-attachments/assets/8674b01d-c9bd-47b6-946a-0329ec4f942b" />
<img width="790" height="496" alt="PixPin_2025-07-14_14-24-34" src="https://github.com/user-attachments/assets/1cb92aa3-16c6-4974-a709-5e50704e77b2" />

## 🚀 如何开始

### 1. 配置

首次启动时，或通过主界面的“设置”按钮，配置以下信息：

- **论坛地址 (URL)**: 您的 Discourse 论坛的完整地址 (例如, `https://forums.example.com`)。
- **API Key**: 在您的 Discourse 用户后台生成的 API 密钥。
- **API 用户名**: 您用于生成 API 密钥的用户名。

配置将自动保存在 `%AppData%\DiscoursePublisher\settings.json`。


### 2. 准备帖子数据

创建一个 `.json` 文件，其中包含一个帖子对象的数组。每个帖子对象应包含以下字段：

- `title` (string): 帖子标题。
- `content` (string): 帖子内容，支持 Markdown。
- `categoryId` (int): 帖子的分类 ID。您可以通过“查询论坛信息”功能获取。
- `tags` (array of strings): 帖子的标签列表。

**示例 `posts.json`:**
```json
[
  {
    "title": "探索WPF：构建现代化桌面应用的艺术",
    "content": "本文将深入探讨 **WPF (Windows Presentation Foundation)** 的核心概念...",
    "categoryId": 10,
    "tags": ["wpf", "csharp", "mvvm"]
  },
  {
    "title": "HandyControl：让你的WPF应用焕然一新",
    "content": "介绍一个功能强大且美观的开源 WPF UI 库...",
    "categoryId": 10,
    "tags": ["wpf", "ui", "opensource"]
  }
]
```

### 3. 使用

1.  启动应用程序。
2.  点击 **"导入帖子 (JSON)"** 按钮并选择您准备好的 `.json` 文件。
3.  帖子列表将显示在主网格中。
4.  点击 **"发布所有待处理帖子"** 按钮开始发布。
5.  在下方的日志区域查看详细的发布结果。

## 🛠️ 开发

本项目使用 .NET 8 和 WPF 构建。

- **构建**: `dotnet build`
- **运行**: `dotnet run`

### 项目结构

- **`/Models`**: 包含应用程序的数据模型 (e.g., `Post`, `Category`, `AppSettings`)。
- **`/Views`**: 包含所有的窗口和用户控件 (XAML)。
- **`/ViewModels`**: 包含所有视图的视图模型，处理业务逻辑和状态管理。
- **`/Services`**: 包含与外部服务（如 Discourse API、文件系统）交互的类。
- **`/Converters`**: 包含在 XAML 绑定中使用的数据转换器。
