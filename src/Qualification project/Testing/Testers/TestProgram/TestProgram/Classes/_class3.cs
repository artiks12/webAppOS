using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class _class3 : _class1
    {
        public _class3 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class3();
            _object = _wm.FindClassByName( "_class3" ).CreateObject();
        }

        public _class3 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class3();
            _object = new( rObject, wm );
        }
    }
}