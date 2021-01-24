using BlazorCRUDApp.Shared.Entities.RequestFeatures;
using BlazorCRUDApp.Shared.Paging;
using BlazorCRUDApp.Shared.Models;
using System.Threading.Tasks;

namespace BlazorCRUDApp.Server.Repository
{
    public interface IPeopleRepository
    {
        Task<PagedList<Person>> GetPeople(PersonParameters personParameters);
    }
}
