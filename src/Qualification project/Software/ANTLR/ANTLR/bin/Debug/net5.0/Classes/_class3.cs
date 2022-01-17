using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace sniegs
{
    class _class3 : _class1
    {
        public _class3 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class3();
            _object = _wm.FindClassByName( "_class3" ).CreateObject();
        }

        public _class3 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class3();
            _object = new( rObject, wm );
        }


        public long sum ( long a , long b )
        {
            string arguments = JsonSerializer.Serialize( new { a , b } );
            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference , "sum" , arguments );
            var json = JsonDocument.Parse(result);
            JsonElement errorMessage;
            if (json.RootElement.TryGetAttribute("error", out errorMessage) == true)
            {
                throw new Exception(errorMessage.GetString());
            }
            else
            {
                var r = json.RootElement.GetAttribute("result");
                return r.GetInt64();
            }
        }
    }
}