using System.ComponentModel.DataAnnotations;

namespace Projekt_ISS.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
