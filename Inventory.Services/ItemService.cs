using Autofac.Features.Indexed;
using Inventory.Core.ChainOfResponsibility;
using Inventory.Core.HelperObjects;
using Inventory.Services.Interfaces;
using Inventory.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Inventory.Services
{
    public class ItemService : IItemService
    {
        private readonly ILogger<ItemService> _logger;
        private readonly IIndex<string, IChainHandler> _handlerObj;

        public ItemService(ILogger<ItemService> logger, IIndex<string, IChainHandler> handlerObj)
        {
            _logger = logger;
            _handlerObj = handlerObj;
        }

        public ServiceResponse AddItem(Item vehicle)
        {
            // Create object of responsibility handler
            IChainHandler itemMapping = _handlerObj["itemMapping"];
            IChainHandler addItem = _handlerObj["addItem"];

            // set sequence of responsibility handler
            itemMapping.SetNext(addItem);

            ServiceResponse handleRes = (ServiceResponse)itemMapping.Handle(vehicle);
            return handleRes;
        }

        public ServiceResponse ModifyItem(Guid id, Item vehicle)
        {
            // Create object of responsibility handler
            IChainHandler itemMapping = _handlerObj["itemMapping"];
            IChainHandler modifyItem = _handlerObj["modifyItem"];

            // set sequence of responsibility handler
            itemMapping.SetNext(modifyItem);

            vehicle.Id = id;

            ServiceResponse handleRes = (ServiceResponse)itemMapping.Handle(vehicle);
            return handleRes;
        }

        public ServiceResponse DeleteItem(Guid id)
        {
            // Create object of responsibility handler
            IChainHandler deleteItem = _handlerObj["deleteItem"];

            ServiceResponse handleRes = (ServiceResponse)deleteItem.Handle(id);
            return handleRes;
        }

        public List<Item> GetAllItems()
        {
            // Create object of responsibility handler
            IChainHandler getAllItem = _handlerObj["getAllItem"];
            IChainHandler itemDtoToItemMapping = _handlerObj["itemDtoToItemMapping"];

            // set sequence of responsibility handler
            getAllItem.SetNext(itemDtoToItemMapping);

            List<Item> handleRes = (List<Item>)getAllItem.Handle();
            return handleRes;
        }
    }
}
