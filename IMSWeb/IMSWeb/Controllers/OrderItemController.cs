using IMSWeb.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        public IOrderItem orderItem { get; set; }
        public OrderItemController(IOrderItem orderItem) 
        {
            this.orderItem = orderItem;
        }

    }
}
