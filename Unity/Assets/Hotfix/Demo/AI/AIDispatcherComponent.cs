using System.Collections.Generic;

namespace ETHotfix
{
    public class AIDispatcherComponent: Entity
    {
        public static AIDispatcherComponent Instance;
        
        public Dictionary<string, AAIHandler> AIHandlers = new Dictionary<string, AAIHandler>();
    }
}