using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class Raivis : BaseObject
    {

        public Raivis ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "Raivis" );
            List<string> associations = new() { "target1" , "source1" , "Raivis" , "Artis" , "false" };
            checkAssociation( associations );
            _object = _wm.FindClassByName( "Raivis" ).CreateObject();
        }

        public Raivis ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "Raivis" );
            List<string> associations = new() { "target1" , "source1" , "Raivis" , "Artis" , "false" };
            checkAssociation( associations );
            _object = new( rObject, wm );
        }


        public List<Artis> source1
        {
            get
            {
                var c = _wm.FindClassByName( "Raivis" );
                var a = c.FindAssociationEnd( "Artis" );
                var list = _object.LinkedObjects(a);
                List<Artis> result = new();
                foreach (var l in list)
                {
                    result.Add( new Artis( _wm , _wc , l.GetReference() ));
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