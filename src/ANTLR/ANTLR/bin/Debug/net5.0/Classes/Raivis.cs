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
            _object = _wm.FindClassByName( Raivis ).CreateObject();
            List<string> attributes = new() {  };
            checkClass( attributes , "Raivis" );
        }

        public Raivis ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            _object = new( rObject, wm );
            List<string> attributes = new() {  };
            checkClass( attributes , "Raivis" );
        }


        public List<Artis> source1
        {
            get
            {
                var a = checkAssociation( "target1" , "source1" , "Raivis" , "Artis" , false);
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
                var a = checkAssociation( "target1" , "source1" , "Raivis" , "Artis" , false);
                var list = value;
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
            }
        }
    }
}