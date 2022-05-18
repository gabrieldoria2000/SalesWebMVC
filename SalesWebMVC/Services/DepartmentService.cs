using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //METODO LISTAR SÍNCRONO
        public List<Department> FindAll()
        {
       //     //return _context.Department.ToList();
        //    //Ordena por nome e retorna a lista já ordenada
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

        //METODO LISTAR ASSINCRONO - ou seja essa execucao nao vai bloquear a aplicação.
        public async Task<List<Department>> FindAllAsync()
        {
            //return _context.Department.ToList();
            //Ordena por nome e retorna a lista já ordenada
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();   
        }
    }
}
