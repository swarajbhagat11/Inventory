namespace Inventory.Services.Interfaces
{
    public interface IAccessService
    {
        public bool IsAdmin(string accessId, string secretKey);
    }
}
