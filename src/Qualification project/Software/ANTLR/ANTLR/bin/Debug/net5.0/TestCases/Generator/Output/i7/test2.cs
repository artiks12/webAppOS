using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test2 : test1
    {
        public test2 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            List<string> attributes = new() { "_str" , "String" , "_bool" , "Boolean" , "_double" , "Real" };
            checkClass( attributes , "test2" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test2" );
            _object = _wm.FindClassByName( "test2" ).CreateObject();
        }

        public test2 ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() { "_str" , "String" , "_bool" , "Boolean" , "_double" , "Real" };
            checkClass( attributes , "test2" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test2" );
            _object = new( rObject, wm );
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