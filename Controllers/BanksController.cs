using System;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public class BanksController : ODataController
    {
        private readonly DbContext _dbContext;

        public BanksController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery]
        public IQueryable<Bank> Get()
        {
            return _dbContext.Banks;
        }

        [EnableQuery]
        public SingleResult<Bank> Get(Guid key)
        {
            var query = _dbContext.Banks.Where(o => o.Id == key);
            return SingleResult.Create(query);
        }

        public IActionResult Post([FromBody] Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Banks.Add(bank);
            _dbContext.SaveChanges();

            return Created(bank);
        }
    }
}
