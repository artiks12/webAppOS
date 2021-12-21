using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test1 : BaseObject
    {
        private void _constructor()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "test1" );
        }

        public test1 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor();
            _object = _wm.FindClassByName( "test1" ).CreateObject();
        }

        public test1 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor();
            _object = new( rObject, wm );
        }

        public test1 ( IWebMemory wm ) : base( wm )
        {
            _constructor();
        }


        private long _int ( )
        {
            string arguments = JsonSerializer.Serialize( new {  } );
            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference , "_int" , arguments );
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

        public string _str ( long a )
        {
            string arguments = JsonSerializer.Serialize( new { a } );
            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference , "_str" , arguments );
            var json = JsonDocument.Parse(result);
            JsonElement errorMessage;
            if (json.RootElement.TryGetAttribute("error", out errorMessage) == true)
            {
                throw new Exception(errorMessage.GetString());
            }
            else
            {
                var r = json.RootElement.GetAttribute("result");
                return r.GetString();
            }
        }
    }
}