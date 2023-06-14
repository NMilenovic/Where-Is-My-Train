using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Aplikacija.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreskaController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public GreskaController(AplikacijaContext context)
        {
            Context = context;
        }

        [HttpPost]
        [Route("NapisiGresku/{idVoznje}/{sadrzaj}")]
        public async Task<ActionResult> NapisiGresku(int idVoznje, string sadrzaj)
        {
            if(idVoznje<=0)
                return BadRequest("Nevalidan ID voznje");
            if(string.IsNullOrWhiteSpace(sadrzaj) || sadrzaj.Length > 2000)
                return BadRequest("Nevalidna velicina sadrzaja");
            try{
                Voznja v = await Context.Voznje.Where(p => p.ID == idVoznje).FirstOrDefaultAsync();
                if( v == null)
                    return BadRequest("Voznja ne postoji!");
                Greska greska = new Greska{
                    Voznja = v,
                    Opis = sadrzaj
                };
                Context.Greske.Add(greska);
                await Context.SaveChangesAsync();
                return Ok("Greska je dodata!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("VratiSveGreske")]
        public async Task<ActionResult> VratiSveGreske()
        {
            var lista = await Context.Greske.Include(p => p.Voznja)
            .ThenInclude(p => p.PocetnaStanica)
            .Include(p => p.Voznja)
            .ThenInclude(p => p.KrajnaStanica)
            .ToListAsync();
            return Ok(lista);
        }

        [HttpDelete]
        [Route("ObrisiGresku/{greskaID}")]
        public async Task<ActionResult> ObrisiGresku(int greskaID)
        {
            if(greskaID <=0)
                return BadRequest("Nevalidan ID greske");
            try{
                Greska g = await Context.Greske.FindAsync(greskaID);
                if(g == null)
                    return BadRequest("Greska ne postoji!");
                Context.Greske.Remove(g);
                await Context.SaveChangesAsync();
                return Ok("Greska je izbrisana iz liste!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
