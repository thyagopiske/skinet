using Api.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{

    //controller para análise de mensagens de erro a fim de gerar uma resposta de erro consistente da Api
    public class BuggyController : BaseController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("notfound")]
        public ActionResult GetNotFound()
        {
            return NotFound(new ApiResponse((int) HttpStatusCode.NotFound));
        }

        [HttpGet]
        [Route("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse((int) HttpStatusCode.BadRequest));
        }

        [HttpGet]
        [Route("server-error")]
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(-1);
            var nullReferenceException = product.ToString();
            return Ok();
        }

        //Aqui será retornado um validation erro caso vc passe uma string em vez de um número, por exemplo
        [HttpGet]
        [Route("badrequest/{id}")]
        public ActionResult GetBadRequestValidationError(int id)
        {
            return Ok();
        }
    }
}
