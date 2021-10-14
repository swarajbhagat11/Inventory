using Inventory.Core.ChainOfResponsibility;
using Inventory.Core.HelperObjects;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Inventory.ResponsibilityHandlers.ItemHandlers
{
    public class DeleteItemHandler : ChainHandler
    {
        private readonly ILogger<DeleteItemHandler> _logger;
        private readonly IGenericRepository<ItemDTO> _itemRepo;

        public DeleteItemHandler(ILogger<DeleteItemHandler> logger, IGenericRepository<ItemDTO> itemRepo)
        {
            _logger = logger;
            _itemRepo = itemRepo;
        }

        public override object Handle(object request)
        {
            ItemDTO presentItemObj = _itemRepo.GetById((Guid)request);
            if (presentItemObj == null)
            {
                _logger.LogInformation("Inventory item not found for provided Id.");
                return new ServiceResponse { hasSuccess = false, errors = new List<string> { "Inventory item not found for provided Id." } };
            }

            _itemRepo.Delete((Guid)request);
            _itemRepo.Save();
            _logger.LogInformation("Inventory item deletion succesful.");
            return new ServiceResponse { hasSuccess = true };
        }
    }
}
