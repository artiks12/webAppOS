using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace sniegs
{
    class _class8 : BaseObject
    {
        public _class8 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class8();
            _object = _wm.FindClassByName( "_class8" ).CreateObject();
        }

        public _class8 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class8();
            _object = new( rObject, wm );
        }


        public List<_class7> source1
        {
            get
            {
                var c = _wm.FindClassByName( "_class8" );
                var a = c.FindTargetAssociationEndByName( "source1" );
                var list = _object.LinkedObjects(a);
                List<_class7> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class7( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class8" );
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


        public List<_class9> target2
        {
            get
            {
                var c = _wm.FindClassByName( "_class8" );
                var a = c.FindTargetAssociationEndByName( "target2" );
                var list = _object.LinkedObjects(a);
                List<_class9> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class9( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class8" );
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