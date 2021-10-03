using System;
using System.ComponentModel;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
#if !SERVER
    //ILRT 里不能用适配器IDisposable，会导致PB解析错误
    public interface IDisposable
    {
        void Dispose();
    }
#endif
    
    public interface ISupportInitialize
    {
        void BeginInit();
        void EndInit();
    }
    
    public abstract class Object: ISupportInitialize, IDisposable
    {
        public virtual void BeginInit()
        {
        }
        
        public virtual void EndInit()
        {
        }

        public virtual void Dispose()
        {
        }
        
        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}