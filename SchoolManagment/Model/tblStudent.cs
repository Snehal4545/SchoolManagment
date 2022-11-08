namespace SchoolManagment.Model
{
    public class tblStudent
    {
        public int Id { get; set; }
        public string? StudName { get; set; }
        public DateTime DOB { get; set; }
        public string? StudAddress { get; set; }
        public string? Gender { get; set; }
        public DateTime AddmissionDate { get; set; }
        public int SportId { get; set; }
        public int ClassId { get; set; }
        public bool IsDeleted { get; set; }

       // public List<tblSports> Students { get; set; } = new List<tblSports>();
    }
}
