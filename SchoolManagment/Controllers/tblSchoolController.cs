using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagment.Model;
using SchoolManagment.Repository.Interface;
using System;
using System.Reflection.Metadata.Ecma335;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tblSchoolController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ItblSchoolRepository tblschoolRepository;
        IConfiguration configuration;

        public tblSchoolController(ILoggerFactory loggerfactory, ItblSchoolRepository tblschoolRepository, IConfiguration configuration)
        {
            this._logger = loggerfactory.CreateLogger<tblSchoolController>();
            this.tblschoolRepository = tblschoolRepository;
            this.configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSchool()
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();
            try
            {
                _logger.LogDebug(string.Format($"tblScholRepository-GetAllSchool: Calling GetAllSchool "));
                var result = await tblschoolRepository.GetAllSchool();

                if (result.Count == 0)
                {
                    var rtnmsg = string.Format("No data Found");
                    _logger.LogInformation(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseStatus.StatusMessage = rtnmsg;

                    return Ok(responseStatus);

                }
                var rtnmnsg = string.Format("All Data fetched Successfully");
                _logger.LogDebug("GetAllSchool: Completed get action ");
                responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage = rtnmnsg;
                responseStatus.ResponseStatus = result;
                return Ok(responseStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                _logger.LogInformation(rtnmsg);
                responseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseStatus.StatusMessage = rtnmsg;
                return Ok(responseStatus);
            }
            return Ok(responseStatus);

        }
        [HttpGet("id")]
        public async Task<IActionResult> GetAllSchoolById(int id)
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();

            _logger.LogDebug(string.Format($"tblSchoolController-GetListById :Calling  action GetListById with Id {id}."));

            if (id == null)
            {
                var returnmsg = string.Format("Please enter valid Id");
                _logger.LogDebug(returnmsg);
                responseStatus.StatusCode = StatusCodes.Status400BadRequest.ToString();
                responseStatus.StatusMessage = returnmsg;
                return Ok(responseStatus);
            }
            var schList = await tblschoolRepository.GetAllSchoolById(id);
            if (schList == null)
            {
                var retunmsg = string.Format($"Requested Id {id} is not available.");
                _logger.LogDebug(retunmsg);
                responseStatus.StatusCode = StatusCodes.Status404NotFound.ToString();
                responseStatus.StatusMessage = retunmsg;
                return Ok(responseStatus);
            }
            var rtnmsg = string.Format($"Completed get action with Id {id}.");
            _logger.LogDebug(rtnmsg);
            responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
            responseStatus.StatusMessage = rtnmsg;
            responseStatus.ResponseStatus = schList;
            return Ok(responseStatus);


        }
        [HttpPut]
        public async Task<IActionResult> SaveInformation(tblSchool sch)
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();
            _logger.LogDebug(string.Format("tblSchoolController-SaveInformation Calling By save Information  method"));
            if (sch != null)
            {
                var execution = await tblschoolRepository.SaveInformation(sch);
                if (execution >= 1)
                {
                    var rtnmsg = string.Format("Record added successfully..");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format("tblSchoolController-SaveInformation : Completed "));
                    responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);

                }
                else
                {
                    var rtnmsg = string.Format("Error while updating");
                    _logger.LogDebug(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);
                }
            }
            else
            {
                var rtnmsg = string.Format("Record added successfully..");
                _logger.LogDebug(rtnmsg);
                responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage = rtnmsg;
                return Ok(responseStatus);
            }
        }
        [HttpPut("AddTeacher")]
        public async Task<IActionResult> AddTeacher(tblTeacher tech)
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();
            _logger.LogDebug(string.Format("tblSchoolController-SaveInformation Calling By save Information  method"));
            if (tech != null)
            {
                var execution = await tblschoolRepository.AddTeacher(tech);
                if (execution >= 1)
                {
                    var rtnmsg = string.Format("Record added successfully..");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format("tblSchoolController-SaveInformation : Completed "));
                    responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);

                }
                else
                {
                    var rtnmsg = string.Format("Error while updating");
                    _logger.LogDebug(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);
                }
            }
            else
            {
                var rtnmsg = string.Format("Record added successfully..");
                _logger.LogDebug(rtnmsg);
                responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage = rtnmsg;
                return Ok(responseStatus);
            }
        }
        [HttpPut("AddClass")]
        public async Task<IActionResult> AddClass(tblClass cls)
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();
            _logger.LogDebug(string.Format("tblSchoolController-SaveInformation Calling By save Information  method"));
            if (cls != null)
            {
                var execution = await tblschoolRepository.AddClass(cls);
                if (execution >= 1)
                {
                    var rtnmsg = string.Format("Record added successfully..");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format("tblSchoolController-SaveInformation : Completed "));
                    responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);

                }
                else
                {
                    var rtnmsg = string.Format("Error while updating");
                    _logger.LogDebug(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);
                }
            }
            else
            {
                var rtnmsg = string.Format("Record added successfully..");
                _logger.LogDebug(rtnmsg);
                responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage = rtnmsg;
                return Ok(responseStatus);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSchool(UpdateSchool sch)
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();
            _logger.LogDebug(string.Format("tblSchoolController-updateSchool Calling By UpdateSchool method"));
            if (sch != null)
            {
                var execution = await tblschoolRepository.UpdateSchool(sch);
                if (execution >= 1)
                {
                    var rtnmsg = string.Format("Record Updated Successfully");
                    _logger.LogDebug(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    responseStatus.ResponseStatus = execution;
                    return Ok(responseStatus);
                }
                else
                {
                    var rtnmsg = string.Format("Error while updating");
                    _logger.LogDebug(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseStatus.StatusMessage = rtnmsg;
                    return Ok(responseStatus);
                }
            }
            else
            {
                var rtnmsg = string.Format("Record Updated Successfully");
                _logger.LogDebug(rtnmsg);
                responseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage = rtnmsg;
                return Ok(responseStatus);
            }


        }
        [HttpPost("UpdateTeacherInformation")]
        public async Task<IActionResult>UpdateTeacher(tblTeacher tech)
        {
            BaseResponseStatus baseResponse=new BaseResponseStatus();
            _logger.LogDebug(string.Format("tblschoolrepository-UpdateTeacher :Calling Update teacher method"));
            if(tech!=null)
            {
                var execution =await  tblschoolRepository.UpdateTeacher(tech);
                if(execution >= 1)
                {
                    var rtnmsg = string.Format("record Updated successfully");
                    _logger.LogDebug(rtnmsg);
                    baseResponse.StatusCode=StatusCodes.Status200OK.ToString(); 
                    baseResponse.StatusMessage = rtnmsg;
                    return Ok(baseResponse);

                }
                else
                {
                    var rtnmsg = string.Format("Error While Updating");
                    _logger.LogDebug(rtnmsg);
                    baseResponse.StatusCode=StatusCodes.Status409Conflict.ToString();
                    baseResponse.StatusMessage= rtnmsg;
                    return Ok(baseResponse);
                }
            }
            else
            {
                var rtnmsg = string.Format("reocord Updated Successfully");
                _logger.LogDebug(rtnmsg);
                baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponse.StatusMessage = rtnmsg;
                return Ok(baseResponse);
            }


        }
        
        [HttpDelete]
        public async Task<IActionResult>DeleteSchool(BaseModel.DeleteObj delete)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            _logger.LogDebug(string.Format($"tblSchoolController-DeleteSchool: Calling delete action with{delete.Id}"));

            var execution = await tblschoolRepository.DeleteSchool(delete);
            if(execution==0)
            {
                var rtnmsg = string.Format($"Record with Id {delete.Id} does not exist");
                _logger.LogDebug(rtnmsg);
                baseResponseStatus.StatusCode=StatusCodes.Status404NotFound.ToString();
                baseResponseStatus.StatusMessage=rtnmsg;
                return Ok(baseResponseStatus);
            }
            else
            {
                var rtnmsg = string.Format($"Record with Id{delete.Id} Deleted successfully");
                _logger.LogDebug(rtnmsg);
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = rtnmsg;
                baseResponseStatus.ResponseStatus = execution;
                return Ok(baseResponseStatus);
            }

           // return Ok(baseResponseStatus);
        }

       
    }
}
