using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService : Controller
    {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService()
        {

        }

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }


        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return  result
                .Include(x => x.seller)
                .Include(x => x.seller.department)//Faz o join nas tabelas e lista o resultado pelo comando abaixo
                .OrderByDescending(x => x.Date)
                .ToList();


        }
    }
}
