using System.ComponentModel.DataAnnotations;

namespace Projekt_ISS.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public int DeadlineId { get; set; }
        public DateTime Deadline { get; set; }
    }
}
