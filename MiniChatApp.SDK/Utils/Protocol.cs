using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChatApp.SDK.Utils
{
    public class Protocol
    {
        public const string PROTOCOL_VERSION = "1.0";
        public const string PREFIX = "/";
        public const string CREATE_COMMAND = $"{PREFIX}create";
    }
}
