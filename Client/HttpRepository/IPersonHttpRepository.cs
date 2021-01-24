using BlazorCRUDApp.Client.Features;
using BlazorCRUDApp.Shared.Entities.RequestFeatures;
using BlazorCRUDApp.Shared.Models;
using System.Threading.Tasks;

namespace BlazorCRUDApp.Client.HttpRepository
{
    public interface IPersonHttpRepository
    {
        Task<PagingResponse<Person>> GetPeople(PersonParameters personParameters);
    }
}
