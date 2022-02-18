using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;
namespace SalesWebMvc.Data
{
    public class SeedingService //Esta classe é responsável por popular as tabelas
    {
        private SalesWebMvcContext _context; //A classe responsável pelo DBcontext é associado para que possa referencia-lo ao fazer a injeção de dependencia na tabela

        public SeedingService()
        {

        }

        public SeedingService(SalesWebMvcContext context) //Quando um seedingService for criado automaticamente ele vai receber uma instancia do context também
        {
            _context = context;
        }

        public void Seed() //Responsável por popular a base de dados
        {
            if(_context.Department.Any() || //Função link Any() = Verificar se há algum registro na tabela departament
                _context.Seller.Any() ||
                _context.SalesRecord.Any()) 
            {
                return; // Faz a validação do banco e verifica se já foi populado. Caso já tenha algum dado, o método simplesmente não retorna nada
            }

            //Nesta área os objetos são instanciados e populados para preencher o banco de dados

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Cristian", "Cristian@gmail.com", 3000.0, new DateTime(1999,07,31), d1);
            Seller s2 = new Seller(2, "Rendrix", "Rendrix@gmail.com", 3000.0, new DateTime(1994,07,31), d1);
            Seller s3 = new Seller(3, "Maicon", "Maicon@gmail.com", 2500.0, new DateTime(1992,07,31), d2);
            Seller s4 = new Seller(4, "Diego", "Diego@gmail.com", 10000.0, new DateTime(2002,05,28), d4);
            Seller s5 = new Seller(5, "Giovanna", "Giovanna@gmail.com", 7000.0, new DateTime(1998,11,05), d3);


            SalesRecord sr1 = new SalesRecord(1, new DateTime(2022, 02, 17), 1500.00, SaleStatus.Billed,s1);
            SalesRecord sr2 = new SalesRecord(2, new DateTime(2022, 02, 01), 5000.00, SaleStatus.Pending,s2);
            SalesRecord sr3 = new SalesRecord(3, new DateTime(2022, 02, 03), 5010.00, SaleStatus.Canceled,s3);
            SalesRecord sr4 = new SalesRecord(4, new DateTime(2022, 02, 04), 10500.00, SaleStatus.Pending,s4);
            SalesRecord sr5 = new SalesRecord(5, new DateTime(2022, 02, 05), 1580.00, SaleStatus.Billed,s4);
            SalesRecord sr6 = new SalesRecord(6, new DateTime(2022, 02, 06), 15500.00, SaleStatus.Billed,s1);
            SalesRecord sr7 = new SalesRecord(7, new DateTime(2022, 02, 06), 1550.00, SaleStatus.Canceled,s5);
            SalesRecord sr8 = new SalesRecord(8, new DateTime(2022, 02, 07), 1400.00, SaleStatus.Billed,s3);
            SalesRecord sr9 = new SalesRecord(9, new DateTime(2022, 02, 08), 100.00, SaleStatus.Pending,s1);
            SalesRecord sr10 = new SalesRecord(10, new DateTime(2022, 02, 18), 100.00, SaleStatus.Billed,s2);

            //Usando o Entity frameworks
            //Adicionando as informações na tabela responsável pelo DBContext 
            _context.Department.AddRange(d1,d2,d3,d4);//AddRange = Permite adicionar vários departamentos ou objetos de uma só vez

            _context.Seller.AddRange(s1, s2, s3, s4, s5);

            _context.SalesRecord.AddRange(sr1, sr2, sr3, sr4, sr5, sr6, sr7, sr8, sr9, sr10);

            _context.SaveChanges(); //Salva e confirma as alterções no banco de dados
        
        }

    }
}
