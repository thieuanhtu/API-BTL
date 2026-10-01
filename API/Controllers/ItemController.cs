using BLL;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemBusiness _itemBusiness;

        public ItemController(IItemBusiness itemBusiness)
        {
            _itemBusiness = itemBusiness;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ItemModel model)
        {
            var result = _itemBusiness.Create(model);
            return Ok(new { success = result });
        }

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
    }
}