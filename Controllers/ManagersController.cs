using System;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public class ManagersController : ODataController
    {
        private readonly DbContext _dbContext;

        public ManagersController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery]
        public IQueryable<Manager> Get()
        {
            return _dbContext.Managers;
        }

        [EnableQuery]
        public SingleResult<Manager> Get(Guid key)
        {
            var query = _dbContext.Managers.Where(o => o.Id == key);
            return SingleResult.Create(query);
        }

        public IActionResult Post([FromBody] Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Managers.Add(manager);
            _dbContext.SaveChanges();

            return Created(manager);
        }
    }
}
