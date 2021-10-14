using Inventory.Core.ChainOfResponsibility;
using Inventory.Core.Mappers;
using Inventory.Models;
using Inventory.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.ResponsibilityHandlers.ItemHandlers
{
    public class ItemDtoToItemMappingHandler : ChainHandler
    {
        private readonly ILogger<ItemDtoToItemMappingHandler> _logger;

        public ItemDtoToItemMappingHandler(ILogger<ItemDtoToItemMappingHandler> logger)
        {
            _logger = logger;
        }

        public override object Handle(object request)
        {
            IEnumerable<Item> allItems = ModelMapper.mapObjects<IEnumerable<Item>>((IEnumerable<ItemDTO>)request);
            _logger.LogInformation("All inventory ItemDTO mapped to Item class.");
            return allItems.ToList();
        }
    }
}
