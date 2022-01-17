using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace sniegs
{
    class _class7 : BaseObject
    {
        public _class7 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class7();
            _object = _wm.FindClassByName( "_class7" ).CreateObject();
        }

        public _class7 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class7();
            _object = new( rObject, wm );
        }


        public List<_class8> target1
        {
            get
            {
                var c = _wm.FindClassByName( "_class7" );
                var a = c.FindTargetAssociationEndByName( "target1" );
                var list = _object.LinkedObjects(a);
                List<_class8> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class8( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class7" );
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

    }
}