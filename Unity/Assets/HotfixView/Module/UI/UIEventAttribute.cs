using System;
using ET;

namespace ETHotfix
{
    public class UIEventAttribute: BaseAttribute
    {
        public string UIType { get; }

        public UIEventAttribute(string uiType)
        {
            this.UIType = uiType;
        }
    }
}