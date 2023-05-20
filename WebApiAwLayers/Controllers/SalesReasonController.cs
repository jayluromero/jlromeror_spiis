using AwBll.Interfaces.Sales;
using Common.Runtime;
using Entities.Sales;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiAwLayers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReasonController : ControllerBase
    {
        private readonly ISalesBll _bll;
        public SalesReasonController(ISalesBll categoryBll)
        {
            _bll = categoryBll;
        }
        /// <summary>
        /// Retorna listado de cateogrías
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SalesReason>> Get()
        {
            return  await _bll.GetAll();
        }

        /// <summary>
        /// Obtiene una categoría que tiene como id el valor especificado
        /// </summary>
        /// <param name="id">Identificador de la categoría</param>
        /// <returns><see cref="SalesReason"/></returns>
        [HttpGet("{id}")]
        public async Task<SalesReason> Get(int id)
        {
            return await _bll.GetById(id);
        }

        /// <summary>
        /// Crear una nueva categorìa
        /// </summary>
        /// <param name="salesName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> Post(string salesName)
        {
            return await _bll.Create(salesName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesName"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<ExecutionResult> Save(string salesName)
        {
            return await _bll.Save(salesName);
        }
        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
