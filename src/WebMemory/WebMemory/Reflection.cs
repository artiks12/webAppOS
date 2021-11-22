using System;
using System.Reflection;

namespace WebAppOS
{
    public class Reflection : ILocalWebCalls
    {
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
            Type t = Type.GetType("Namespace.ClassName");

            var mInfo = t.GetMethod("MethodName");

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