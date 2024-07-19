using Acudir.Test.Core.Entities;
using Acudir.Test.Core.Interfaces;

namespace Acudir.Test.Apis.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public TestController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet("test-data")]
        public async Task<IActionResult> TestData()
        {
            // Verifica si se pueden obtener datos
            var person = await _personRepository.Get(1); // Supone que el ID 1 debe existir

            if (person == null)
            {
                return NotFound("No se encontraron datos");
            }

            return Ok(person);
        }

        [HttpGet("test-create")]
        public async Task<IActionResult> TestCreate()
        {
            // Prueba crear un nuevo registro
            var newPerson = new Person
            {
                id = 10,
                NombreCompleto = "Persona de Prueba",
                Edad = 25,
                Domicilio = "Calle de Prueba",
                Telefono = "123456789",
                Profesion = "Tester",
                Active = true
            };

            await _personRepository.Create(newPerson);
            var person = await _personRepository.Get(10); // Verifica si el nuevo registro se ha creado correctamente

            if (person == null)
            {
                return NotFound("No se encontró el registro creado");
            }

            return Ok(person);

        }
        
        //Devolver una lista que retorne Personas
        [HttpGet("GetAll")]
        public ActionResult<Object>? GetAll()
        {
            return null;
        } 
        //Post Guardar una Persona o mas

        //Put Modificarlas
    }
}