using AutoMapper;
using Inventory.Models;
using Inventory.ViewModels;

namespace Inventory.Core.Mappers
{
    internal static class MapperConfig
    {
        internal static MapperConfiguration config()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Item, ItemDTO>();
                config.CreateMap<ItemDTO, Item>();
            });
        }
    }
}
