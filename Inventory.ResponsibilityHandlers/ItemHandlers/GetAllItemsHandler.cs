using Inventory.Core.ChainOfResponsibility;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Inventory.ResponsibilityHandlers.ItemHandlers
{
    public class GetAllItemsHandler : ChainHandler
    {
        private readonly ILogger<GetAllItemsHandler> _logger;
        private readonly IGenericRepository<ItemDTO> _itemRepo;

        public GetAllItemsHandler(ILogger<GetAllItemsHandler> logger, IGenericRepository<ItemDTO> itemRepo)
        {
            _logger = logger;
            _itemRepo = itemRepo;
        }

        public override object Handle(object request = null)
        {
            IEnumerable<ItemDTO> allItems = _itemRepo.GetAll();
            _logger.LogInformation("All inventory items fetched.");
            return base.Handle(allItems);
        }
    }
}
