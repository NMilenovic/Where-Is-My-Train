using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Aplikacija.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KartaController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public KartaController(AplikacijaContext context)
        {
            Context = context;
        }

        
        [HttpPost]
        [Route("DodajKartu/{voznjaID}/{cenaKarte}")]
        public async Task<ActionResult> DodajKartu(int voznjaID, double cenaKarte)
        {
            if(voznjaID <= 0)
                return BadRequest("Los ID voznje");
            if(cenaKarte < 1 || cenaKarte > 10000)
                return BadRequest("Nevalidna vrednost cene karte (od 1 do 10k)");
            try{
                Voznja v = await Context.Voznje.Where(p => p.ID == voznjaID).FirstOrDefaultAsync();
                if(v == null)
                    return BadRequest("Voznja ne postoji");
                Karta k = await Context.Karte.Where(p => p.Voznja == v)
                .Include(p => p.Voznja)
                .FirstOrDefaultAsync();
                if(k != null)
                    return BadRequest("Voznja vec ima kartu!");
                k = new Karta{
                    Cena = cenaKarte,
                    Voznja = v
                };

                Context.Karte.Add(k);
                await Context.SaveChangesAsync();
                return Ok("Karta je dodata");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("KupiKartu/{korisnikUser}/{voznjaID}")]
        public async Task<ActionResult> KupiKartu(string korisnikUser, int voznjaID)
        {
            if(voznjaID <= 0)
                return BadRequest("Los id voznje");
            try{
                Korisnik k = await Context.Korisnici.Where(p => p.Username == korisnikUser)
                .Include(p => p.kupljeneKarte)
                .FirstOrDefaultAsync();
                if (k == null)
                    return BadRequest("Korisnik ne postoji");
                Voznja v = await Context.Voznje.Where(p => p.ID == voznjaID).FirstOrDefaultAsync();
                if(v == null)
                    return BadRequest("Voznja ne postoji");
                Karta karta = await Context.Karte.Where(p => p.Voznja == v).FirstOrDefaultAsync();
                if(karta == null)
                    return BadRequest("Voznja nema kartu");
                if(k.kupljeneKarte.Contains(karta))
                    return BadRequest("Korisnik je vec kupio kartu za ovu voznju");
                k.kupljeneKarte.Add(karta);
                Context.Korisnici.Update(k);
                await Context.SaveChangesAsync();
                return Ok("Korisnik je kupio kartu");
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("VratiKupljeneKarte/{korisnikID}")]
        public async Task<ActionResult> VratiKupljeneKarte(int korisnikID)
        {
            if(korisnikID <=0)
                return BadRequest("Korisnicki ID je nevalidan");
            Korisnik k = await Context.Korisnici.Where( p => p.ID == korisnikID)
            .Include(p => p.kupljeneKarte)
            .ThenInclude(p => p.Voznja)
            .FirstOrDefaultAsync();
            if(k == null)
                return BadRequest("Korisnik ne postoji");
            var karte = k.kupljeneKarte;
            return Ok(karte);
        }
    }
}