using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Aplikacija.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AplikacijaController : ControllerBase
    {
        
        AplikacijaContext Context {get;set;}

        public AplikacijaController(AplikacijaContext context)
        {
            Context = context;
        }
    }
}
