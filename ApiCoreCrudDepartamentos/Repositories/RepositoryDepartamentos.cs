using ApiCoreCrudDepartamentos.Data;
using ApiCoreCrudDepartamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCoreCrudDepartamentos.Repositories
{
    public class RepositoryDepartamentos
    {

        private DepartamentosContext context;

        public RepositoryDepartamentos(DepartamentosContext context)
        {
            this.context = context;
        }

        public async Task<List<Departamento>>
            GetDepartamentosAsync()
        {
            return await 
                this.context.Departamentos.ToListAsync();
        }

        public async Task<Departamento>
            FindDepartamentoAsync(int id)
        {
            return await
                this.context.Departamentos.
                FirstOrDefaultAsync(z => z.IdDepartamento == id);
        }

        public async Task InsertDeptAsync
            (int id, string nombre, string loc)
        {
            Departamento dept = new Departamento();
            dept.IdDepartamento = id;
            dept.Nombre = nombre;
            dept.Localidad = loc;
            this.context.Departamentos.Add(dept);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateDeptAsync
           (int id, string nombre, string loc)
        {
            Departamento dept = await 
                this.FindDepartamentoAsync(id);
            dept.Nombre = nombre;
            dept.Localidad = loc;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteDeptAsync(int id)
        {
            Departamento dept = await
                this.FindDepartamentoAsync(id);
            this.context.Departamentos.Remove(dept);
            await this.context.SaveChangesAsync();
        }
    }
}
