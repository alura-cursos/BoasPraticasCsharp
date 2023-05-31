using Alura.Adopet.API.Dados.Context;
using Alura.Adopet.API.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alura.Adopet.API.Controladores
{
    [ApiController]
    [Route("/pet/")]
    public class PetController:ControllerBase
    {
        private DataBaseContext _context;
        public PetController(DataBaseContext ctx)
        {
            this._context = ctx;
        }
        [HttpGet]
        [Route("list")]
        public async Task<IResult> ListaDePet()
        {
            var listaDePet = await _context.Pets.Include(x=>x.Proprietario).ToListAsync();
            return Results.Ok(listaDePet);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IResult> CadatrarPet([FromBody]Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            _context.SaveChanges();
            return Results.Ok("Pet cadastrado com sucesso!");
        }
    }
}
