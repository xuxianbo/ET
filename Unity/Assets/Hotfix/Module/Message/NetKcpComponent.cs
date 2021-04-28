﻿using ET;

 namespace ETHotfix
{
    public class NetKcpComponent: Entity
    {
        public AService Service;
        
        public IMessageDispatcher MessageDispatcher { get; set; }
    }
}