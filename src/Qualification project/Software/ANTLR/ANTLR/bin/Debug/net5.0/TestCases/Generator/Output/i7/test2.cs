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
            List<string> attributes = new() { "_str" , "String" , "_bool" , "Boolean" , "_double" , "Real" };
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


        public string _str 
        {
            get { return _object["_str"]; }
            set { _object["_str"] = Convert.ToString( value ); }
        }

        private bool _bool 
        {
            get { return Convert.ToBoolean( _object["_bool"] ); }
            set { _object["_bool"] = Convert.ToString( value ); }
        }

        public double _double 
        {
            get { return Convert.ToDouble( _object["_double"] ); }
            set { _object["_double"] = Convert.ToString( value ); }
        }
    }
}