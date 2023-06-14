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
    public class AdministratorController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public AdministratorController(AplikacijaContext context)
        {
            Context = context;
        }
        public static string sha256_hashFunkcija( string value )
        {
             using var hash = SHA256.Create();
             var niz = hash.ComputeHash(Encoding.UTF8.GetBytes( value ) );
             return Convert.ToHexString(niz).ToLower();
        }
        //Dodavanje administratora
        [HttpPost]
        [Route("DodajAdministratora/{pass}")]
        public async Task<ActionResult> DodajAdministratora([FromBody]Administrator administrator, string pass)
        {
            if(administrator == null)
                return BadRequest("Administrator je null!");
            if(string.IsNullOrWhiteSpace(administrator.Username) || administrator.Username.Length > 50)
                return BadRequest("Nevalidno ime administratora");
             try
             {
                
                var a = await Context.Administratori.Where(p => p.Username == administrator.Username).FirstOrDefaultAsync();
                
                if(a != null)
                    return BadRequest("Korisnik sa zadatim korisnickim imenom vec postoji");
                administrator.Password = sha256_hashFunkcija(pass);
                Context.Administratori.Add(administrator);
                await Context.SaveChangesAsync();
                return Ok($"{administrator.Username} je dodat!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Vracanje administratora
        [HttpGet]
        [Route("VratiAdministratora/{username}/{pass}")]
        public async Task<Administrator> VratiAdminstratora(string username,string pass)
        {
            return await Context.Administratori.Where(p => p.Username == username && p.Password == sha256_hashFunkcija(pass)).FirstOrDefaultAsync();
            
        }
    }
   

}