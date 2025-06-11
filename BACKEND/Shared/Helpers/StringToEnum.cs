using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class StringToEnum
    {
        public static T ParseEnum<T>(string value) where T : struct
        {
            if (Enum.TryParse<T>(value, true, out T result))
            {
                return result;
            }
            return default(T);
        }

    }
}
