using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mohafadati.Services.Titres.Data;
using Mohafadati.Services.Titres.Models;
using Mohafadati.Services.Titres.Models.Dto;

namespace Mohafadati.Services.Titres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitresAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TitresAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public ActionResult Post([FromBody] TitreDto obj)
        {
            //StringComparison.OrdinalIgnoreCase
            try
            {
                List<Titre> TitreContent = _db.Titres.ToList();
                
                    Titre? titre = TitreContent.Find((element) => element.ConservationFonciere.Equals(obj.ConservationFonciere)
                                                                 && element.Indice.Equals(obj.Indice)
                                                                 && element.IndiceSpecial == obj.IndiceSpecial
                                                                 && element.NumeroTitre == obj.NumeroTitre);
                    
                
                
                if (titre is null)
                {
                    throw new Exception("Ce titre n'existe pas!");
                }
                return Ok(obj);

            }
            catch (Exception ex) 
            {
                return NotFound( new {message = ex.Message} );
            }
        }
    }
}
