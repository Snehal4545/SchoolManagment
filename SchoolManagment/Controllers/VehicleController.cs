using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Model;
using SchoolManagment.Repository;
using SchoolManagment.Repository.Interface;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IVehicleInterface VehicleInterface;
        IConfiguration configuration;

        public VehicleController(ILoggerFactory loggerfactory, IVehicleInterface VehicleInterface, IConfiguration configuration)
        {
            this._logger = loggerfactory.CreateLogger<tblSchoolController>();
            this.VehicleInterface = VehicleInterface;
            this.configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> InsertTrans(List<vehicle> veh)
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();
            _logger.LogDebug(string.Format("VehicleController-InsertTrans Calling By Insert transaction Method"));
            if (veh != null)
            {
                var execution = await VehicleInterface.InsertTrans(veh);
                if (execution >= 1)
                {
                    var rtnmsg = string.Format("Record added successfully..");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format("VehicleController-InsertTrans : Completed "));
                    responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);

                }
                else 
                {
                    var rtnmsg = string.Format("Error while Adding");
                    _logger.LogDebug(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);
                }
            }
            else
            {
                var rtnmsg = string.Format("Record Added successfully..");
                _logger.LogDebug(rtnmsg);
                responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage = rtnmsg;
                return Ok(responseStatus);
            }

        }
        

       
    }
}
