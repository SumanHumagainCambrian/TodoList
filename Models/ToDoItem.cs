namespace TodoList.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
