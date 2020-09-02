using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanusTest.Janus.Data
{
    public static class ResponseHelper
    {
        private const string JANUS_SUCCESS_TEXT = "success";
        public static bool IsSucceded(this JanusBaseObject janusObject)
        {
            return string.Equals(janusObject.Janus, JANUS_SUCCESS_TEXT, StringComparison.OrdinalIgnoreCase);
        }
    }
}
