using System.ComponentModel.DataAnnotations;

namespace BlazorCRUDApp.Shared.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
