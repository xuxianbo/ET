using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ET;

namespace ETHotfix
{
    [ObjectSystem]
    public class ConfigAwakeSystem: AwakeSystem<ConfigComponent>
    {
        public override void Awake(ConfigComponent self)
        {
            ConfigComponent.Instance = self;
        }
    }

    [ObjectSystem]
    public class ConfigDestroySystem: DestroySystem<ConfigComponent>
    {
        public override void Destroy(ConfigComponent self)
        {
            ConfigComponent.Instance = null;
        }
    }

    public static class ConfigComponentSystem
    {
        public static void Load(this ConfigComponent self)
        {
            self.AllConfig.Clear();
            Game.EventSystem.RegisterAttribute<ConfigAttribute>();
            HashSet<Type> types = Game.EventSystem.GetTypes(typeof (ConfigAttribute));

            Dictionary<string, byte[]> configBytes = new Dictionary<string, byte[]>();
            ConfigComponent.GetAllConfigBytes(configBytes);

            foreach (Type type in types)
            {
                try
                {
                    self.LoadOne(type, configBytes);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw;
                }

            }
        }

        private static void LoadOne(this ConfigComponent self, Type configType, Dictionary<string, byte[]> configBytes)
        {
            byte[] oneConfigBytes = configBytes[configType.Name];

            object category = ProtobufHelper.FromBytes(configType, oneConfigBytes, 0, oneConfigBytes.Length);
            self.AllConfig[configType] = category;
        }
    }
}