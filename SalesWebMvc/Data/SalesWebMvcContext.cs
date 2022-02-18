using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : DbContext //Esta é classe responsável por se comunicar com o framework e criar as tabelas no banco de dados
    {
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }

        //As variáveis abaixo são classes que são associadas ao DBcontext para a criação das tabelas e também atributos
        public DbSet<Department> Department { get; set; } 
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }

    }
}
