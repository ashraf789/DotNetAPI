using API.Models;
using Common;
using Core.Exceptions;
using Core.MessageResponse;
using Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.v1.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiBaseController
    {
        #region authentication 

        /// <summary>
        /// User login
        /// </summary>
        /// <remarks>
        /// User login api
        /// </remarks>
        /// <param name="apiRequest">apiRequest</param>
        /// <returns>success or not</returns>
        [Route("login"), HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> Login([FromBody] UserRequest apiRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(apiRequest.Email)) 
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "Email is required", null));
                }
                
                if (string.IsNullOrEmpty(apiRequest.Password)) 
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "Password is required", null));
                }

                var result = (await RespositoryService.UserService.GetUser(apiRequest)).ResponseObject;

                if (result == null)
                {
                    return Ok(new ResultDetail(ErrorConstants.Unknown, "User not found", Version, result));
                }

                if (!result.Password.ToUpper().Equals(ConfigHelpers.EncryptStringMD5(apiRequest.Password).ToUpper()))
                {
                    return Ok(new ResultDetail(ErrorConstants.WRONG_USER_AUTHENTICATION, ErrorConstants.ErrorDescription(ErrorConstants.WRONG_USER_AUTHENTICATION), Version, null));
                }

                JwtManager jwtManager = new JwtManager();
                TokenRequest tokenRequest = new TokenRequest();
                tokenRequest.UserID = result.ID;
                tokenRequest.Name = result.Name;
                tokenRequest.Email = result.Email;
                tokenRequest.RoleID = result.RoleID;

                var token = jwtManager.GenerateSecurityToken(tokenRequest);
                token.UserDetail = result;

                var response = new ResultDetail(ErrorConstants.Success, "Success", Version, token);
                return Ok(response);

            }catch (Exception ex)
            {
                throw new AppHttpException(HttpStatusCode.OK
                    , new ResultDetail(ErrorConstants.Unknown, ex.Message, Version));
            }
        }

        /// <summary>
        /// User signup
        /// </summary>
        /// <remarks>
        /// User signup api
        /// </remarks>
        /// <param name="apiRequest">apiRequest</param>
        /// <returns>success or not</returns>
        [Route("signup"), HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> SignUp([FromBody] CreateUserRequest apiRequest)
        {
            try
            {
                if(apiRequest == null)
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "Empty required fields", null));
                }

                if (string.IsNullOrEmpty(apiRequest.Name)) 
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "Name is required", null));
                }
                
                if (string.IsNullOrEmpty(apiRequest.Email)) 
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "Email is required", null));
                }
                
                if (string.IsNullOrEmpty(apiRequest.Password)) 
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "Password is required", null));
                }
                
                if (apiRequest.RoleID == 0) 
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "RoleId is required", null));
                }


                var result = (await RespositoryService.UserService.CreateUser(apiRequest)).ResponseObject;
                if(result < 0)
                {
                    return Ok(new ResultDetail(result, ErrorConstants.ErrorDescription(result), Version, result));
                }

                var response = new ResultDetail(ErrorConstants.Success, "Success", Version, result);
                return Ok(response);

            }catch (Exception ex)
            {
                throw new AppHttpException(HttpStatusCode.OK
                    , new ResultDetail(ErrorConstants.Unknown, ex.Message, Version));
            }
        }
        #endregion

        #region user information
        /// <summary>
        /// User Details
        /// </summary>
        /// <remarks>
        /// User Information
        /// </remarks>
        /// <param name="apiRequest">apiRequest</param>
        /// <returns>success or not</returns>
        [Route("information"), HttpPost, Authorize]
        public async Task<IHttpActionResult> Information([FromBody] UserInformationRequest apiRequest)
        {
            try
            {
                if (apiRequest == null)
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "Empty required fields", null));
                }

                if (string.IsNullOrEmpty(apiRequest.UserID))
                {
                    return Ok(new ResultDetail(ErrorConstants.InvalidPropertyValue, "UserID is required", null));
                }

                var result = (await RespositoryService.UserService.GetUserInfo(apiRequest)).ResponseObject;
                if (result == null)
                {
                    return Ok(new ResultDetail(ErrorConstants.Unknown, "User not found", Version, result));
                }

                var response = new ResultDetail(ErrorConstants.Success, "Success", Version, result);
                return Ok(response);

            }
            catch (Exception ex)
            {
                throw new AppHttpException(HttpStatusCode.OK
                    , new ResultDetail(ErrorConstants.Unknown, ex.Message, Version));
            }
        }
        #endregion
    }
}