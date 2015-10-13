using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public static class ObjectExtensions
    {
        public static string ConvertToString(this object obj)
        {
            //int[]
            int[] intArray = obj as int[];
            if (null != intArray)
            {
                return string.Join(",", intArray);
            }

            //string[]
            string[] strArray = obj as string[];
            if (null != strArray)
            {
                strArray = strArray.Select(item => string.Format("\"{0}\"", item)).ToArray();
                return string.Join(",", strArray);
            }

            //string
            string str = obj as string;
            if (null != str)
            {
                return string.Format("\"{0}\"", str);
            }

            //others
            return obj.ToString();
        }
    }
}
