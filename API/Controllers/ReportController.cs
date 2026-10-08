using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportBusiness _reportBusiness;

        public ReportController(IReportBusiness reportBusiness)
        {
            _reportBusiness = reportBusiness;
        }

        [HttpPost("revenue")]
        public IActionResult Revenue([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                DateTime fromDate = DateTime.Parse(formData["fromDate"].ToString());
                DateTime toDate = DateTime.Parse(formData["toDate"].ToString());
                string ca = formData.ContainsKey("ca") ? Convert.ToString(formData["ca"]) : null;
                var data = _reportBusiness.GetRevenue(fromDate, toDate, ca);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("best-sellers")]
        public IActionResult BestSellers([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                DateTime fromDate = DateTime.Parse(formData["fromDate"].ToString());
                DateTime toDate = DateTime.Parse(formData["toDate"].ToString());
                int top = formData.ContainsKey("top") ? int.Parse(formData["top"].ToString()) : 10;
                var data = _reportBusiness.GetBestSellers(fromDate, toDate, top);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}