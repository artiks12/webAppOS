// Reflection.cs
/******************************************************
* Satur interfeisa ILocalWebCalls funkciju definīcijas.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using System; // nodrošina ievad-izvadierīču lietošanu
using System.Reflection; // Nodrošina darbu ar tipu refleksiju
using System.Collections.Generic; // Nodrošina darbu ar iebūvētajām datu struktūrām
using System.Linq; // Nodrošina LINQ funkcijas


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