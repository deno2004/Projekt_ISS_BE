using System.ComponentModel.DataAnnotations;

namespace Projekt_ISS.Models
{
    public class Deadline
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
