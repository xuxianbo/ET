using System.IO;

using UnityEditor;

namespace ET
{
    public static class WebFileHelper
    {
        [MenuItem("Tools/web资源服务器")]
        public static void OpenFileServer()
        {
            ProcessHelper.Run("dotnet", "FileServer.dll", "../FileServer/");
        }
    }
}
