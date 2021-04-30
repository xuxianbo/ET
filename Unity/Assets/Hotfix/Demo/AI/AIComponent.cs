using ET;

namespace ETHotfix
{
    // 客户端挂在ZoneScene上，服务端挂在Unit上
    public class AIComponent: Entity
    {
        public int AIConfigId;
        
        public ETCancellationToken CancellationToken;

        public long Timer;

        public int Current;
        
        private void Cancel()
        {
            CancellationToken?.Cancel();
            Current = 0;
            CancellationToken = null;
        }
        
        public void Check()
        {
            if (Parent == null)
            {
                TimerComponent.Instance.Remove(ref Timer);
                return;
            }

            var oneAI = AIConfigCategory.Instance.AIConfigs[AIConfigId];

            foreach (AIConfig aiConfig in oneAI.Values)
            {

                AIDispatcherComponent.Instance.AIHandlers.TryGetValue(aiConfig.Name, out AAIHandler aaiHandler);

                if (aaiHandler == null)
                {
                    Log.Error($"not found aihandler: {aiConfig.Name}");
                    continue;
                }

                int ret = aaiHandler.Check(this, aiConfig);
                if (ret != 0)
                {
                    continue;
                }

                if (Current == aiConfig.Id)
                {
                    break;
                }

                Cancel(); // 取消之前的行为
                ETCancellationToken cancellationToken = new ETCancellationToken();
                CancellationToken = cancellationToken;
                Current = aiConfig.Id;

                aaiHandler.Execute(this, aiConfig, cancellationToken).Coroutine();
                return;
            }
            
        }
    }
}