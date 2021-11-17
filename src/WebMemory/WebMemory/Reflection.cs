using System;
using System.Reflection;

namespace WebAppOS
{
    public class Reflection
    {
        public void webcall(string whatToInvoke, WebObject rObject, IWebMemory raapi, string pwd, string project_id, string appFullName, string login)
        {
            Type t = Type.GetType("Namespace.ClassName");

            var mInfo = t.GetMethod("MethodName");

            if (mInfo != null) 
            {
                ParameterInfo[] parameters = mInfo.GetParameters();
                object classInstance = Activator.CreateInstance(t, null);

                if (parameters.Length == 0)
                {
                    var result = mInfo.Invoke(classInstance, null);
                }
                else
                {
                    object[] parametersArray = new object[] { null, rObject, raapi, pwd, project_id, appFullName, login };
                    var result = mInfo.Invoke(classInstance, parametersArray);
                }
            }
        }
    }
}
