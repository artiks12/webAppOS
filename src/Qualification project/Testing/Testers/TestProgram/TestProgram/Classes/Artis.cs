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
            List<string> associations = new() { "source1" , "target1" , "Raivis" , "false" , "source2" , "target2" , "Raivis" , "false" , "target3" , "source3" , "Raivis" , "false" };
            checkAssociations( associations , "Artis" );
            _object = _wm.FindClassByName( "Artis" ).CreateObject();
        }

        public Artis ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "Artis" );
            List<string> associations = new() { "source1" , "target1" , "Raivis" , "false" , "source2" , "target2" , "Raivis" , "false" , "target3" , "source3" , "Raivis" , "false" };
            checkAssociations( associations , "Artis" );
            _object = new( rObject, wm );
        }


        public List<Raivis> target1
        {
            get
            {
                var c = _wm.FindClassByName( "Artis" );
                var a = c.FindTargetAssociationEndByName( "Raivis" );
                var list = _object.LinkedObjects(a);
                List<Raivis> result = new();
                foreach (var l in list)
                {
                    result.Add( new Raivis( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "Artis" );
                var a = c.FindTargetAssociationEndByName( "Raivis" );
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
                _object.LinkObjects(a,result);
            }
        }

        public List<Raivis> target2
        {
            get
            {
                var c = _wm.FindClassByName( "Artis" );
                var a = c.FindTargetAssociationEndByName( "Raivis" );
                var list = _object.LinkedObjects(a);
                List<Raivis> result = new();
                foreach (var l in list)
                {
                    result.Add( new Raivis( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "Artis" );
                var a = c.FindTargetAssociationEndByName( "Raivis" );
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
                _object.LinkObjects(a,result);
            }
        }

        public List<Raivis> source3
        {
            get
            {
                var c = _wm.FindClassByName( "Artis" );
                var a = c.FindTargetAssociationEndByName( "Raivis" );
                var list = _object.LinkedObjects(a);
                List<Raivis> result = new();
                foreach (var l in list)
                {
                    result.Add( new Raivis( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "Artis" );
                var a = c.FindTargetAssociationEndByName( "Raivis" );
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
                _object.LinkObjects(a,result);
            }
        }
    }
}