using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class _class6 : _class3
    {
        public _class6 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class6();
            _object = _wm.FindClassByName( "_class6" ).CreateObject();
        }

        public _class6 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class6();
            _object = new( rObject, wm );
        }


        public long _int 
        {
            get { return Convert.ToInt64( _object["_int"] ); }
            set { _object["_int"] = Convert.ToString( value ); }
        }


        public List<_class2> source1
        {
            get
            {
                var c = _wm.FindClassByName( "_class6" );
                var a = c.FindTargetAssociationEndByName( "source1" );
                var list = _object.LinkedObjects(a);
                List<_class2> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class2( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class6" );
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

    }
}