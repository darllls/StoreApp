using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.Controllers
{
    [Route("api/orderdetailsfacts")]
    [ApiController]
    public class OrderDetailsFactController : ControllerBase
    {
        private readonly IOrderDetailsFactRepository _orderDetailsFactRepository;

        public OrderDetailsFactController(IOrderDetailsFactRepository orderDetailsFactRepository)
        {
            _orderDetailsFactRepository = orderDetailsFactRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDetailsFactDTO>>> GetAllOrderDetailsFacts()
        {
            var orderDetailsFacts = await _orderDetailsFactRepository.GetAllOrderDetailsFactsAsync();
            return Ok(orderDetailsFacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsFactDTO>> GetOrderDetailsFactById(int id)
        {
            var orderDetailsFact = await _orderDetailsFactRepository.GetOrderDetailsFactByIdAsync(id);
            if (orderDetailsFact == null)
                return NotFound();

            return Ok(orderDetailsFact);
        }
    }
}
