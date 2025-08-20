using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Laboratorios.Core.ViewModels;
using System.IdentityModel.Tokens.Jwt;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionOrderController : ControllerBase
    {
        private readonly IProductionOrderRepository _ProductionOrderRepository;

        public ProductionOrderController(IProductionOrderRepository ProductionOrderRepository)
        {
            _ProductionOrderRepository = ProductionOrderRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetProductionOrder(bool? status, bool? consumibles)
        {
            try
            {
                var ProductionOrder = await _ProductionOrderRepository.GetProductionOrders(status, consumibles);
                return Ok(ProductionOrder);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet("{productionOrderId}"), Authorize]
        public async Task<IActionResult> GetProductionOrderById(int productionOrderId)
        {
            try
            {
                var ProductionOrder = await _ProductionOrderRepository.GetProductionOrderById(productionOrderId);
                return Ok(ProductionOrder);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet("{date},{endDate}, {status}"), Authorize]
        public async Task<IActionResult> GetReportProductionsOrders(DateTime date, DateTime endDate, int status)
        {
            try
            {
                var ProductionOrder = await _ProductionOrderRepository.GetReportProductionsOrders(date, endDate, status);
                return Ok(ProductionOrder);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertProductionOrder([FromBody] ProductionOrder ProductionOrder)
        {
            try
            {
                var user = await _ProductionOrderRepository.InsertProductionOrder(ProductionOrder);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insertReplicate")]
        public async Task<IActionResult> InsertProductionOrderReplicate([FromBody] ProductionOrderViewModel data)
        {
            try
            {
                var user = await _ProductionOrderRepository.InsertProductionOrderReplicate(data);
                return Ok(data.productionOrder);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("sendProductionOrders")]
        public async Task<IActionResult> sendProductionOrders([FromBody] ProductionOrderOject ProductionOrder)
        {
            try
            {
                string jwtEncoded = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                var jwtDecoded = new JwtSecurityTokenHandler().ReadToken(jwtEncoded) as JwtSecurityToken;
                var userId = Convert.ToInt32(jwtDecoded.Claims.FirstOrDefault(j => j.Type.EndsWith("id")).Value);
                var orders = await _ProductionOrderRepository.sendProductionOrders(ProductionOrder, userId, 0);
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("sendGoodIssues")]
        public async Task<IActionResult> sendGoodIssues([FromBody] ProductionOrderOject ProductionOrder)
        {
            try
            {
                string jwtEncoded = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                var jwtDecoded = new JwtSecurityTokenHandler().ReadToken(jwtEncoded) as JwtSecurityToken;
                var userId = Convert.ToInt32(jwtDecoded.Claims.FirstOrDefault(j => j.Type.EndsWith("id")).Value);
                var orders = await _ProductionOrderRepository.sendGoodIssues(ProductionOrder, userId, 0);
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
