using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public static class ErrorConstants
    {
        /**/
        public const int Success = 0;
        public const int Unknown = Success - 1;
        public const int LoginExpried = Unknown - 1;

        /*System Error*/

        /*Authentication*/
        public const int SystemInsufficientPermission = -1000;

        /*Property*/
        public const int InvalidPropertyValue = -2000;

        #region authentication 
        public const int UNKNOWN_ERROR = -5000;
        public const int DB_INSERTATION_FAILED = UNKNOWN_ERROR - 1;
        public const int DB_UPDATE_FAILED = DB_INSERTATION_FAILED - 1;
        public const int DB_DELETE_FAILED = DB_UPDATE_FAILED - 1;
        public const int DB_NOT_FOUND_ERROR = DB_DELETE_FAILED - 1;
        public const int USER_ALREADY_EXIST= DB_NOT_FOUND_ERROR - 1;
        #endregion

        #region Error description
        public static string ErrorDescription(int id)
        {
            switch (id)
            {
                #region authentication
                case DB_INSERTATION_FAILED: return "Failed to create new record";
                case USER_ALREADY_EXIST: return "User already exist";
                default: return "UNKNOWN Error";
                #endregion
            }
        }
        #endregion
    }
}