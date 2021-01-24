using System.Collections.Generic;
using BlazorCRUDApp.Shared.Entities.RequestFeatures;

namespace BlazorCRUDApp.Client.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
