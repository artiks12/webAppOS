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
            var o = checkClass( attributes , "test3" );
            if(o == false)
            {
               // SuperClass Check
               test2 test2 = new( _wm );
               var c = _wm.FindClassByName( "test3");
               c.CreateGeneralization( "test2");
            }
            _object = _wm.FindClassByName( "test3" ).CreateObject();
        }

        public test3 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            List<string> attributes = new() { "_str" , "String" };
            var o = checkClass( attributes , "test3" );
            if(o == false)
            {
               // SuperClass Check
               test2 test2 = new( _wm );
               var c = _wm.FindClassByName( "test3");
               c.CreateGeneralization( "test2");
            }
            _object = new( rObject, wm );
        }

        public test3 ( IWebMemory wm ) : base( wm )
        {
            List<string> attributes = new() { "_str" , "String" };
            var o = checkClass( attributes , "test3" );
            if(o == false)
            {
               // SuperClass Check
               test2 test2 = new( _wm );
               var c = _wm.FindClassByName( "test3");
               c.CreateGeneralization( "test2");
            }
        }



        public string _str 
        {
            get { return _object["_str"]; }
            set { _object["_str"] = Convert.ToString( value ); }
        }
    }
}