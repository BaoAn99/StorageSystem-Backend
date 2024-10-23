using StorageSystem.Order.Domain.Commons.Interfaces;

namespace StorageSystem.Order.Domain.Commons
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
