using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModifierController : ControllerBase
    {
        private readonly IModifierBusiness _modifierBusiness;

        public ModifierController(IModifierBusiness modifierBusiness)
        {
            _modifierBusiness = modifierBusiness;
        }

        // Lấy danh sách lựa chọn, có thể lọc theo nhóm món: ?itemGroupId=G01_2
        [HttpGet("get-all")]
        public IActionResult GetAll([FromQuery] string? itemGroupId = null)
        {
            return Ok(_modifierBusiness.GetAll(itemGroupId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] ModifierModel model)
        {
            try
            {
                var result = _modifierBusiness.Create(model);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("update")]
        public IActionResult Update([FromBody] ModifierModel model)
        {
            try
            {
                var result = _modifierBusiness.Update(model);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _modifierBusiness.Delete(id);
            return Ok(new { success = result });
        }
    }
}