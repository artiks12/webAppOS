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
            List<string> attributes = new() {  };
            checkClass( attributes , "test2" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test2" );
            _object = _wm.FindClassByName( "test2" ).CreateObject();
        }

        public test2 ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "test2" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test2" );
            _object = new( rObject, wm );
        }

    }
}