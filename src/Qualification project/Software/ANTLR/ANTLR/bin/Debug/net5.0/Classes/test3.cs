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
            List<string> attributes = new() {  };
            checkClass( attributes , "test3" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test3" );
            _object = _wm.FindClassByName( "test3" ).CreateObject();
        }

        public test3 ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "test3" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test3" );
            _object = new( rObject, wm );
        }

    }
}