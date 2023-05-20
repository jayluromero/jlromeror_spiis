using System;
using AwDal.Production;
using Common.Runtime;
using Entities.Sales;
using Microsoft.Extensions.Configuration;
using AwBll.Interfaces.Sales;

namespace AwBll.Implementations.Sales
{
	public class SalesBll : ISalesBll
	{
        private readonly IConfiguration _configuration;
        private readonly SalesReasonDal dbSales;

        public SalesBll(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString(name: "AwConnectionString");
            dbSales = new SalesReasonDal(connectionString);
        }

        public Task<int> Create(string categoryName)
        {
            return dbSales.Create(categoryName);
        }

        public async Task<List<SalesReason>> GetAll()
        {
            return dbSales.GetAll();
        }

        public async Task<SalesReason> GetById(int categoryId)
        {
            return await dbSales.GetById(categoryId);
        }

        public Task<ExecutionResult> Save(string categoryName)
        {
            return dbSales.Save(categoryName);
        }
    }
}

