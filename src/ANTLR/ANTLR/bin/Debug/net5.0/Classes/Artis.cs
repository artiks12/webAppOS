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
            _object = _wm.FindClassByName( Artis ).CreateObject();
            List<string> attributes = new() {  };
            checkClass( attributes , "Artis" );
        }

        public Artis ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            _object = new( rObject, wm );
            List<string> attributes = new() {  };
            checkClass( attributes , "Artis" );
        }


        public List<Raivis> target1
        {
            get
            {
                var a = checkAssociation( "source1" , "target1" , "Artis" , "Raivis" , false);
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
                var a = checkAssociation( "source1" , "target1" , "Artis" , "Raivis" , false);
                var list = value;
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
            }
        }
    }
}