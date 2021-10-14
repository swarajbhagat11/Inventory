using Inventory.Core.ChainOfResponsibility;
using Inventory.Core.HelperObjects;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.ResponsibilityHandlers.ItemHandlers
{
    public class AddItemHandler : ChainHandler
    {
        private readonly ILogger<AddItemHandler> _logger;
        private readonly IGenericRepository<ItemDTO> _itemRepo;

        public AddItemHandler(ILogger<AddItemHandler> logger, IGenericRepository<ItemDTO> itemRepo)
        {
            _logger = logger;
            _itemRepo = itemRepo;
        }

        public override object Handle(object request)
        {
            ItemDTO itemObj = (ItemDTO)request;
            IEnumerable<ItemDTO> existingCars = _itemRepo.Find(x => x.name == itemObj.name && x.price == itemObj.price);
            if (existingCars.Count() > 0)
            {
                _logger.LogInformation("Inventory item already exists with combination of name and price.");
                return new ServiceResponse { hasSuccess = false, errors = new List<string> { "Inventory item already exists with combination of name and price." } };
            }

            _itemRepo.Insert(itemObj);
            _itemRepo.Save();
            _logger.LogInformation("AddItem ItemDTO model insertion succesful.");
            return new ServiceResponse { hasSuccess = true };
        }
    }
}
