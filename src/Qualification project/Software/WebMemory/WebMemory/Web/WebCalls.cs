// Reflection.cs
/******************************************************
* Satur WebCalls interfeisu aprakstus un 
* interfeisa IRemoteWebCalls funkcijas definīciju.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

namespace WebAppOS
{
    public interface IRemoteWebCalls 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_k"></param>
        /// <param name="wmObjRef">Objeckta atsauce</param>
        /// <param name="methodName">metodes vārds</param>
        /// <param name="arguments">metodes argumenti (JSON)</param>
        /// <returns></returns>
        public string WebCall(TDAKernel _k, long wmObjRef, string methodName, string arguments);
    }

    public interface ILocalWebCalls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_k"></param>
        /// <param name="wmObjRef">Objeckta atsauce</param>
        /// <param name="methodURL">metodes URL</param>
        /// <param name="arguments">metodes argumenti (JSON)</param>
        /// <param name="wc"></param>
        /// <returns></returns>
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