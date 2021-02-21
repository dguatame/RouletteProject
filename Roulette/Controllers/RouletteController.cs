using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Roulette.Models;
using Roulette.Repositories;

namespace Roulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouletteController : Controller
    {
        // GET: api/Roulette/GetAll
        [HttpGet("GetAll")]
        public IEnumerable<RouletteModel> Get()
        {
            RouletteRepositorySQL rouletteRepository = new RouletteRepositorySQL();

            return rouletteRepository.GetAllRoulettes();
        }
        // POST api/Roulette/AddRoulette
        [HttpPost("AddRoulette")]
        public int Post()
        {
            RouletteModel roulette = new RouletteModel { IsOpen = false };
            RouletteRepositorySQL rouletteRepository = new RouletteRepositorySQL();
            int rouletteId = rouletteRepository.AddRoulette(roulette: roulette);

            return rouletteId;
        }
        // PUT api/Roulette/OpenRoulette/5
        [HttpPut("OpenRoulette/{id}")]
        public string Put(int id)
        {
            RouletteRepositorySQL rouletteRepository = new RouletteRepositorySQL();
            RouletteModel roulette = rouletteRepository.GetRouletteById(rouletteId: id);
            if (roulette.IsValidToOpen())
            {
                roulette.IsOpen = true;
                rouletteRepository.ChangeStateRoulette(roulette: roulette);

                return $"La apertura de la ruleta {id} fue exitosa";
            }
            else
            {

                return $"La ruleta {id} ya está abierta o no existe";
            }
        }
    }
}
