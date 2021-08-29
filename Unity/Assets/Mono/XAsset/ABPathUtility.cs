//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2020年10月14日 22:27:15
//------------------------------------------------------------

namespace ET
{
    /// <summary>
    /// AB实用函数集，主要是路径拼接
    /// </summary>
    public class XAssetPathUtilities
    {
        public static string GetTexturePath(string fileName)
        {
            return $"Assets/Bundles/Altas/{fileName}.prefab";
        }

        public static string GetFGUIDesPath(string fileName)
        {
            return $"Assets/Res/FGUI/{fileName}.bytes";
        }
        
        public static string GetFGUIResPath(string fileName,string extension)
        {
            return $"Assets/Res/FGUI/{fileName}{extension}";
        }
        
        public static string GetNormalConfigPath(string fileName)
        {
            return $"Assets/Res/Config/Config/{fileName}.bytes";
        }
        
        public static string GetSoundPath(string fileName)
        {
            return $"Assets/Bundles/Sounds/{fileName}.prefab";
        }
        
        public static string GetSkillConfigPath(string fileName)
        {
            return $"Assets/Bundles/SkillConfigs/{fileName}.prefab";
        }
        
        public static string GetUnitPath(string fileName)
        {
            return $"Assets/Res/Unit/{fileName}.prefab";
        }
        
        public static string GetScenePath(string fileName)
        {
            return $"Assets/Res/Scenes/{fileName}.unity";
        }
        
        public static string GetHotfixDllPath(string fileName)
        {
            return $"Assets/Res/Code/{fileName}.dll.bytes";
        }
        
        public static string GetHotfixPdbPath(string fileName)
        {
            return $"Assets/Res/Code/{fileName}.pdb.bytes";
        }
    }
}