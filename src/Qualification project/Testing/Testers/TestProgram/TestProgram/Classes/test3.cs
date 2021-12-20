using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test3 : test2
    {
        public test3 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            List<string> attributes = new() { "_str" , "String" };
            List<string> associations = new() {  };
            checkClass( attributes , associations , "test3" );
            test2 test2 = new();
            _object = _wm.FindClassByName( "test3" ).CreateObject();
        }

        public test3 ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() { "_str" , "String" };
            List<string> associations = new() {  };
            checkClass( attributes , associations , "test3" );
            _object = new( rObject, wm );
            test2 test2 = new();
        }

        public test3 () : base()
        {
            List<string> attributes = new() { "_str" , "String" };
            List<string> associations = new() {  };
            checkClass( attributes , associations , "test3" );
            test2 test2 = new();
        }


        public string _str 
        {
            get { return _object["_str"]; }
            set { _object["_str"] = Convert.ToString( value ); }
        }
    }
}