using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrizesService.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Models
{

    public class Response
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
    }

    public class ReturnResponse
    {
        public static dynamic ExceptionResponse(Exception ex)
        {
            Response response = new Response();
            response.status = false;
            if (ex.Data.Keys.Count > 0)
            {
                var statusMessage = ex.Data.Keys.Cast<string>().Single();
                var statusCode = ex.Data[statusMessage].ToString();
                response.message = statusMessage;
                response.statusCode = Convert.ToInt32(statusCode);
            }
            else
            {
                response.message = CommonMessage.ExceptionMessage + ex.Message;
                response.statusCode = StatusCodes.Status500InternalServerError;
            }
            return response;
        }

        public static dynamic SuccessResponse(string message, bool isCreated)
        {
            Response response = new Response();
            response.status = true;
            response.message = message;
            if (isCreated)
                response.statusCode = StatusCodes.Status201Created;
            else
                response.statusCode = StatusCodes.Status200OK;
            return response;
        }

        public static dynamic ErrorResponse(string message, int statusCode)
        {
            Response response = new Response();
            response.status = false;
            response.message = message;
            response.statusCode = statusCode;
            return response;
        }
    }

    public class CandidateResponse : Response
    {
        public Pagination pagination { get; set; }
        public List<CandidatesGetModel> data { get; set; }
    }

    public class NationalitiesResponse : Response
    {
        public Pagination pagination { get; set; }
        public List<NationalitiesModel> data { get; set; }
    }

    public class DrawsResponse : Response
    {
        public Pagination pagination { get; set; }
        public List<DrawsModel> data { get; set; }
    }
}
