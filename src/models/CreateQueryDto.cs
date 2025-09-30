namespace SmartStudentQueryAPI.Models
{
    // Only the data we need from the client
    public class CreateQueryDto
    {
        public string QueryText { get; set; } = "";
        public int StudentId { get; set; }
    }
}
