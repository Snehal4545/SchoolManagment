namespace SchoolManagment.Model
{
    public class tblTeacher
    {
        public int Id { get; set; }
        public string? TeacherName { get; set; }
        public string? MobileNum { get; set; }
        public string? EmailId { get; set; }
        public string? TeacherAddress { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Subject { get; set; }
        public bool IsDeleted { get; set; }
        public int SchoolId { get; set; }
    }
}

// [Id],[TeacherName],[MobileNum],[EmailId],
// [TeacherAddress],[JoiningDate],
// [Subject],[IsDeleted],[SchoolId]