using Dapper;
using SchoolManagment.Model;
using SchoolManagment.Repository.Interface;
using System;
using System.Data.Common;

namespace SchoolManagment.Repository
{
    public class VehicleRepository: BaseAsyncRepository ,IVehicleInterface
    {
        public VehicleRepository(IConfiguration _con) : base(_con)
        {
        }

        public async Task<int> InsertTrans(List<vehicle> veh)
        {
            int result=0;

            using (DbConnection con = SqlReaderConnection)
            {
                await con.OpenAsync();
                foreach (var vehicle in veh)
                {
                    var res = await con.QuerySingleOrDefaultAsync<vehicle>("Select * from vehicle where Vnum=@Vnum", new { Vnum = vehicle.Vnum });
                    // var res1=res.FirstOrDefault();
                    if (res == null)
                    {
                        result = await con.QueryFirstOrDefaultAsync<int>(" Insert into Vehicle(Vname,VComp,Vnum,Price)" +
                            " values(@Vname,@VComp,@Vnum,@Price) select cast(scope_identity() as int)", vehicle);
                    }
                    else
                    {
                        vehicle.Vhlid = res.Vhlid;
                        /*
                        vehicle.Vnum = res.Vnum;
                        vehicle.VComp = res.VComp;
                        vehicle.Vname = res.Vname;
                        */
                        result = await con.ExecuteAsync("Update Vehicle set  Vname=@Vname ,VComp=@VComp ,Vnum=@Vnum, " +
                            "Price=@Price where Vhlid=@Vhlid", vehicle);

                    }
                }
            }
            return result;
            

        }
    }
}
