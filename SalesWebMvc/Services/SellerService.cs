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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync(); //Acessa a fonte de dados de vendedores es converte para lista
        }

        public async Task InsertAsync(Seller obj) //Insere no banco de dados o cadastro do novo vendedor
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindbyIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.department).FirstOrDefaultAsync(obj => obj.Id == id); //Para carregar outros objetos jutamente com a busca principal

        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException("Não é possivel deletar o vendedor pois no cadastro há vendas vinculadas a este vendedor");
            }
         
        }

        public async Task Update(Seller obj)
        {
            var hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny) //Verifica se há algum vendedor com o ID indicado
            {
                throw new NotFoundException("Id não encontrado");

            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }









    }
}
