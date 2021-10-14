using Inventory.Core.ChainOfResponsibility;
using Inventory.Core.Mappers;
using Inventory.Models;
using Inventory.ViewModels;
using Microsoft.Extensions.Logging;

namespace Inventory.ResponsibilityHandlers.ItemHandlers
{
    public class ItemMappingHandler : ChainHandler
    {
        private readonly ILogger<ItemMappingHandler> _logger;

        public ItemMappingHandler(ILogger<ItemMappingHandler> logger)
        {
            _logger = logger;
        }

        public override object Handle(object request)
        {
            ItemDTO carDtoObj = ModelMapper.mapObjects<ItemDTO>((Item)request);
            _logger.LogInformation("Item mapping completed to ItemDTO.");
            return base.Handle(carDtoObj);
        }
    }
}
