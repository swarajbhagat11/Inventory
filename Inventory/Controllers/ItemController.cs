using Inventory.Core.HelperObjects;
using Inventory.Filters;
using Inventory.Services.Interfaces;
using Inventory.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Inventory.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(IsAdminFilter))]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService _itemService;

        public ItemController(ILogger<ItemController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [HttpPost("addItem")]
        public IActionResult AddItem(Item item)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("AddItem service call started.");
                ServiceResponse res = _itemService.AddItem(item);
                _logger.LogInformation("AddItem service call completed.", res);
                return res.hasSuccess ? Ok("Item added successfully.") : BadRequest(res.errors);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("modifyItem/{id}")]
        public IActionResult ModifyItem(Guid id, Item item)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("ModifyItem service call started.");
                ServiceResponse res = _itemService.ModifyItem(id, item);
                _logger.LogInformation("ModifyItem service call completed.", res);
                return res.hasSuccess ? Ok("Item updated successfully.") : BadRequest(res.errors);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("deleteItem/{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            _logger.LogInformation("DeleteItem service call started.");
            ServiceResponse res = _itemService.DeleteItem(id);
            _logger.LogInformation("DeleteItem service call completed.", res);
            return res.hasSuccess ? Ok("Item deleted successfully.") : BadRequest(res.errors);
        }

        [HttpGet("getAllItems")]
        public IActionResult GetAllItems()
        {
            _logger.LogInformation("GetAllItems service call started.");
            List<Item> res = _itemService.GetAllItems();
            _logger.LogInformation("GetAllItems service call completed.", res);
            return Ok(res);
        }
    }
}
