using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        [Route("all")]
        public string GetProducts()
        {
            return "Hi";
        }

        [HttpGet]
        [Route("{id}")]
        public string GetProduct(int id)
        {
            return "Meu produto";
        }
    }
}
