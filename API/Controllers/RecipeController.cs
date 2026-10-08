using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Bep")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeBusiness _recipeBusiness;

        public RecipeController(IRecipeBusiness recipeBusiness)
        {
            _recipeBusiness = recipeBusiness;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RecipeModel model)
        {
            var result = _recipeBusiness.Create(model);
            return Ok(new { success = result });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] RecipeModel model)
        {
            var result = _recipeBusiness.Update(model);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            var data = _recipeBusiness.GetDatabyID(id);
            return Ok(data);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _recipeBusiness.Delete(id);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-item-id/{itemId}")]
        public IActionResult GetByItemID(string itemId)
        {
            var data = _recipeBusiness.GetByItemID(itemId);
            return Ok(data);
        }
    }
}