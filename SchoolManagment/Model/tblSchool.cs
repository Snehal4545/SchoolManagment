namespace SchoolManagment.Model
{
    public class tblSchool : BaseModel
    {
        public int Id { get; set; }
        public string? SchoolName { get; set; }
        public string? Grade { get; set; }
        public int NoOfTeacher { get; set; }
        public string? SchoolAddress { get; set; }
        public string? Telephone { get; set; }
        public string? SchoolType { get; set; }
        public DateTime Established { get; set; }
        //public bool Isdeleted { get; set; }



        public List<tblClass> classlist { get; set; } = new List<tblClass>();
        public List<tblTeacher> teachlist { get; set; } = new List<tblTeacher>();
    }
   
}
