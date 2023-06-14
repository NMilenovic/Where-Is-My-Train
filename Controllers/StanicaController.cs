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
    public class StanicaController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public StanicaController(AplikacijaContext context)
        {
            Context = context;
        }

        [HttpPost]
        [Route("DodajStanicu/{mesto}")]
        public async Task<ActionResult> DodajStanicu(string mesto)
        {
            if(string.IsNullOrWhiteSpace(mesto) || mesto.Length > 50)
                return BadRequest("Nevalidno mesto");
            try
            {
                mesto = mesto.ToLower();
                var s = Context.Stanice.Where(p => p.Mesto == mesto).FirstOrDefault();
                if (s != null)
                    return BadRequest("Vec postoji stanica u tom mestu!");
                Stanica stanica = new Stanica{
                    Mesto = mesto,

                };
                Context.Stanice.Add(stanica);
                await Context.SaveChangesAsync();
                return Ok("Stanica je dodata!");
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("PretragaStanice/{str}")]
        public async Task<ActionResult> PretragaStanice(string str)
        {
            if(string.IsNullOrWhiteSpace(str) || str.Length > 50)
                return BadRequest("Nevalidan tekst");
            try{
                var s = await Context.Stanice.Where(p => p.Mesto.Contains(str)).ToListAsync();
                if (s == null)
                    return BadRequest("Nema takve stanice");
                return Ok(s);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
