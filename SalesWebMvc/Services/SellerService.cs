using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;


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

        public Seller FindbyId(int id)
        {
            return _context.Seller.Include(obj => obj.department).FirstOrDefault(obj => obj.Id == id); //Para carregar outros objetos jutamente com a busca principal

        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id)) //Verifica se há algum vendedor com o ID indicado
            {
                throw new NotFoundException("Id não encontrado");

            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }









    }
}
