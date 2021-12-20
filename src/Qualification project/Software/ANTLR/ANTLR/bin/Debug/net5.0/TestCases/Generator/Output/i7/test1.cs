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
            List<string> attributes = new() { "_int" , "Integer" , "_double" , "Real" };
            checkClass( attributes , "test1" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test1" );
            _object = _wm.FindClassByName( "test1" ).CreateObject();
        }

        public test1 ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() { "_int" , "Integer" , "_double" , "Real" };
            checkClass( attributes , "test1" );
            List<string> associations = new() {  };
            checkAssociations( associations , "test1" );
            _object = new( rObject, wm );
        }


        public long _int 
        {
            get { return Convert.ToInt64( _object["_int"] ); }
            set { _object["_int"] = Convert.ToString( value ); }
        }

        private double _double 
        {
            get { return Convert.ToDouble( _object["_double"] ); }
            set { _object["_double"] = Convert.ToString( value ); }
        }
    }
}