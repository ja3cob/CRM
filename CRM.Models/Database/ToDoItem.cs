namespace CRM.Models.Database
{
    public class ToDoItem
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Text { get; set; }
        public int Progress { get; set; }
        public Person AssignedTo { get; set; }
        public Person CreatedBy { get; }

        public ToDoItem(DateTime startTime, DateTime endTime, string text, int progress, Person assignedTo, Person createdBy)
        {
            StartTime = startTime;
            EndTime = endTime;
            Text = text;
            Progress = progress;
            AssignedTo = assignedTo;
            CreatedBy = createdBy;
        }
    }
}
