using BlazorCRUDApp.Shared.Entities.RequestFeatures;
using BlazorCRUDApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using BlazorCRUDApp.Server.Repository;

namespace BlazorCRUDApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController: ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IPeopleRepository _repo;

        public PeopleController(AppDbContext context, IPeopleRepository repo)
        {
            this.context = context;
            _repo = repo;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Person>>> Get()
        //{
        //    return await context.People.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PersonParameters personParameters)
        {
            var persons = await _repo.GetPeople(personParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(persons.MetaData));
            return Ok(persons);
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            return await context.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Person person)
        {
            context.Add(person);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetPerson", new { id = person.Id }, person);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Person person)
        {
            context.Entry(person).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = new Person { Id = id };
            context.Remove(person);
            await context.SaveChangesAsync();
            ResetAI();
            return NoContent();
        }

        private void ResetAI()
        {
            var entityType = context.Model.FindEntityType(typeof(Person));
            var tableName = entityType.GetTableName();
            var param = new SqlParameter("@tblname", tableName);
            var idlast = context.People.OrderByDescending(a => a.Id).FirstOrDefault();
            int rsid = (context.People.Count() > 0) ? idlast.Id : 0;

            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(@tblname, RESEED, " + rsid + ")", param);
        }
    }
}
