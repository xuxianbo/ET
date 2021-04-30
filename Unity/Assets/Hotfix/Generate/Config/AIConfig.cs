using System;
using System.Collections.Generic;
using ET;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ETHotfix
{
    [ProtoContract]
    [Config]
    public partial class AIConfigCategory : ProtoObject
    {
        public static AIConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, AIConfig> dict = new Dictionary<int, AIConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private AIConfig[] list = new AIConfig[10];
		
        public AIConfigCategory()
        {
            Instance = this;
        }
        
        public void AfterDeserialization()
        {
            if (this.list == null)
            {
                Log.Info("????????????????????????????????????????????????????????????????");
            }
            foreach (AIConfig config in list)
            {
                Log.Info(config.Id.ToString());
                this.dict.Add(config.Id, config);
            }
            list = null;
            Log.Info("????????????????????????????????");
            this.EndInit();
        }
		
        public AIConfig Get(int id)
        {
            this.dict.TryGetValue(id, out AIConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (AIConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, AIConfig> GetAll()
        {
            return this.dict;
        }

        public AIConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class AIConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public int AIConfigId { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public int Order { get; set; }
		[ProtoMember(4, IsRequired  = true)]
		public string Name { get; set; }
		[ProtoMember(5, IsRequired  = true)]
		public int[] NodeParams { get; set; }

        
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
