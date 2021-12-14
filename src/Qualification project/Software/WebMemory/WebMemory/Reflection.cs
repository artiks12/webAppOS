using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;


namespace WebAppOS
{
    public class Reflection : ILocalWebCalls
    {
        public List<string> GetMethodInfo(string methodURL) 
        {
            string[]seperator = { ":", "#" };
            List<string> list = new(methodURL.Split(seperator, StringSplitOptions.RemoveEmptyEntries));
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_k"></param>
        /// <param name="wmObjRef"></param>
        /// <param name="methodURL">ir formātā "dotnet:local:namespace.classname#methodname"</param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public string WebCall(TDAKernel _k, long wmObjRef, string methodURL, string arguments, IRemoteWebCalls wc)
        {
            var l = GetMethodInfo(methodURL);
            
            Type t = Type.GetType(l[2]);

            var mInfo = t.GetMethod(l[3]);

            if (mInfo != null) 
            {
                object classInstance = Activator.CreateInstance(t, _k, wmObjRef, wc);

                string result = (string)mInfo.Invoke(classInstance, new[]{ arguments });

                return result;
            }
            return "{ \"error\": \"method not found\" }";
        }
    }
}