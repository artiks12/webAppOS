using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test2 : BaseObject
    {
        public test2 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor_test2();
            _object = _wm.FindClassByName( "test2" ).CreateObject();
        }

        public test2 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor_test2();
            _object = new( rObject, wm );
        }
    }
}