using ApiCoreCrudDepartamentos.Models;
using ApiCoreCrudDepartamentos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreCrudDepartamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private RepositoryDepartamentos repo;
        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>>
            GetDepartamentos()
        {
            return await this.repo.GetDepartamentosAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Departamento>>
            FindDepartamento(int id)
        {
            return await this.repo.FindDepartamentoAsync(id);
        }

        //los metodos por defecto de post o put, reciben un objeto 
        //sim quisieramos recibir los datos por parametros,
        //debemos usar route
        //podemos personalizar la respuesta en el caso que no nos
        //guste algo, pudiendo devolver not fund, badrequest

        [HttpPost]
        public async Task<ActionResult> PostDepartamento
            (Departamento departamento)
        {
            await this.repo.InsertDeptAsync(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //PODEMOS PERSONALIZAR LA RESPUESTA
            if (await this.repo.FindDepartamentoAsync(id) == null)
            {
                //NO EXISTE EL DEPARTAMENTO PARA ELIMINARLO
                return NotFound();
            }
            else
            {
                await this.repo.DeleteDeptAsync(id);
                return Ok();
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutDepartamento
            (Departamento departamento)
        {
            await this.repo.UpdateDeptAsync(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
            return Ok();
        }


        //podemso tener todos los metodos de accion que queramos 
        //pero debemos personalizarlos con Route
        [HttpPost]
        [Route("[action]/{id}/{nombre}/{loc}")]
        public async Task<ActionResult> InsertParams
            (int id, string nombre, string loc)
        {
            await this.repo.InsertDeptAsync(id, nombre, loc);
            return Ok();
        }

        //tambien podemos combinar recibir obj  con routes
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<ActionResult> UpdateParams
            (int id, Departamento dept)
        {
            await this.repo.UpdateDeptAsync(id,
                dept.Nombre, dept.Localidad);
            return Ok();
        }
    }
}
