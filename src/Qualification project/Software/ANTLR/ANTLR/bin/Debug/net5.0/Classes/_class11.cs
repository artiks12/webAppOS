using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace sniegs
{
    class _class11 : BaseObject
    {
        public _class11 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class11();
            _object = _wm.FindClassByName( "_class11" ).CreateObject();
        }

        public _class11 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class11();
            _object = new( rObject, wm );
        }


        public List<_class10> source1
        {
            get
            {
                var c = _wm.FindClassByName( "_class11" );
                var a = c.FindTargetAssociationEndByName( "source1" );
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
                var c = _wm.FindClassByName( "_class11" );
                var a = c.FindTargetAssociationEndByName( "source1" );
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.Add( l._object );
                }
                _object.LinkObjects(a,result);
            }
        }


        public List<_class12> target2
        {
            get
            {
                var c = _wm.FindClassByName( "_class11" );
                var a = c.FindTargetAssociationEndByName( "target2" );
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
                var c = _wm.FindClassByName( "_class11" );
                var a = c.FindTargetAssociationEndByName( "target2" );
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