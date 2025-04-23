using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChatApp.Server.Utils
{
    internal class HeaderParser
    {
        public static bool TryParseHeader(string header, out Guid id)
        {
            ArgumentNullException.ThrowIfNull(header);

            var parts = header.Split(":", 2);
            if (parts.Length == 2 && Guid.TryParse(parts[1].Trim(), out id))
            {
                return true;
            }
            else
            {
                id = default;
                return false;
            }
        }
    }
}
