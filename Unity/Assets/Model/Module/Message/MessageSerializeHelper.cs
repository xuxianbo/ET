using System;
using System.IO;
using MongoDB.Bson.IO;

namespace ET
{
    public static class MessageSerializeHelper
    {
        public static MemoryStream GetStream(int count = 0)
        {
            MemoryStream stream;
            if (count > 0)
            {
                stream = new MemoryStream(count);
            }
            else
            {
                stream = new MemoryStream();
            }

            return stream;
        }
    }
}