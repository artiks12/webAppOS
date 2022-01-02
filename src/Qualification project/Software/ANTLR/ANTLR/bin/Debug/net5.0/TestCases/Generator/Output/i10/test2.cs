using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test2 : test1
    {
        public test2 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor_test2();
            _object = _wm.FindClassByName( "test2" ).CreateObject();
        }

        public test2 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor_test2();
            _object = new( rObject, wm );
        }


        public List<test1> source1
        {
            get
            {
                var c = _wm.FindClassByName( "test2" );
                var a = c.FindTargetAssociationEndByName( "source1" );
                var list = _object.LinkedObjects(a);
                List<test1> result = new();
                foreach (var l in list)
                {
                    result.Add( new test1( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test2" );
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


        public List<test1> target2
        {
            get
            {
                var c = _wm.FindClassByName( "test2" );
                var a = c.FindTargetAssociationEndByName( "target2" );
                var list = _object.LinkedObjects(a);
                List<test1> result = new();
                foreach (var l in list)
                {
                    result.Add( new test1( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test2" );
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


        public List<test1> target3
        {
            get
            {
                var c = _wm.FindClassByName( "test2" );
                var a = c.FindTargetAssociationEndByName( "target3" );
                var list = _object.LinkedObjects(a);
                List<test1> result = new();
                foreach (var l in list)
                {
                    result.Add( new test1( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test2" );
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


        public List<test1> source4
        {
            get
            {
                var c = _wm.FindClassByName( "test2" );
                var a = c.FindTargetAssociationEndByName( "source4" );
                var list = _object.LinkedObjects(a);
                List<test1> result = new();
                foreach (var l in list)
                {
                    result.Add( new test1( _wm , _wc , l.GetReference ));
                }
                return result;
            }
            set
            {
                var c = _wm.FindClassByName( "test2" );
                var a = c.FindTargetAssociationEndByName( "source4" );
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