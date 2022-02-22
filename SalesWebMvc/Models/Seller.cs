using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int  Id { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")] //Campo obrigatório
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0}Tamanho do nome deve ser entre {1} e {2}")] //{0} pega automaticamente o nome do atributo {1} pega o o minimo e {2} o maximo
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} Obrigatório")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} tem que ser entre {1} to {2}")]
        public double BaseSalary { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Display(Name = "Birth Date")] //Customiza o que irá aparecer em tela
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        public Department department { get; set; }
        public int DepartmentId { get; set; } //Para que o framework consiga indentificar que essa variavel é uma chave esgtrangeira, é nessário criar desta forma sempre seguindo a sintaxe original juntamente com o sufixo ID
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {


        }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            this.department = department;
        }


        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final) //Método que recebe uma data, e soma o valor das vendas de um vendedor especifico em um determinado espaço de tempo
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }


    }
}
