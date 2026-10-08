using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemBusiness _itemBusiness;

        public ItemController(IItemBusiness itemBusiness)
        {
            _itemBusiness = itemBusiness;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] ItemModel model)
        {
            var result = _itemBusiness.Create(model);
            return Ok(new { success = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("update")]
        public IActionResult Update([FromBody] ItemModel model)
        {
            var result = _itemBusiness.Update(model);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            var data = _itemBusiness.GetDatabyID(id);
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _itemBusiness.Delete(id);
            return Ok(new { success = result });
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int pageIndex = int.Parse(formData["pageIndex"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string itemGroupId = formData.Keys.Contains("itemGroupId") ? Convert.ToString(formData["itemGroupId"]) : null;
                string itemName = formData.Keys.Contains("itemName") ? Convert.ToString(formData["itemName"]) : null;

                long total = 0;
                var data = _itemBusiness.Search(pageIndex, pageSize, out total, itemGroupId, itemName);
                return Ok(new { TotalItems = total, Data = data, PageIndex = pageIndex, PageSize = pageSize });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("add-combo-item")]
        public IActionResult AddComboItem([FromBody] Dictionary<string, object> formData)
        {
            string comboId = formData["comboId"].ToString();
            string itemId = formData["itemId"].ToString();
            int quantity = int.Parse(formData["quantity"].ToString());
            var result = _itemBusiness.AddComboItem(comboId, itemId, quantity);
            return Ok(new { success = result });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("remove-combo-item")]
        public IActionResult RemoveComboItem([FromBody] Dictionary<string, string> formData)
        {
            string comboId = formData["comboId"];
            string itemId = formData["itemId"];
            var result = _itemBusiness.RemoveComboItem(comboId, itemId);
            return Ok(new { success = result });
        }

        [HttpGet("get-combo-items/{comboId}")]
        public IActionResult GetComboItems(string comboId)
        {
            var data = _itemBusiness.GetComboItems(comboId);
            return Ok(data);
        }
    }
}