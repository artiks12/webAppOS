using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace sniegs
{
    class _class2 : _class1
    {
        public _class2 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor__class2();
            _object = _wm.FindClassByName( "_class2" ).CreateObject();
        }

        public _class2 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor__class2();
            _object = new( rObject, wm );
        }


        public long _int 
        {
            get { return Convert.ToInt64( _object["_int"] ); }
            set { _object["_int"] = Convert.ToString( value ); }
        }

        public bool _bool 
        {
            get { return Convert.ToBoolean( _object["_bool"] ); }
            set { _object["_bool"] = Convert.ToString( value ); }
        }


        public List<_class6> target1
        {
            get
            {
                var c = _wm.FindClassByName( "_class2" );
                var a = c.FindTargetAssociationEndByName( "target1" );
                var list = _object.LinkedObjects(a);
                List<_class6> result = new();
                foreach (var l in list)
                {
                    result.Add( new _class6( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "_class2" );
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