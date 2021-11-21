using CustomSchemeNinjaApi.Providers.AuthHandlers.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomSchemeNinjaApi.Controllers
{

    [Route("api/[controller]")]
    public class NinjasController : ControllerBase
    {
        private readonly NinjaModel[] ninjas;

        public NinjasController(IHostEnvironment env)
        {
            var text = System.IO.File.ReadAllText(Path.Combine(env.ContentRootPath, @"data/ninja.json"));
            ninjas = JsonConvert.DeserializeObject<NinjaModel[]>(text);
        }

        [HttpGet("alive")]
        public string Alive()
        {
            return "Ninja clan is Alive";
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = AuthSchemeConstants.MyNinjaAuthScheme)]
        public NinjaModel[] Get()
        {
            return ninjas;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = AuthSchemeConstants.MyNinjaAuthScheme)]
        public NinjaModel Get(int id)
        {
            return ninjas.Where(x => x.Id == id).FirstOrDefault();
        }
    }

    public class NinjaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Moniker { get; set; }
        public string[] Techniques { get; set; }
    }
}