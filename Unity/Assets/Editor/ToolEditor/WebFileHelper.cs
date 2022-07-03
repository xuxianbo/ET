using System.IO;
using UnityEditor;

namespace ET
{
    public static class WebFileHelper
    {
        [MenuItem("Tools/ETEditor_WebFileServer")]
        public static void OpenFileServer()
        {
            ProcessHelper.Run("dotnet", "FileServer.dll", "../FileServer/");
        }
    }
}