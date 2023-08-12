namespace CRM.Models
{
    public class ToDoItem
    {
        public int? Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string? Text { get; set; }
        public int Progress { get; set; }

        public string? AssignedToUsername { get; set; }
        public Person? AssignedTo { get; set; }
        public string? CreatedByUsername { get; set; }
        public Person? CreatedBy { get; set; }
    }
}
