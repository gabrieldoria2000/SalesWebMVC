using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {

        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int Id)
        {
            //return  _context.Seller.FirstOrDefault(x => x.Id == Id);
            // para usar o INCLUDE adicionar o using Microsfot.EntityFramework
            // É ASSIM QUE FAZ O JOIN!!!! :)
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == Id);
        }

        public void Remove(int Id)
        {
            var obj = _context.Seller.Find(Id);
            _context.Seller.Remove(obj);
            //removeu do DbSet
            _context.SaveChanges();

        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                //Se não existir nenhum vendedor com o mesmo ID que foi passado
                throw new NotFoundExceptions("ID not Found!");

            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                //captura a exption de concorrencia do framework e lança uma que nós criamos personalizada
                throw new DbConcurrencyException(ex.Message);
            }
            
        }
    }
}
