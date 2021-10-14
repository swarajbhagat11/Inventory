using Inventory.Core.HelperObjects;
using Inventory.ViewModels;
using System;
using System.Collections.Generic;

namespace Inventory.Services.Interfaces
{
    public interface IItemService
    {
        ServiceResponse AddItem(Item vehicle);

        ServiceResponse ModifyItem(Guid id, Item vehicle);

        ServiceResponse DeleteItem(Guid id);

        List<Item> GetAllItems();
    }
}
