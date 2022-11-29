using SchoolManagment.Model;

namespace SchoolManagment.Repository.Interface
{
    public interface IVehicleInterface
    {
        public Task<int> InsertTrans(List<vehicle> veh);
    }
}
