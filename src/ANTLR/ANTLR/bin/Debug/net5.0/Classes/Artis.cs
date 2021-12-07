using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class Artis : BaseObject
    {

        public Artis ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "Artis" );
            List<string> associations = new() { "source1" , "target1" , "Raivis" , "false" , "Artis" };
            checkAssociations( associations , "Artis" );
            _object = _wm.FindClassByName( "Artis" ).CreateObject();
        }

        public Artis ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "Artis" );
            List<string> associations = new() { "source1" , "target1" , "Raivis" , "false" , "Artis" };
            checkAssociations( associations , "Artis" );
            _object = new( rObject, wm );
        }


        public List<Raivis> target1
        {
            get
            {
                var c = _wm.FindClassByName( "Artis" );
                var a = c.FindAssociationEnd( "Raivis" );
                var list = _object.LinkedObjects(a);
                List<Raivis> result = new();
                foreach (var l in list)
                {
                    result.Add( new Raivis( _wm , _wc , l.GetReference() ));
                }
                return result;
            }
            set
            {
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
            }
        }
    }
}