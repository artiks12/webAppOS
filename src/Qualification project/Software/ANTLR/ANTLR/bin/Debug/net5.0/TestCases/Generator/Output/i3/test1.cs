using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test1 : BaseObject
    {
        public test1 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "test1" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test1" );
            _object = _wm.FindClassByName( "test1" ).CreateObject();
        }

        public test1 ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "test1" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test1" );
            _object = new( rObject, wm );
        }
    }
}