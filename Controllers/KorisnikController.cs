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
    public class KorisnikController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public KorisnikController(AplikacijaContext context)
        {
            Context = context;
        }
         public static string sha256_hashFunkcija( string value )
        {
             using var hash = SHA256.Create();
             var niz = hash.ComputeHash(Encoding.UTF8.GetBytes( value ) );
             return Convert.ToHexString(niz).ToLower();
        }

         //Dodavanje korisnika
        [HttpPost]
        [Route("DodajKorisnika/{pass}")]
        public async Task<ActionResult> DodajKorisnika([FromBody]Korisnik korisnik, string pass)
        {
            if(korisnik == null)
                return BadRequest("Korsinik je null!");
            if(string.IsNullOrWhiteSpace(korisnik.Username) || korisnik.Username.Length > 50)
                return BadRequest("Nevalidno ime administratora");
             try
             {  
                var k = await Context.Korisnici.Where(x => x.Username == korisnik.Username).FirstOrDefaultAsync();
                if(k != null)
                    return BadRequest("Korisnik vec postoji!");
                korisnik.Password = sha256_hashFunkcija(pass);
                korisnik.Ban = false;
                Context.Korisnici.Add(korisnik);
                await Context.SaveChangesAsync();
                return Ok($"{korisnik.Username} je dodat!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Vracanje korisnika
        [HttpGet]
        [Route("VratiKorisnika/{username}/{pass}")]
        public async Task<Korisnik> VratiKorisnika(string username,string pass)
        {
            return await Context.Korisnici.Where(p => p.Username == username && p.Password == sha256_hashFunkcija(pass))
            .Include(p => p.recenzijePrevoznika)
            .Include(p => p.recenzijeStanice)
            .FirstOrDefaultAsync();
        }

        [HttpPut]
        [Route("PromeniLozinku/{korisnikID}/{sPass}/{nPass}")]
        public async Task<ActionResult> PromeniLozinku(int korisnikID,string sPass, string nPass)
        {
            try{
                var korisnik = await Context.Korisnici.Where(p => p.ID == korisnikID).FirstOrDefaultAsync();
                if(korisnik == null)
                    return BadRequest("Korisnik je null!");
                if(korisnik.Password != sha256_hashFunkcija(sPass))
                    return BadRequest("Pogresna trenutna lozinka!");
                korisnik.Password = sha256_hashFunkcija(nPass);
                Context.Korisnici.Update(korisnik);
                await Context.SaveChangesAsync();
                return Ok($"sp: {sha256_hashFunkcija(sPass)}, np:{korisnik.Password}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("BanujKorinsika/{username}")]
        public async Task<ActionResult> BanujKorisnika(string username)
        {
            if(string.IsNullOrWhiteSpace(username) || username.Length > 50)
                return BadRequest("Nevalidno korisnicko ime!");
            try{
                Korisnik k = await Context.Korisnici.Where(p=> p.Username == username && p.Ban == false).FirstOrDefaultAsync();
                if (k == null)
                    return BadRequest("Korisnik ne postoji!");
                k.Ban = true;
                Context.Korisnici.Update(k);
                await Context.SaveChangesAsync();
                return Ok("Korisnik je  banovan!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("PronadjiKorisnike/{str}")]
        public async Task<ActionResult> PronadjiKorisnike(string str)
        {
            
            return Ok(await Context.Korisnici.Where(p => p.Username.Contains(str)).ToListAsync());
        }
        

        [HttpPut]
        [Route("UnbanKorisnika/{username}")]
        public async Task<ActionResult> UnbanKorisnika(string username)
        {
            if(string.IsNullOrWhiteSpace(username) || username.Length > 50)
                return BadRequest("Nevalidno korisnicko ime");
            try{
                Korisnik k = await Context.Korisnici.Where(p=> p.Username == username && p.Ban == true).FirstOrDefaultAsync();
                if (k == null)
                    return BadRequest("Korisnik ne postoji!");
                k.Ban = false;
                Context.Korisnici.Update(k);
                await Context.SaveChangesAsync();
                return Ok("Korisnik je  unbanovan!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
