using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test2 : BaseObject
    {
        private void _constructor()
        {
            List<string> attributes = new() { "_int1" , "Integer" , "_int2" , "Integer" };
            var o = checkClass( attributes , "test2" );
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



        public long _int1 
        {
            get { return Convert.ToInt64( _object["_int1"] ); }
            set { _object["_int1"] = Convert.ToString( value ); }
        }

        public long _int2 
        {
            get { return Convert.ToInt64( _object["_int2"] ); }
            set { _object["_int2"] = Convert.ToString( value ); }
        }
    }
}