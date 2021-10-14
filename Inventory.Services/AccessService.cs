using Inventory.Models;
using Inventory.Models.Enums;
using Inventory.Repositories.Interfaces;
using Inventory.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Inventory.Services
{
    public class AccessService : IAccessService
    {
        private readonly ILogger<AccessService> _logger;
        private readonly IGenericRepository<AccessDTO> _accessRepo;

        public AccessService(ILogger<AccessService> logger, IGenericRepository<AccessDTO> accessRepo)
        {
            _logger = logger;
            _accessRepo = accessRepo;
        }

        public bool IsAdmin(string accessId, string secretKey)
        {
            _logger.LogInformation("Admin authorization started.");
            AccessDTO accessObj = _accessRepo.Find(x => x.accessId == accessId && x.secretKey == secretKey).FirstOrDefault();
            return accessObj != null && accessObj.role == Roles.Admin;
        }
    }
}
