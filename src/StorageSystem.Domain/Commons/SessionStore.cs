using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Domain.Commons
{
    public class SessionStore : ISessionStore
    {
        public string GetUserId()
        {
            return "khuongpham";
        }

        public string GetUserName()
        {
            return "khuongpham";
        }
    }
}
