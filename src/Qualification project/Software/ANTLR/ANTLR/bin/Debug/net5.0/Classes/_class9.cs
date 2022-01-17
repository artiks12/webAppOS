using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace sniegs
{
    class _class9 : BaseObject
    {
        public _class9 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class9();
            _object = _wm.FindClassByName( "_class9" ).CreateObject();
        }

        public _class9 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class9();
            _object = new( rObject, wm );
        }


        public List<_class8> source2
        {
            get
            {
                var c = _wm.FindClassByName( "_class9" );
                var a = c.FindTargetAssociationEndByName( "source2" );
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
                var c = _wm.FindClassByName( "_class9" );
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

    }
}