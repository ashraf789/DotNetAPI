using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API
{
    public interface IApiBaseController
    {

    }
    public class ApiBaseController : ApiController, IApiBaseController
    {
        protected const string Version = "v1";
        protected ServiceManager RespositoryService => ServiceManager.RepositoryService;
    }
}