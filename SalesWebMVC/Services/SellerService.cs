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

        //public List<Seller> FindAll()
        // {
        //     return _context.Seller.ToList();
        // }
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }


        //public void Insert(Seller obj)
        //{
        //    //obj.Department = _context.Department.First();
        //    _context.Add(obj);
        //    _context.SaveChanges();
        //}

        //Insert assincrono!
        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


        // public Seller FindById(int Id)
        // {
        //     //return  _context.Seller.FirstOrDefault(x => x.Id == Id);
        // para usar o INCLUDE adicionar o using Microsfot.EntityFramework
        // É ASSIM QUE FAZ O JOIN!!!! :)
        //   return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == Id);
        // }

        //metodo assincrono
        public async Task<Seller> FindByIdAsync(int Id)
         {
        //     //return  _context.Seller.FirstOrDefault(x => x.Id == Id);
        // para usar o INCLUDE adicionar o using Microsfot.EntityFramework
        // É ASSIM QUE FAZ O JOIN!!!! :)
           return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == Id);
         }

        //public void Remove(int Id)
       // {
        //    var obj = _context.Seller.Find(Id);
        //    _context.Seller.Remove(obj);
            //removeu do DbSet
        //    _context.SaveChanges();

       // }

        //REMOVE assincrono
        public async Task RemoveAsync(int Id)
        {
            var obj = await _context.Seller.FindAsync(Id);
            _context.Seller.Remove(obj);
            //removeu do DbSet
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                //Se não existir nenhum vendedor com o mesmo ID que foi passado
                throw new NotFoundExceptions("ID not Found!");

            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                //captura a exption de concorrencia do framework e lança uma que nós criamos personalizada
                throw new DbConcurrencyException(ex.Message);
            }
            
        }
    }
}
