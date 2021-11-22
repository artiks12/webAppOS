using System;

namespace WebAppOS
{
    public interface IRemoteWebCalls 
    {
        public string WebCall(TDAKernel _k, long wmObjRef, string methodName, string arguments);
    }

    public interface ILocalWebCalls
    {
        public string WebCall(TDAKernel _k, long wmObjRef, string methodURL, string arguments, IRemoteWebCalls wc);
    }

    public class RemoteWebCalls : IRemoteWebCalls
    {
        public string WebCall(TDAKernel _k, long wmObjRef, string methodName, string arguments) 
        {
            Console.WriteLine(wmObjRef + " " + methodName + " " + arguments);

            return "{ \"result\": 5 }";
            //return "{ \"error\": \"not implemented\" }";
        }
    }
}
