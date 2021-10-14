using Inventory.Core;
using Inventory.Core.ChainOfResponsibility;
using Inventory.Core.HelperObjects;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Inventory.ResponsibilityHandlers.ItemHandlers
{
    public class ModifyItemHandler : ChainHandler
    {
        private readonly ILogger<ModifyItemHandler> _logger;
        private readonly IGenericRepository<ItemDTO> _itemRepo;

        public ModifyItemHandler(ILogger<ModifyItemHandler> logger, IGenericRepository<ItemDTO> itemRepo)
        {
            _logger = logger;
            _itemRepo = itemRepo;
        }

        public override object Handle(object request)
        {
            ItemDTO itemObj = (ItemDTO)request;

            ItemDTO presentItemObj = _itemRepo.GetById(itemObj.id);
            if (presentItemObj == null)
            {
                _logger.LogInformation("Inventory item not found for provided Id.");
                return new ServiceResponse { hasSuccess = false, errors = new List<string> { "Inventory item not found for provided Id." } };
            }

            Utility.copyObject(itemObj, presentItemObj);
            _logger.LogInformation("Source ItemDTO object copied to destination ItemDTO object.");

            _itemRepo.Update(presentItemObj);
            _itemRepo.Save();
            _logger.LogInformation("Inventory item updation succesful.");
            return new ServiceResponse { hasSuccess = true };
        }
    }
}
