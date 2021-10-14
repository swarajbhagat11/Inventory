using Autofac;
using Inventory.Core.ChainOfResponsibility;
using Inventory.Models;
using Inventory.Repositories;
using Inventory.Repositories.Interfaces;
using Inventory.ResponsibilityHandlers.ItemHandlers;
using Inventory.Services;
using Inventory.Services.Interfaces;

namespace Inventory.DIContainer
{
    public class AppAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ItemService>().As<IItemService>();
            builder.RegisterType<AccessService>().As<IAccessService>();
            builder.RegisterType<ItemMappingHandler>().Named<IChainHandler>("itemMapping");
            builder.RegisterType<AddItemHandler>().Named<IChainHandler>("addItem");
            builder.RegisterType<DeleteItemHandler>().Named<IChainHandler>("deleteItem");
            builder.RegisterType<GetAllItemsHandler>().Named<IChainHandler>("getAllItem");
            builder.RegisterType<ItemDtoToItemMappingHandler>().Named<IChainHandler>("itemDtoToItemMapping");
            builder.RegisterType<ModifyItemHandler>().Named<IChainHandler>("modifyItem");
            builder.RegisterType<GenericRepository<ItemDTO>>().As<IGenericRepository<ItemDTO>>();
            builder.RegisterType<GenericRepository<AccessDTO>>().As<IGenericRepository<AccessDTO>>();
        }
    }
}

