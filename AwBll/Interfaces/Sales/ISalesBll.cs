using Common.Runtime;
using Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwBll.Interfaces.Sales
{
    public interface ISalesBll
    {
        Task<List<SalesReason>> GetAll();
        Task<int> Create(string salesName);
        Task<SalesReason> GetById(int salesId);
        Task<ExecutionResult> Save(string salesName);
    }
}
