using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test1 : BaseObject
    {
        public test1 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor_test1();
            _object = _wm.FindClassByName( "test1" ).CreateObject();
        }

        public test1 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor_test1();
            _object = new( rObject, wm );
        }


        public List<test2> target1
        {
            get
            {
                var c = _wm.FindClassByName( "test1" );
                var a = c.FindTargetAssociationEndByName( "target1" );
                var list = _object.LinkedObjects(a);
                List<test2> result = new();
                foreach (var l in list)
                {
                    result.Add( new test2( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test1" );
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


        public List<test2> source2
        {
            get
            {
                var c = _wm.FindClassByName( "test1" );
                var a = c.FindTargetAssociationEndByName( "source2" );
                var list = _object.LinkedObjects(a);
                List<test2> result = new();
                foreach (var l in list)
                {
                    result.Add( new test2( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test1" );
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


        public List<test2> source3
        {
            get
            {
                var c = _wm.FindClassByName( "test1" );
                var a = c.FindTargetAssociationEndByName( "source3" );
                var list = _object.LinkedObjects(a);
                List<test2> result = new();
                foreach (var l in list)
                {
                    result.Add( new test2( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test1" );
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


        public List<test2> target4
        {
            get
            {
                var c = _wm.FindClassByName( "test1" );
                var a = c.FindTargetAssociationEndByName( "target4" );
                var list = _object.LinkedObjects(a);
                List<test2> result = new();
                foreach (var l in list)
                {
                    result.Add( new test2( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test1" );
                var a = c.FindTargetAssociationEndByName( "target4" );
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