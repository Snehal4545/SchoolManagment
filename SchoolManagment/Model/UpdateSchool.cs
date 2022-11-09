namespace SchoolManagment.Model
{
    public class UpdateSchool:BaseModel
    {
        public int Id { get; set; }
        public string? SchoolName { get; set; }
        public string? Grade { get; set; }
        public int NoOfTeacher { get; set; }
        public string? SchoolAddress { get; set; }
        public string? Telephone { get; set; }
        public string? SchoolType { get; set; }
        public DateTime Established { get; set; }
    }
}
