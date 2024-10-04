using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Domain.Commons
{
    public class SessionStore : ISessionStore
    {
        public string GetUserId()
        {
            return "KhuongPham";
        }

        public string GetUserName()
        {
            return "Pham Duy Khuong";
        }
    }
}
