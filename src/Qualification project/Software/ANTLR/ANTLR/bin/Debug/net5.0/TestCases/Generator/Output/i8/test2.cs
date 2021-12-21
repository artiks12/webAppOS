using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test2 : test1
    {
        private void _constructor()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "test2" );
            if(o == false)
            {
               // SuperClass Check
               test1 test1 = new( _wm );
               var c = _wm.FindClassByName( "test2");
               c.CreateGeneralization( "test1");
            }
        }

        public test2 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor();
            _object = _wm.FindClassByName( "test2" ).CreateObject();
        }

        public test2 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor();
            _object = new( rObject, wm );
        }

        public test2 ( IWebMemory wm ) : base( wm )
        {
            _constructor();
        }


        public long _int ( )
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

        private bool _bool ( long a , long b )
        {
            string arguments = JsonSerializer.Serialize( new { a , b } );
            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference , "_bool" , arguments );
            var json = JsonDocument.Parse(result);
            JsonElement errorMessage;
            if (json.RootElement.TryGetAttribute("error", out errorMessage) == true)
            {
                throw new Exception(errorMessage.GetString());
            }
            else
            {
                var r = json.RootElement.GetAttribute("result");
                return r.GetBoolean();
            }
        }

        public double _double ( string a , bool b , double c )
        {
            string arguments = JsonSerializer.Serialize( new { a , b , c } );
            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference , "_double" , arguments );
            var json = JsonDocument.Parse(result);
            JsonElement errorMessage;
            if (json.RootElement.TryGetAttribute("error", out errorMessage) == true)
            {
                throw new Exception(errorMessage.GetString());
            }
            else
            {
                var r = json.RootElement.GetAttribute("result");
                return r.GetDouble();
            }
        }
    }
}