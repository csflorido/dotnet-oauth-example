using Microsoft.AspNetCore.Mvc;
using OauthExample.MyWebApi.Config;
using OauthExample.MyWebApi.Models;

namespace OauthExample.MyWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(TokenValidationFilter))]
    public class BooksController : ControllerBase
    {
        public BooksController() { }

        [HttpGet]
        public IActionResult Fetch()
        {
            var books = new List<Book>()
            {
                new Book(74276, "Arthur's inheritance : Or, how he conquered", "Leslie, Emma"),
                new Book(74272, "The charm of Venice : An anthology", "Hyatt, Alfred H. (Alfred Henry)"),
                new Book(74277,"Beauty and the beast : An old tale new-told, with pictures", "E. V. B. (Eleanor Vere Boyle)"),
                new Book(74270, "A woman's trust; or, Lady Elaine's martyrdom : a novel", "Clay, Bertha M."),
                new Book(74271,"The Queen of the Swamp, and other plain Americans", "Catherwood, Mary Hartwell")
            };

            return Ok(books);
        }
    }
}
