using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;

        //Criar a injeção de dependencia, ou seja, quano um seedingservice for criado, automaticamente 
        //ele vai receber uma instancia do Contex tambem
        public SeedingService(SalesWebMVCContext contex)
        {
            _context = contex;
        }

        public void Seed()
        {
            //verifica se existem registros nas tabelas
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecords.Any())
            {
                //se existir, da um return para cortar a execução do método, ou seja, não faz nada
                return;
            }

            Department d1 = new Department(1, "Human Resources");
            Department d2 = new Department(2, "Administrative");
            Department d3 = new Department(3, "Executives");
            Department d4 = new Department(4, "Delivery");

            Seller s1 = new Seller(1, "Bob", "bob@gmail.com", new DateTime(1982, 11, 20), 8541.58, d1);
            Seller s2 = new Seller(2, "Mary", "mary@gmail.com", new DateTime(1975, 05, 03), 18541.50, d3);
            Seller s3 = new Seller(3, "Gabriel", "doria@gmail.com", new DateTime(1982, 10, 3), 3500.00, d1);
            Seller s4 = new Seller(4, "Tery", "Tery@gmail.com", new DateTime(2000, 11, 1), 2478.03, d1);
            Seller s5 = new Seller(5, "Mark", "mark@gmail.com", new DateTime(1978, 05, 03), 25874, d2);
            Seller s6 = new Seller(6, "Wilson", "wilson@gmail.com", new DateTime(1999, 12, 3), 5000, d4);

            SalesRecord sr1 = new SalesRecord(1, new DateTime(2022, 12, 3), 587, SaleStatus.Billed, s1);
            SalesRecord sr2 = new SalesRecord(2, new DateTime(2022, 12, 20), 352, SaleStatus.Billed, s1);
            SalesRecord sr3 = new SalesRecord(3, new DateTime(2022, 12, 20), 40.05, SaleStatus.Billed, s1);
            SalesRecord sr4 = new SalesRecord(4, new DateTime(2022, 12, 20), 183, SaleStatus.Pending, s1);
            SalesRecord sr5 = new SalesRecord(5, new DateTime(2022, 12, 3), 23.78, SaleStatus.Canceled, s3);
            SalesRecord sr6 = new SalesRecord(6, new DateTime(2022, 12, 3), 100, SaleStatus.Billed, s3);
            SalesRecord sr7 = new SalesRecord(7, new DateTime(2022, 12, 3), 113, SaleStatus.Billed, s4);
            SalesRecord sr8 = new SalesRecord(8, new DateTime(2022, 12, 3), 120.09, SaleStatus.Billed, s4);
            SalesRecord sr9 = new SalesRecord(9, new DateTime(2022, 12, 3), 87, SaleStatus.Billed, s4);
            SalesRecord sr10 = new SalesRecord(10, new DateTime(2022, 12, 3), 22, SaleStatus.Pending, s1);
            SalesRecord sr11 = new SalesRecord(11, new DateTime(2022, 12, 20), 33, SaleStatus.Billed, s3);
            SalesRecord sr12 = new SalesRecord(12, new DateTime(2022, 12, 20), 44, SaleStatus.Billed, s3);
            SalesRecord sr13 = new SalesRecord(13, new DateTime(2022, 12, 20), 57, SaleStatus.Pending, s3);
            SalesRecord sr14 = new SalesRecord(14, new DateTime(2022, 12, 3), 13.4, SaleStatus.Pending, s3);
            SalesRecord sr15 = new SalesRecord(15, new DateTime(2022, 12, 3), 88, SaleStatus.Pending, s3);
            SalesRecord sr16 = new SalesRecord(16, new DateTime(2022, 12, 3), 77, SaleStatus.Billed, s1);
            SalesRecord sr17 = new SalesRecord(17, new DateTime(2022, 12, 3), 20.09, SaleStatus.Pending, s3);
            SalesRecord sr18 = new SalesRecord(18, new DateTime(2022, 12, 3), 12, SaleStatus.Billed, s4);

            _context.Department.AddRange(d1,d2,d3,d4);
            _context.Seller.AddRange(s1,s2,s3,s4);
            _context.SalesRecords.AddRange(sr1,sr2,sr3,sr4,sr5,sr6,sr7,sr8,sr9,sr10,sr11,sr12,sr13,sr14,sr15,sr16,sr17,sr18);

            _context.SaveChanges();


        }

    }
}
