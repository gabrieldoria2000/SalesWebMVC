using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="{0} é requerido!")]
        [StringLength(60, MinimumLength =3, ErrorMessage ="Tamanho do campo {0} tem que estar entre {2} e {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} é requerido!")]
        [EmailAddress(ErrorMessage ="Entre com um email válido!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é requerido!")]
        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "{0} é requerido!")]
        [Display(Name = "Base Salary")]
        [Range(100,50000,ErrorMessage ="{0} deve estar entre {1} e {2}")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthdate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            Birthdate = birthdate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord Sr)
        {
            Sales.Add(Sr);
        }

        public void RemoveSales(SalesRecord Sr)
        {
            Sales.Remove(Sr);
        }

        public double TotalSales(DateTime dtIni, DateTime dtFim)
        {
            return Sales.Where( sr => sr.Date >= dtIni && sr.Date <= dtFim).Sum(sr => sr.Amount);
        }
    }
}
