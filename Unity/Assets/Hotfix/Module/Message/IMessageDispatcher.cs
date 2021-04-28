using System.IO;

namespace ETHotfix
{
    public interface IMessageDispatcher
    {
        void Dispatch(Session session, MemoryStream message);
    }
}