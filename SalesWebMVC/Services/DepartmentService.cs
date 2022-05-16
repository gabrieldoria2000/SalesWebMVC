using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            //return _context.Department.ToList();
            //Ordena por nome e retorna a lista já ordenada
            return _context.Department.OrderBy(x => x.Name).ToList();   
        }
    }
}
