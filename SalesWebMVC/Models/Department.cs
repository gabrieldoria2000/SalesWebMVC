using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {

        }


        //NÃO COLOCAR A REFERENCIA DE COLEÇÕES NO COSTRUTOR
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller sr)
        {
            Sellers.Add(sr);
        }

        public double TotalSales(DateTime dtini, DateTime dtfim)
        {
            return Sellers.Sum(seller => seller.TotalSales(dtini, dtfim));
        }
    }
}
