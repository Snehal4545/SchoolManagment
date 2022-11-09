﻿using SchoolManagment.Model;

namespace SchoolManagment.Repository.Interface
{
    public interface ItblSchoolRepository
    {
          public  Task<List<tblSchool>> GetAllSchool();
          public Task<List<tblSchool>> GetAllSchoolById(int id);
          public  Task<int> SaveInformation(tblSchool sch);
         
          public  Task<int> UpdateSchool(UpdateSchool sch);
        public Task<string> UpdateTeacher(tblTeacher tech);
          public Task<int> DeleteSchool(BaseModel.DeleteObj delete);

    }
}
