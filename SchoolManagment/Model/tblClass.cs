namespace SchoolManagment.Model
{
    public class tblClass
    {
        public int Id { get; set; }
        public string? ClsStandered { get; set; }
        
        public double Fees { get; set; }
        public int SchoolId { get; set; }
        public int TeacherId { get; set; }
        public bool Isdeleted { get; set; }

        public List<tblStudent> studlist { get; set; } = new List<tblStudent>();
    }
}
        /*
         * [Id]
         * [Std]
         *[Fees] 
         *[SchoolId]
         *[TeacherId]
         *[Createddate]
         *[Isdeleted]
         */