using BlazorCRUDApp.Shared.Entities.RequestFeatures;
using BlazorCRUDApp.Shared.Paging;
using BlazorCRUDApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlazorCRUDApp.Server.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AppDbContext _context;
        public PeopleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<Person>> GetPeople(PersonParameters personParameters)
        {
            var persons = await _context.People.ToListAsync();
            return PagedList<Person>
                .ToPagedList(persons, personParameters.PageNumber, personParameters.PageSize);
        }
    }
}
