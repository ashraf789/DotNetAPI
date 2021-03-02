using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public static class ConfigHelpers
    {
        #region DB status code
        public const int STATUS_NEW = 0;
        public const int STATUS_AVAILABLE = 1;
        public const int STATUS_DISABLE = 2;
        public const int STATUS_DELETED = 3;
        #endregion
    }
}