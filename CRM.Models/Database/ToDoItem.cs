namespace CRM.Models.Database
{
    public class ToDoItem
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Text { get; set; }
        public int Progress { get; set; }
        public Person AssignedTo { get; set; }
        public Person CreatedBy { get; }

        public ToDoItem(DateOnly date, TimeOnly startTime, TimeOnly endTime, string text, int progress, Person assignedTo, Person createdBy)
        {
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Text = text;
            Progress = progress;
            AssignedTo = assignedTo;
            CreatedBy = createdBy;
        }
    }
}
