using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService()
        {

        }

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); //Acessa a fonte de dados de vendedores es converte para lista
        }

        public void Insert(Seller obj) //Insere no banco de dados o cadastro do novo vendedor
        {
        
            _context.Add(obj);
            _context.SaveChanges();
        }



    }
}
