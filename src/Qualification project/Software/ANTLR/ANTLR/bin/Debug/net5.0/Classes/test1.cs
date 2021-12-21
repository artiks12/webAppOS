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
            List<string> attributes = new() { "_bool" , "Boolean" , "_real" , "Real" };
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



        public bool _bool 
        {
            get { return Convert.ToBoolean( _object["_bool"] ); }
            set { _object["_bool"] = Convert.ToString( value ); }
        }

        public double _real 
        {
            get { return Convert.ToDouble( _object["_real"] ); }
            set { _object["_real"] = Convert.ToString( value ); }
        }
    }
}