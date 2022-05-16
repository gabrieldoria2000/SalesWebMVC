using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
