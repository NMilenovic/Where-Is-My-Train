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
    public class PrevoznikController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public PrevoznikController(AplikacijaContext context)
        {
            Context = context;
        }
        public static string sha256_hashFunkcija( string value )
        {
             using var hash = SHA256.Create();
             var niz = hash.ComputeHash(Encoding.UTF8.GetBytes( value ) );
             return Convert.ToHexString(niz).ToLower();
        }
        //Dodavanje prevoznika
        [HttpPost]
        [Route("DodajPrevoznika/{pass}")]
        public async Task<ActionResult> DodajPrevoznika([FromBody]Prevoznik prevoznik, string pass)
        {
            if(prevoznik == null)
                return BadRequest("Prevoznik je null!");
            if(string.IsNullOrWhiteSpace(prevoznik.Username) || prevoznik.Username.Length > 50)
                return BadRequest("Nevalidno ime prevoznika");
             try
             {
                var p = await Context.Prevoznici.Where(p => p.Username == prevoznik.Username).FirstOrDefaultAsync();
                if (p != null)
                    return BadRequest("Prevoznik vec postoji");
                prevoznik.Password = sha256_hashFunkcija(pass);
                Context.Prevoznici.Add(prevoznik);
                await Context.SaveChangesAsync();
                return Ok($"{prevoznik.Username} je dodat!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Vracanje prevoznika
        [HttpGet]
        [Route("VratiPrevoznika/{username}/{pass}")]
        public async Task<ActionResult> VratiPrevoznika(string username,string pass)
        {
            var p = await Context.Prevoznici.Where(p => p.Username == username && p.Password == sha256_hashFunkcija(pass))
            .FirstOrDefaultAsync();
            if (p == null)
                return BadRequest("Login failed");
            return Ok(p);
        }

        //Promeni lozinku prevoznika
        [HttpPut]
        [Route("PromeniLozinkuPrevoznika/{prevoznikID}/{sPass}/{nPass}")]
        public async Task<ActionResult> PromeniLozinkuPrevoznika(int prevoznikID,string sPass, string nPass)
        {
            try{
                var prevoznik = await Context.Prevoznici.Where(p => p.ID == prevoznikID).FirstOrDefaultAsync();
                if(prevoznik == null)
                    return BadRequest("Prevoznik je null!");
                if(prevoznik.Password != sha256_hashFunkcija(sPass))
                    return BadRequest("Pogresna trenutna lozinka!");
                prevoznik.Password = sha256_hashFunkcija(nPass);
                await Context.SaveChangesAsync();
                return Ok($"sp: {sha256_hashFunkcija(sPass)}, np:{prevoznik.Password}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PretragaPrevoznika/{str}")]
        public async Task<ActionResult> PretragaPrevoznika(string str)
        {
            if(string.IsNullOrWhiteSpace(str) || str.Length > 50)
                return BadRequest("Nevalidan tekst");
            try{
                var s = await Context.Prevoznici.Where(p => p.Username.Contains(str))
                .ToListAsync();
                if (s == null)
                    return BadRequest("Nema takvog prevoznika");
                return Ok(s);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
