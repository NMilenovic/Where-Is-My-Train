using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Aplikacija.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoznjaController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public VoznjaController(AplikacijaContext context)
        {
            Context = context;
        }


        [HttpPost]
        [Route("DodajVoznju/{pStanica}/{kStanica}/{datum}/{datumStizanja}")]
        public async Task<ActionResult> DodajVoznju(string pStanica,string kStanica, DateTime datum, DateTime datumStizanja)
        {
            if(string.IsNullOrWhiteSpace(pStanica))
                return BadRequest("Pocetna stanica nije unesena!");
            if(string.IsNullOrWhiteSpace(kStanica))
                return BadRequest("Krajnja stanica nije unesena!");
            if( datum.CompareTo(datumStizanja) > 0)
                return BadRequest("Datum dolaska ne moze biti manji od datuma polaska!");
            try{
                var pS = await Context.Stanice.Where(p => p.Mesto == pStanica).FirstOrDefaultAsync();
                if(pS == null)
                    return BadRequest("Pocetna stanica ne postoji!");
                var kS = await Context.Stanice.Where(p =>p.Mesto == kStanica).FirstOrDefaultAsync();
                if(kS == null)
                    return BadRequest("Krajnja stanica ne postoji!");

                Voznja voznja = new Voznja{
                    PocetnaStanica = pS,
                    KrajnaStanica = kS,
                    Termin = datum,
                    Stize = datumStizanja
                };
                Context.Voznje.Add(voznja);
                await Context.SaveChangesAsync();
                return Ok("Voznja je dodata");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    
        [HttpGet]
        [Route("VratiVoznje/{pStanica}/{kStanica}/{datum}")]
        public async Task<ActionResult> VratiVoznje(string pStanica, string kStanica, DateTime datum)
        {
            var voznje =  await Context.Voznje.Where(p => p.PocetnaStanica.Mesto == pStanica && p.KrajnaStanica.Mesto == kStanica && p.Termin.Date == datum.Date  && p.Prevoznik != null)
            .Include(x => x.PocetnaStanica)
            .Include(x => x.KrajnaStanica)
            .Include(x => x.Prevoznik)
            .ToListAsync();
            return Ok(voznje);
        }

        [HttpGet]
        [Route("VratiSlobodneVoznje/{pStanica}/{kStanica}/{datum}")]
        public async Task<ActionResult> VratiSlobodneVoznje(string pStanica, string kStanica, DateTime datum)
        {
            var  slobVoznje = await Context.Voznje.Where(p => p.PocetnaStanica.Mesto == pStanica && p.KrajnaStanica.Mesto == kStanica && p.Termin.Date == datum.Date  && p.Prevoznik == null).Include(x => x.PocetnaStanica).Include(x => x.KrajnaStanica).ToListAsync();
            return Ok(slobVoznje);
        }

        [HttpPut]
        [Route("DodajPrevoznika/{idVoznje}/{prevoznikUser}")]
        public async Task<ActionResult> DodajPrevoznika(int idVoznje,string prevoznikUser)
        {
            if(idVoznje <= 0)
                return BadRequest("ID je 0 ili manji!");
            try{
                Prevoznik p = await Context.Prevoznici.Where(p => p.Username == prevoznikUser)
                .Include(p =>p.Zahtevi)
                .ThenInclude(p => p.ListaVoznji)
                .ThenInclude(p => p.Prevoznik)
                .FirstOrDefaultAsync();
                if(p == null)
                    return BadRequest("Prevoznik  ne postoji!");
                Zahtev z = p.Zahtevi.Where(p => p.ListaVoznji.ID == idVoznje).FirstOrDefault();
                
                //Provera da li je poslat zahtev za voznju
                if(z == null)
                    return BadRequest("Zahtev za voznju ne postoji!");
                var v = z.ListaVoznji;
                if(v.Prevoznik != null)
                    {
                        var zah = await Context.Zahtevi.Where(p => p.ListaVoznji == v).ToListAsync();
                        zah.ForEach(p =>{
                            Context.Zahtevi.Remove(p);
                        });
                        await Context.SaveChangesAsync();
                        return BadRequest("Voznja vec ima prevoznika!");
                    }
                   v.Prevoznik = p;
                   var zahtevi = await Context.Zahtevi.Where(p => p.ListaVoznji == v).ToListAsync();
                   zahtevi.ForEach(p =>{
                        Context.Zahtevi.Remove(p);
                        });
                   Context.Voznje.Update(v);
                   await Context.SaveChangesAsync();

                 return Ok("Prevoznik je dodat na voznju!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("IzmeniVoznju/{id}/{pS}/{kS}/{termin}/{stize}")]
        public async Task<ActionResult> IzmeniVoznju(int id,string pS, string kS,DateTime termin, DateTime stize)
        {
            if(id <= 0)
            {
                return BadRequest("Nevalidna vrednost ID");
            }
            if(string.IsNullOrWhiteSpace(pS) || string.IsNullOrWhiteSpace(kS))
                return BadRequest("Imena stanice ne smeju piti prazna!");
            if(termin >= stize)
                return BadRequest("Termin kada voz stize ne moze biti pre nego sto krene!");
            try{

                var pocetnaS = await Context.Stanice.Where(p => p.Mesto == pS).FirstOrDefaultAsync();
                var krajnjaS = await Context.Stanice.Where(p => p.Mesto == kS).FirstOrDefaultAsync();

                if(pocetnaS == null || krajnjaS == null)
                    return BadRequest("Neka od stanica ne postoji!");

                var v = await Context.Voznje.FindAsync(id);
                if (v == null)
                    return BadRequest("Voznja ne postoji!");

                v.PocetnaStanica  = pocetnaS;
                v.KrajnaStanica = krajnjaS;
                v.Termin = termin;
                v.Stize = stize;

                Context.Voznje.Update(v);
                await Context.SaveChangesAsync();
                return Ok("Voznja je izmenjena");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("PosaljiZahtev/{prevoznikUsername}/{voznjaID}")]
        public async Task<ActionResult> PosaljiZahtev(string prevoznikUsername, int voznjaID)
        {
            if(voznjaID < 0)
                return BadRequest("Voznja je nevalidna");
            try
            {
                var p = await Context.Prevoznici.Where(x => x.Username == prevoznikUsername)
                .Include(p => p.Zahtevi)
                .FirstOrDefaultAsync();
                Ok(p);
                if(p == null)
                    return BadRequest("Prevoznik ne postoji");
                var v = await Context.Voznje.Where(p => p.ID == voznjaID).FirstOrDefaultAsync();
                if (v == null)
                    return BadRequest("Voznja ne postoji");
                if (v.Prevoznik != null)
                    return BadRequest("Voznja je vec zauzeta");
                var zah = await Context.Zahtevi.Where(p => p.Prevoznik.ID == p.ID && p.ListaVoznji.ID == v.ID ).FirstOrDefaultAsync();
                if( zah != null)
                    return BadRequest("zahtev za ovu voznju je vec posalat");
                Zahtev z = new Zahtev{
                     Prevoznik = p,
                     ListaVoznji = v,
                     UsernamePrevoznik = p.Username
                };
                Context.Zahtevi.Update(z);
                await Context.SaveChangesAsync();
                return Ok("Voznja je dodata u listu zahteva");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("VratiSveZahteve")]
        public async Task<ActionResult> VratiSveZahteve()
        {
            var z = await Context.Zahtevi.Include(p => p.ListaVoznji)
            .ThenInclude(p => p.PocetnaStanica)
            .Include(p => p.ListaVoznji)
            .ThenInclude(p => p.KrajnaStanica)
            .ToListAsync();
            return Ok(z);
        }

        
        [HttpDelete]
        [Route("ObrisiVoznju/{voznjaID}")]
        public async Task<ActionResult> ObrisiVoznju(int voznjaID)
        {
            if(voznjaID < 0)
                return BadRequest("Los ID voznje");
            try{
                Voznja v = await Context.Voznje.Where(p => p.ID == voznjaID)
                .FirstOrDefaultAsync();
                List<Zahtev> z = await Context.Zahtevi.Where(p => p.ListaVoznji == v).ToListAsync();
                List<Karta> k = await Context.Karte.Where(p => p.Voznja == v).ToListAsync();
                foreach (Zahtev zah in z)
                {
                    Context.Zahtevi.Remove(zah);
                    await Context.SaveChangesAsync();
                }
                foreach (Karta karta in k)
                {
                    Context.Karte.Remove(karta);
                    await Context.SaveChangesAsync();
                }
                await Context.SaveChangesAsync();
                if(v == null)
                    return BadRequest("Voznja sa tim ID-om ne postoji");
                Context.Voznje.Remove(v);
                await Context.SaveChangesAsync();
                return Ok("Voznja je obrisana");       
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}
