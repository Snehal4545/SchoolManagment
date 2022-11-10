using Dapper;
using SchoolManagment.Model;
using SchoolManagment.Repository.Interface;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace SchoolManagment.Repository
{
    public class tblschoolRepository : BaseAsyncRepository, ItblSchoolRepository
    {
        public tblschoolRepository(IConfiguration _con) : base(_con)
        {

        }

        public async Task<List<tblSchool>> GetAllSchool()
        {
            List<tblSchool> school = new List<tblSchool>();
            var sql = "select * from tblSchool";
            using (DbConnection con = SqlReaderConnection)
            {
                await con.OpenAsync();

                var res = await con.QueryAsync<tblSchool>(sql);
                school = res.ToList();
                foreach (var schools in school)
                {
                    var class1 = await con.QueryAsync<tblClass>("select * from tblclass where schoolid=@Id", new { schools.Id });
                    schools.classlist = class1.ToList();

                    foreach (var classes in class1)
                    {
                        var res1 = await con.QueryAsync<tblStudent>("select * from tblstudent where ClassId=@Id", new { classes.Id });
                        classes.studlist = res1.ToList();
                    }
                    foreach (var teacher in school)
                    {
                        var tech = await con.QueryAsync<tblTeacher>("select * from tblTeacher where schoolid=@Id", new { teacher.Id });
                        teacher.teachlist = tech.ToList();
                    }

                }
            }
            return school;


        }

        public async Task<List<tblSchool>> GetAllSchoolById(int id)
        {
            List<tblSchool> schoolList = new List<tblSchool>();
            var query = "select * from tblschool where id=@id";
            using (DbConnection con = SqlReaderConnection)
            {
                await con.OpenAsync();

                var res = await con.QueryAsync<tblSchool>(query, new { id });
                schoolList = res.ToList();

                foreach (var schlst in schoolList)
                {
                    var res1 = await con.QueryAsync<tblClass>("select * from tblclass where schoolid=@id",
                                                            new { schlst.Id });
                    schlst.classlist = res1.ToList();

                    foreach (var stud in res1)
                    {
                        var res3 = await con.QueryAsync<tblStudent>("select * from tblStudent where classId=@Id",
                                                                    new { stud.Id });
                        stud.studlist = res3.ToList();
                    }

                    foreach (var tchlist in schoolList)
                    {
                        var res2 = await con.QueryAsync<tblTeacher>("select * from tblTeacher where schoolid=@id",
                                                                    new { tchlist.Id });
                        tchlist.teachlist = res2.ToList();
                    }
                }

            }
            return schoolList;


        }
        public async Task<int> SaveInformation(tblSchool sch)
        {
            var query = "insert into tblschool( SchoolName ,Grade , NoOfTeacher,  SchoolAddress , Telephone ,SchoolType, Established, " +
                        "  createdBy ,createdDate,Isdeleted)   values( @SchoolName, @Grade , @NoOfTeacher,  @SchoolAddress ," +
                        " @Telephone ,@SchoolType, @Established, 1 ,@createdDate,0);select cast(scope_identity() as int )";
            using(DbConnection con = SqlReaderConnection)
            {
                await con.OpenAsync();
                sch.createdDate = DateTime.Now;
                sch.NoOfTeacher = sch.teachlist.Count;
                var res1= await con.QueryFirstOrDefaultAsync<int>(query,sch);
                foreach(var teacher in sch.teachlist)
                {
                    await SaveTeacher(teacher,res1);
                  
                }
              
                return res1;

            }
        }
        public async Task SaveTeacher(tblTeacher tech,int id)
        {
            var query = " Insert into tblTeacher(TeacherName,MobileNum,EmailId,TeacherAddress ,JoiningDate,Subject,IsDeleted,SchoolId )" +
                        " values(@TeacherName, @MobileNum , @EmailId, @TeacherAddress , @JoiningDate, @Subject, 0, @SchoolId )";

            using(DbConnection con=SqlReaderConnection)
            {
                tech.SchoolId = id;
                await con.OpenAsync();
                var res1= await con.ExecuteAsync(query,tech);
            }

        }
        public async Task<int> AddTeacher(tblTeacher tech)
        {
            List<tblSchool>sch=new List<tblSchool>();
            var query = " Insert into tblTeacher(TeacherName,MobileNum,EmailId,TeacherAddress,JoiningDate,Subject,IsDeleted,SchoolId) " +
                        " values(@TeacherName,@MobileNum,@EmailId,@TeacherAddress,@JoiningDate,@Subject,0,@SchoolId);" +
                        "select cast(scope_identity() as int) ";
            using(DbConnection con= SqlReaderConnection)
            {
                await con.OpenAsync();
                var rtn = await con.QueryFirstOrDefaultAsync<int>(query, tech);
                var qry1 = "select noOfTeacher from tblSchool where Id=@id";
                var res1=await con.QuerySingleAsync<tblSchool>(qry1, new {id=tech.SchoolId});
                int techcount = res1.NoOfTeacher;
                techcount++;
                var qry = "Update tblSchool set NoOfTeacher=@techcount where Id=@id";

                var res2 = await con.ExecuteAsync(qry, new {techcount,id=tech.SchoolId});

               
                return rtn;
            }

        }
        public async Task<int> UpdateSchool(UpdateSchool sch)
        {
            int res1;
            var query = "update tblSchool set SchoolName=@SchoolName ,Grade=@Grade , NoOfTeacher=@NoOfTeacher,  SchoolAddress=@SchoolAddress " +
                ", Telephone=@Telephone ,SchoolType=@SchoolType, Established=@Established, modifiedBy=1,modifiedDate=@modifiedDate" +
                ", Isdeleted=0 where Id=@Id ";
            using(DbConnection con= SqlReaderConnection)
            {
                await con.OpenAsync();
                sch.modifiedDate= DateTime.Now;
                
                res1=await con.ExecuteAsync(query,sch);
               
               
            }
            return res1;

        }
        public async Task<int> UpdateTeacher(tblTeacher tech)
        {
            int rtn1;
           
            var query = " Update tblTeacher set TeacherName=@TeacherName, MobileNum=@MobileNum, EmailId=@EmailId, " +
                        " TeacherAddress=@TeacherAddress ,JoiningDate=@JoiningDate, Subject=@Subject,SchoolId=@SchoolId,Isdeleted=0" +
                        "where id=@id ";
            using(DbConnection con=SqlReaderConnection)
            {
                await con.OpenAsync();
                rtn1 = await con.ExecuteAsync(query, tech);
                
            }
            return rtn1;
        }
        public async Task<int> DeleteSchool(BaseModel.DeleteObj delete)
        {
            int result=0;
            if(delete.Id!=0)
            {
                using(DbConnection con=SqlReaderConnection)
                {
                    await con.OpenAsync();
                    result= await con.ExecuteAsync("Update tblschool set Isdeleted='True' where id=@id", new {id=delete.Id});
                }
            }
            return result;
           


        }
    }
}


