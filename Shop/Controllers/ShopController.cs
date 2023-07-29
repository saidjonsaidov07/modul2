using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly AppDbContext _context;
        

        public ShopController(AppDbContext context) 
        { 
            _context = context;
        }

        [HttpPost]
        public ActionResult<Shop> Post([FromBody] Shop shop )
        {
            var result = _context.Shops.Add( shop );
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Shop>> GetShop()
        {
            var shops = _context.Shops.ToList();
            return Ok( shops );
        }

        [HttpPut("ShopId")]
        public ActionResult UpdateShop(int ShopId, [FromBody] Shop shop)
        {
            if (ShopId != shop.ShopId)
            {
                return BadRequest();
            }
            _context.Entry(shop).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("ShopId")]
        public ActionResult DeleteShop([FromQuery]int ShopId)
        {
            var shopToDelete = _context.Shops.FirstOrDefault(x => x.ShopId == ShopId);
            _context.Shops.Remove( shopToDelete );
            _context.SaveChanges();
            return NoContent();
        }

    }
}
