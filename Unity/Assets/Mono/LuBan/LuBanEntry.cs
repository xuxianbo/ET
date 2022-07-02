// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月2日, 星期六
// --------------------------

using Bright.Serialization;
using SimpleJSON;
using YooAsset;

namespace ET
{
    public static class LuBanEntry
    {
        public static async ETTask<JSONNode> LoadJsonBuf(string fileName)
        {
            RawFileOperation result = await YooAssetProxy.GetRawFileAsync($"Config_{fileName}");
            return JSON.Parse(result.LoadFileText());
        }

        public static async ETTask<ByteBuf> LoadBytesBuf(string fileName)
        {
            RawFileOperation result = await YooAssetProxy.GetRawFileAsync($"Config_{fileName}");
            return new ByteBuf(result.LoadFileData());
        }
    }
}