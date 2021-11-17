using System;

namespace WebAppOS
{
    public interface IWebCalls 
    {
        public string WebCall(IWebMemory wm, long wmObjRef, string methodName, string arguments);
    }

    public class WebCalls : IWebCalls
    {
        public string WebCall(IWebMemory wm, long wmObjRef, string methodName, string arguments) 
        {
            Console.WriteLine(wmObjRef + " " + methodName + " " + arguments);
            return "{ \"error\": \"not implemented\" }";
        }
    }
}
