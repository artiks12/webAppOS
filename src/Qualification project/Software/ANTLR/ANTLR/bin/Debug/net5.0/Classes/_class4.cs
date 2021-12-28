using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class _class4 : _class2
    {
        public _class4 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class4();
            _object = _wm.FindClassByName( "_class4" ).CreateObject();
        }

        public _class4 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class4();
            _object = new( rObject, wm );
        }


        public double _real 
        {
            get { return Convert.ToDouble( _object["_real"] ); }
            set { _object["_real"] = Convert.ToString( value ); }
        }
    }
}