using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace CDY.WebView2Helper
{
    /// <summary>
    /// WebView2 扩展方法
    /// </summary>
    public static class WebView2Extend
    {
        /// <summary>
        /// 创建WebView2环境并指定数据目录到AppData中
        /// </summary>
        /// <param name="webView">WebView2对象</param>
        /// <param name="cacheFolder">数据目录名称</param>
        /// <returns>返回WebView2环境对象</returns>
        public static async Task<CoreWebView2Environment> CreateUserDataFolderEnvironmentAsync(this WebView2 webView, string cacheFolder)
        {
            // 设置新的数据目录到用户本地应用数据
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), cacheFolder);
            if (!Directory.Exists(userDataFolder))
            {
                Directory.CreateDirectory(userDataFolder);
            }

            // 创建WebView2环境并指定用户数据目录
            return await CoreWebView2Environment.CreateAsync(null, userDataFolder);
        }

        /// <summary>
        /// 开启窗口移动
        /// css使用-webkit-app-region: drag;
        /// </summary>
        /// <param name="webView"></param>
        public static void EnableMoveWindow(this WebView2 webView)
        {
            webView.CoreWebView2.Settings.IsNonClientRegionSupportEnabled = true;
        }

        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <param name="cacheFolder">数据目录名称</param>
        public static void ClearCache(string cacheFolder)
        {
            var userDataFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), cacheFolder);
            if (Directory.Exists(userDataFolder))
            {
                try
                {
                    Directory.Delete(userDataFolder, true);
                }
                catch
                {
                    // 可能需要记录错误或者通知用户
                }
            }
        }

        /// <summary>
        /// 获取WebView内核版本
        /// </summary>
        /// <returns></returns>
        public static Version GetVerion()
        {
            return new Version(CoreWebView2Environment.GetAvailableBrowserVersionString().Split(' ')[0]);
        }

        /// <summary>
        /// 授权使用麦克风
        /// </summary>
        /// <param name="webView"></param>
        public static void AuthMicrophone(this WebView2 webView)
        {
            webView.CoreWebView2.PermissionRequested += (s, e) =>
            {
                if (e.PermissionKind == CoreWebView2PermissionKind.Microphone)
                {
                    e.State = CoreWebView2PermissionState.Allow;
                }
            };
        }

        /// <summary>
        /// 禁用右键菜单
        /// </summary>
        /// <param name="webView"></param>
        public static void DisableContextMenu(this WebView2 webView)
        {
            webView.CoreWebView2.ContextMenuRequested += (s, e2) =>
            {
                e2.Handled = true;
            };
        }

        /// <summary>
        /// 禁用手势滑动导航
        /// </summary>
        /// <param name="webView"></param>
        public static void DisableSwipeNavigation(this WebView2 webView)
        {
            webView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
        }

        /// <summary>
        /// 将本地目录映射为站点
        /// </summary>
        /// <param name="coreWebView2"></param>
        /// <param name="hostName">本地域名地址，尽量特殊一点。比如：local.chendaye.local</param>
        /// <param name="localFolderPath">本地目录地址,完整路径</param>
        public static void HostNameMapping(this CoreWebView2 coreWebView2, string hostName, string localFolderPath)
        {
            coreWebView2.SetVirtualHostNameToFolderMapping(
                hostName,
                localFolderPath,
                CoreWebView2HostResourceAccessKind.Allow
            );
        }
    }
}