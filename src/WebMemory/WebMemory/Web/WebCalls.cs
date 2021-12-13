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
            /*
                string r = _k.enqueueInnerWebCall(wmObjRef,methodName,arguments,"{}"); // Funkcijas izsaukšana caur TDAKernel. Atgriež JSON
                return r;
            */
            
            return "{}";
        }
    }
}