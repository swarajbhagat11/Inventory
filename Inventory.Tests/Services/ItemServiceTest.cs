using Autofac.Features.Indexed;
using Inventory.Core.ChainOfResponsibility;
using Inventory.Core.HelperObjects;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Inventory.ResponsibilityHandlers.ItemHandlers;
using Inventory.Services;
using Inventory.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Inventory.Tests.Services
{
    public class ItemServiceTest
    {
        private Mock<ILogger<ItemService>> _mockLogger = new Mock<ILogger<ItemService>>();
        private Mock<IIndex<string, IChainHandler>> _mockHandlerObj = new Mock<IIndex<string, IChainHandler>>();

        // All Handlers mock
        private Mock<ILogger<ItemMappingHandler>> _mockItemMappingHandlerLogging = new Mock<ILogger<ItemMappingHandler>>();
        private Mock<ILogger<AddItemHandler>> _mockAddItemHandlerLogging = new Mock<ILogger<AddItemHandler>>();
        private Mock<ILogger<DeleteItemHandler>> _mockDeleteItemHandlerLogging = new Mock<ILogger<DeleteItemHandler>>();
        private Mock<ILogger<GetAllItemsHandler>> _mockGetAllItemHandlerLogging = new Mock<ILogger<GetAllItemsHandler>>();
        private Mock<ILogger<ItemDtoToItemMappingHandler>> _mockDtoToItemHandlerLogging = new Mock<ILogger<ItemDtoToItemMappingHandler>>();
        private Mock<ILogger<ModifyItemHandler>> _mockModifyItemHandlerLogging = new Mock<ILogger<ModifyItemHandler>>();

        // All repo mock
        private Mock<IGenericRepository<ItemDTO>> _mockItemRepo = new Mock<IGenericRepository<ItemDTO>>();

        public ItemServiceTest()
        {
            _mockHandlerObj.Setup(x => x["itemMapping"]).Returns(new ItemMappingHandler(_mockItemMappingHandlerLogging.Object));
            _mockHandlerObj.Setup(x => x["addItem"]).Returns(new AddItemHandler(_mockAddItemHandlerLogging.Object, _mockItemRepo.Object));
            _mockHandlerObj.Setup(x => x["deleteItem"]).Returns(new DeleteItemHandler(_mockDeleteItemHandlerLogging.Object, _mockItemRepo.Object));
            _mockHandlerObj.Setup(x => x["getAllItem"]).Returns(new GetAllItemsHandler(_mockGetAllItemHandlerLogging.Object, _mockItemRepo.Object));
            _mockHandlerObj.Setup(x => x["itemDtoToItemMapping"]).Returns(new ItemDtoToItemMappingHandler(_mockDtoToItemHandlerLogging.Object));
            _mockHandlerObj.Setup(x => x["modifyItem"]).Returns(new ModifyItemHandler(_mockModifyItemHandlerLogging.Object, _mockItemRepo.Object));
        }

        [Fact]
        public void Item_added_successfully()
        {
            ItemService service = new ItemService(_mockLogger.Object, _mockHandlerObj.Object);
            Item itemObj = new Item
            {
                name = "Test",
                description = "Test description",
                price = 121.50,
                count = 10
            };
            ServiceResponse res = service.AddItem(itemObj);
            _mockItemRepo.Verify(m => m.Insert(It.Is<ItemDTO>(x => x.name == itemObj.name)), Times.Once);
            _mockItemRepo.Verify(m => m.Save(), Times.Once);
            Assert.True(res.hasSuccess);
        }

        [Fact]
        public void Item_already_exists_with_same_name_and_price()
        {
            ItemService service = new ItemService(_mockLogger.Object, _mockHandlerObj.Object);
            _mockItemRepo.Setup(e => e.Find(It.IsAny<Func<ItemDTO, bool>>())).Returns(new List<ItemDTO> { new ItemDTO() });
            Item itemObj = new Item
            {
                name = "Test",
                description = "Test description",
                price = 121.50,
                count = 10
            };
            ServiceResponse res = service.AddItem(itemObj);
            Assert.False(res.hasSuccess);
        }

        [Fact]
        public void Get_all_items()
        {
            ItemService service = new ItemService(_mockLogger.Object, _mockHandlerObj.Object);
            _mockItemRepo.Setup(e => e.GetAll()).Returns(new List<ItemDTO> { new ItemDTO(), new ItemDTO() });
            List<Item> res = service.GetAllItems();
            Assert.Equal(2, res.Count);
        }

        [Fact]
        public void Modify_item_successfully()
        {
            ItemService service = new ItemService(_mockLogger.Object, _mockHandlerObj.Object);
            Guid id = Guid.NewGuid();
            Item itemObj = new Item
            {
                name = "Test",
                description = "Test description",
                price = 121.50,
                count = 10
            };
            _mockItemRepo.Setup(e => e.GetById(id)).Returns(new ItemDTO());
            ServiceResponse res = service.ModifyItem(id, itemObj);
            _mockItemRepo.Verify(m => m.Update(It.Is<ItemDTO>(x => x.name == itemObj.name)), Times.Once);
            _mockItemRepo.Verify(m => m.Save(), Times.Once);
            Assert.True(res.hasSuccess);
        }

        [Fact]
        public void Modify_item_failed_due_to_inventory_item_not_found()
        {
            ItemService service = new ItemService(_mockLogger.Object, _mockHandlerObj.Object);
            Guid id = Guid.NewGuid();
            Item itemObj = new Item
            {
                name = "Test",
                description = "Test description",
                price = 121.50,
                count = 10
            };
            ServiceResponse res = service.ModifyItem(id, itemObj);
            Assert.False(res.hasSuccess);
        }

        [Fact]
        public void Delete_item_successfully()
        {
            ItemService service = new ItemService(_mockLogger.Object, _mockHandlerObj.Object);
            Guid id = Guid.NewGuid();
            _mockItemRepo.Setup(e => e.GetById(id)).Returns(new ItemDTO());
            ServiceResponse res = service.DeleteItem(id);
            _mockItemRepo.Verify(m => m.Delete(id), Times.Once);
            _mockItemRepo.Verify(m => m.Save(), Times.Once);
            Assert.True(res.hasSuccess);
        }

        [Fact]
        public void Delete_item_failed_due_to_inventory_item_not_found()
        {
            ItemService service = new ItemService(_mockLogger.Object, _mockHandlerObj.Object);
            Guid id = Guid.NewGuid();
            ServiceResponse res = service.DeleteItem(id);
            Assert.False(res.hasSuccess);
        }
    }
}
