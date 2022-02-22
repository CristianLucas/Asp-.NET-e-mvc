using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SalesWebMvc.Services
{
    public class DepartmentService
    {


        private readonly SalesWebMvcContext _context;

        public DepartmentService()
        {

        }

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList(); //Retorna a lista de departamentos, ordernados por nome
        }

        public async Task<List<Department>> FindAllAsynk()
        {
            //Método assincrono = roda em segundo plano o método otimizando tempo e processamento futuro
            return await _context.Department.OrderBy(X => X.Name).ToListAsync(); //Para que o compilador entenda que esse método será assincrona, deve ser informado a palavra await 
        }




    }
}
