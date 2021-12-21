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
            List<string> attributes = new() { "_int" , "Integer" , "_str" , "String" , "_bool" , "Boolean" , "_double" , "Real" };
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


        public long _int 
        {
            get { return Convert.ToInt64( _object["_int"] ); }
            set { _object["_int"] = Convert.ToString( value ); }
        }

        public string _str 
        {
            get { return _object["_str"]; }
            set { _object["_str"] = Convert.ToString( value ); }
        }

        public bool _bool 
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