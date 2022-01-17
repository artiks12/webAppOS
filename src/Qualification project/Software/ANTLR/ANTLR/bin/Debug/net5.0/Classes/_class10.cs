using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace sniegs
{
    class _class10 : BaseObject
    {
        public _class10 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class10();
            _object = _wm.FindClassByName( "_class10" ).CreateObject();
        }

        public _class10 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class10();
            _object = new( rObject, wm );
        }


        public List<_class11> target1
        {
            get
            {
                var c = _wm.FindClassByName( "_class10" );
                var a = c.FindTargetAssociationEndByName( "target1" );
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
                var c = _wm.FindClassByName( "_class10" );
                var a = c.FindTargetAssociationEndByName( "target1" );
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
                _object.LinkObjects(a,result);
            }
        }


        public List<_class12> source3
        {
            get
            {
                var c = _wm.FindClassByName( "_class10" );
                var a = c.FindTargetAssociationEndByName( "source3" );
                var list = _object.LinkedObjects(a);
                List<_class12> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class12( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class10" );
                var a = c.FindTargetAssociationEndByName( "source3" );
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