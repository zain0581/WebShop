using IMSWeb.Dal;
using IMSWeb.Interface;

namespace IMSWeb.Repo
{
    public class OrderItemRepo : IOrderItem
    {
        private IMSContext _context;
        public OrderItemRepo(IMSContext context)
        {
            _context = context;

        }






    }
}
