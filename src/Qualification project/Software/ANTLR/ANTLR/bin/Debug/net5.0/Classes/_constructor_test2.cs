using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class _constructor_test2 : BaseObject
    {
        public _constructor_test2 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__constructor_test2();
            _object = _wm.FindClassByName( "_constructor_test2" ).CreateObject();
        }

        public _constructor_test2 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__constructor_test2();
            _object = new( rObject, wm );
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