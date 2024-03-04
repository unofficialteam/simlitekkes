using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simlitekkes.Core
{
    public class kontrolUnggah
    {
        #region Class Variables

        public string path2save = "";
        public int max_size = 1000;
        public string alllowed_ext = "";
        public string success_info = "";
        public string failed_info = "";
        public bool isReloadParentAfterSuccess = false;
        public bool isSuccess = false;

        #endregion
    }
}