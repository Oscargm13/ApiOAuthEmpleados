using ApiOAuthEmpleados.Data;
using ApiOAuthEmpleados.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthEmpleados.Repositories
{
    public class RepositoryHospital
    {
        private HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await this.context.Empleados.ToListAsync();
        }

        public async Task<List<string>> GetOficios()
        {
            return await this.context.Empleados.Select(e => e.Oficio).Distinct().ToListAsync();
        }

        public async Task<Empleado> FindEmpleado(int idEmpleado)
        {
            return await this.context.Empleados.FirstOrDefaultAsync(z => z.IdEmpleado == idEmpleado);
        }

        public async Task<Empleado> LogInEmpleadoAsync(string apellido, int idEmpleado)
        {
            return await this.context.Empleados.Where(x => x.Apellido == apellido && x.IdEmpleado == idEmpleado)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Empleado>> GetCompisEmpleadoAsync(int idDepartamento)
        {
            return await this.context.Empleados.Where(x => x.IdDepartamento == idDepartamento).ToListAsync();
        }
        public async Task<List<Empleado>> GetEmpleadosbyOficio(List<string> oficios)
        {
            var consulta = (from datos in this.context.Empleados where oficios.Contains(datos.Oficio) select datos);
            return await consulta.ToListAsync();
        }
        public async Task<List<Empleado>> IncrementarSalariosAsync(int incremento, List<string> oficios)
        {
            List<Empleado> empleados = await this.GetEmpleadosbyOficio(oficios);

            foreach (var empleado in empleados)
            {
                empleado.Salario += incremento;
            }

            await this.context.SaveChangesAsync();

            return empleados;
        }
    }
}
