using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test3 : BaseObject
    {
        private void _constructor()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "test3" );
        }

        public test3 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor();
            _object = _wm.FindClassByName( "test3" ).CreateObject();
        }

        public test3 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor();
            _object = new( rObject, wm );
        }

        public test3 ( IWebMemory wm ) : base( wm )
        {
            _constructor();
        }
    }
}