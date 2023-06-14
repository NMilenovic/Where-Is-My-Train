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
    public class RecenzijeController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public RecenzijeController(AplikacijaContext context)
        {
            Context = context;
        }

        [HttpPost]
        [Route("NovaRecenzijaStanice/{korisnikUser}/{stanicaID}/{sadrzaj}")]
        public async Task<ActionResult> NovaRecenzijaStanice(string korisnikUser, int stanicaID, string sadrzaj)
        {
            if(stanicaID <=0)
                return BadRequest("Los ID stanice");
            if(string.IsNullOrWhiteSpace(sadrzaj) )
                return BadRequest("Recenzija ne moze biti prazna!");
            try{
                Korisnik k = await Context.Korisnici.Where(p =>p.Username == korisnikUser).FirstOrDefaultAsync();
                if(k == null)
                    return  BadRequest("Korisnik ne postoji");
                Stanica s = await Context.Stanice.Where(p => p.ID == stanicaID).FirstOrDefaultAsync();
                if(s == null)
                    return BadRequest("Stanica ne postoji!");
                RecenzijaStanice recenzija = new RecenzijaStanice{
                    Stanica = s,
                    Sadrzaj = sadrzaj,
                    Autor = k
                };
                Context.RecenzijeStanice.Add(recenzija);
                await Context.SaveChangesAsync();
                return Ok("Recenzija je dodata!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Route("NovaRecenzijaPrevoznika/{korisnikUser}/{prevoznikID}/{sadrzaj}")]
        public async Task<ActionResult> NovaRecenzijaPrevoznika(string korisnikUser, int prevoznikID, string sadrzaj)
        {
            if(prevoznikID <=0)
                return BadRequest("Los ID prevoznika");
            if(string.IsNullOrWhiteSpace(sadrzaj) )
                return BadRequest("Recenzija ne moze biti prazna!");
            try{
                Korisnik k = await Context.Korisnici.Where(p =>p.Username == korisnikUser).FirstOrDefaultAsync();
                if(k == null)
                    return  BadRequest("Korisnik ne postoji");
                Prevoznik p = await Context.Prevoznici.Where(p => p.ID == prevoznikID).FirstOrDefaultAsync();
                if(p == null)
                    return BadRequest("Prevoznik ne postoji!");
                RecenzijaPrevoznika recenzija = new RecenzijaPrevoznika{
                    Prevoznik = p,
                    Sadrzaj = sadrzaj,
                    Autor = k
                };
                Context.RecenzijePrevoznika.Add(recenzija);
                await Context.SaveChangesAsync();
                return Ok("Recenzija je dodata!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("VratiRecenzijeStanice/{stanica}")]
        public async Task<ActionResult> VratiRecenzijeStanice(string stanica)
        {
            var rec = await Context.RecenzijeStanice.Where(p => p.Stanica.Mesto == stanica)
            .Include(p => p.Autor)
            .Include(p => p.Stanica)
            .ToListAsync();
            return Ok(rec);
        }

        [HttpGet]
        [Route("VratiRecenzijePrevoznika/{prevoznik}")]
        public async Task<ActionResult> VratiRecenzijePrevoznika(string prevoznik)
        {
            var rec = await Context.RecenzijePrevoznika.Where(p => p.Prevoznik.Username == prevoznik)
            .Include(p => p.Autor)
            .Include(p => p.Prevoznik)
            .ToListAsync();
            return Ok(rec);
        }

        [HttpDelete]
        [Route("ObrisiRecenzijuPrevoznika/{id}")]
        public async Task<ActionResult> ObrisiRecenzijuPrevoznika(int id)
        {
            if(id <= 0)
                return BadRequest("Nevalidan ID");
            try{
                RecenzijaPrevoznika r = await Context.RecenzijePrevoznika.Where(p =>p.ID == id).FirstOrDefaultAsync();
                if(r == null)
                    return BadRequest("Recenzija ne postoji!");
                Context.RecenzijePrevoznika.Remove(r);
                await Context.SaveChangesAsync();
                return Ok("Recenzija je obrisana");    
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("ObrisiRecenzijuStanice/{id}")]
        public async Task<ActionResult> ObrisiRecenzijuStanice(int id)
        {
            if(id <= 0)
                return BadRequest("Nevalidan ID");
            try{
                RecenzijaStanice r = await Context.RecenzijeStanice.Where(p =>p.ID == id).FirstOrDefaultAsync();
                if(r == null)
                    return BadRequest("Recenzija ne postoji!");
                Context.RecenzijeStanice.Remove(r);
                await Context.SaveChangesAsync();
                return Ok("Recenzija je obrisana");    
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
