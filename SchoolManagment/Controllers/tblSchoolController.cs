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

        public tblSchoolController(ILoggerFactory loggerfactory , ItblSchoolRepository tblschoolRepository, IConfiguration configuration)
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
                var result= await tblschoolRepository.GetAllSchool();

                if(result.Count==0)
                {
                    var rtnmsg = string.Format("No data Found");
                    _logger.LogInformation(rtnmsg);
                    responseStatus.StatusCode=StatusCodes.Status404NotFound.ToString();
                    responseStatus.StatusMessage=rtnmsg;
                   
                    return Ok(responseStatus);

                }
                var rtnmnsg = string.Format("All Data fetched Successfully");
                _logger.LogDebug("GetAllSchool: Completed get action ");
                responseStatus.StatusCode=StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage = rtnmnsg;
                responseStatus.ResponseStatus = result;
                return Ok(responseStatus);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                var rtnmsg=string.Format(ex.Message);
                _logger.LogInformation(rtnmsg);
                responseStatus.StatusCode =StatusCodes.Status409Conflict.ToString();
                responseStatus.StatusMessage= rtnmsg;
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
        public async Task<IActionResult>SaveInformation(tblSchool sch)
        {
            BaseResponseStatus responseStatus=new BaseResponseStatus();
            _logger.LogDebug(string.Format("tblSchoolController-SaveInformation Calling By save Information  method"));
            if(sch != null)
            {
                var execution = await tblschoolRepository.SaveInformation(sch);
                if(execution >= 1)
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
        public async Task<IActionResult> UpdateSchool(tblSchool sch)
        {
            BaseResponseStatus responseStatus = new BaseResponseStatus();
            _logger.LogDebug(string.Format("tblSchoolController-SaveInformation Calling By save Information  method"));
            if(sch!=null)
            {
              var execution= await tblschoolRepository.UpdateSchool(sch);
                if(execution>=1)
                {
                    var rtnmsg = string.Format("Record Updated Successfully");
                    _logger.LogDebug(rtnmsg);
                    responseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
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
                responseStatus.StatusCode= StatusCodes.Status200OK.ToString();
                responseStatus.StatusMessage= rtnmsg;
                return Ok(responseStatus);
            }


        }

       
    }
}
