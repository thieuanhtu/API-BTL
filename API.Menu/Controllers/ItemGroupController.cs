using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemGroupController : ControllerBase
    {
        private readonly IItemGroupBusiness _itemGroupBusiness;

        public ItemGroupController(IItemGroupBusiness itemGroupBusiness)
        {
            _itemGroupBusiness = itemGroupBusiness;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] ItemGroupModel model)
        {
            var result = _itemGroupBusiness.Create(model);
            return Ok(new { success = result });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("update")]
        public IActionResult Update([FromBody] ItemGroupModel model)
        {
            var result = _itemGroupBusiness.Update(model);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            var data = _itemGroupBusiness.GetDatabyID(id);
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _itemGroupBusiness.Delete(id);
            return Ok(new { success = result });
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int pageIndex = int.Parse(formData["pageIndex"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string itemGroupName = formData.Keys.Contains("itemGroupName") ? Convert.ToString(formData["itemGroupName"]) : null;

                long total = 0;
                var data = _itemGroupBusiness.Search(pageIndex, pageSize, out total, itemGroupName);
                return Ok(new { TotalItems = total, Data = data, PageIndex = pageIndex, PageSize = pageSize });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}