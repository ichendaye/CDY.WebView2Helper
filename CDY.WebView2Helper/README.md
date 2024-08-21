# Webview2扩展方法

## 自定义用户数据文件夹来初始化WebView2
```csharp
var env = await webView2.CreateEnvironmentToUserDataFolderAsync("自定义数据文件夹名称");
await webView2.EnsureCoreWebView2Async(env);
```

## 开启窗口移动

```charp
webview2.EnableMoveWindow()
```

## 清理webview2缓存

```csharp
webview2.ClearCache("自定义的数据文件夹名称")
```

## 获取webview2内核版本

```csharp
var version=webview2.GetVerion()
```

## 授权使用麦克风

> 使用麦克风的时候让网页不弹授权窗口，自动授权

```csharp
webview2.AuthMicrophone()
```

## 禁用右键菜单

```csharp
webview2.DisableContextMenu()
```

## 禁用手势滑动导航

```csharp
webview2.DisableSwipeNavigation()
```

## 将本地目录映射为站点访问

```csharp
webView2.CoreWebView2.HostNameMapping("local.livehelper.local", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot"));
```

## 禁用浏览器默认的快捷键

```csharp
webview2.DisableWebviewKeys()
```

具体禁用的快捷键

- Ctrl+F 和 F3 用于在页面上查找
- Ctrl+P for Print Ctrl+P 用于打印
- Ctrl+R 和 F5 用于重新加载
- Ctrl+加号和 Ctrl+减号用于缩放
- Ctrl+Shift-C 和 F12 用于开发工具
- 用于浏览器功能的特殊键，例如后退、前进和搜索
