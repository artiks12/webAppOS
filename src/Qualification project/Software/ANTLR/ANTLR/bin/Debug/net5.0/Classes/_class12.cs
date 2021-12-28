using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class _class12 : BaseObject
    {
        public _class12 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class12();
            _object = _wm.FindClassByName( "_class12" ).CreateObject();
        }

        public _class12 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class12();
            _object = new( rObject, wm );
        }


        public List<_class11> source2
        {
            get
            {
                var c = _wm.FindClassByName( "_class12" );
                var a = c.FindTargetAssociationEndByName( "source2" );
                var list = _object.LinkedObjects(a);
                List<_class11> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class11( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class12" );
                var a = c.FindTargetAssociationEndByName( "source2" );
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
                _object.LinkObjects(a,result);
            }
        }


        public List<_class10> target3
        {
            get
            {
                var c = _wm.FindClassByName( "_class12" );
                var a = c.FindTargetAssociationEndByName( "target3" );
                var list = _object.LinkedObjects(a);
                List<_class10> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class10( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class12" );
                var a = c.FindTargetAssociationEndByName( "target3" );
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