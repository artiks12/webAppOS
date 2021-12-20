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
            List<string> attributes = new() {  };
            checkClass( attributes , "test1" );
            List<string> associations = new() { "source1" , "target1" , "test2" , "false" , "target2" , "source2" , "test2" , "false" , "target3" , "source3" , "test2" , "true" , "source4" , "target4" , "test2" , "true" };
            checkAssociations( associations , "test1" );
            _object = _wm.FindClassByName( "test1" ).CreateObject();
        }

        public test1 ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() {  };
            checkClass( attributes , "test1" );
            List<string> associations = new() { "source1" , "target1" , "test2" , "false" , "target2" , "source2" , "test2" , "false" , "target3" , "source3" , "test2" , "true" , "source4" , "target4" , "test2" , "true" };
            checkAssociations( associations , "test1" );
            _object = new( rObject, wm );
        }


        public List<test2> target1
        {
            get
            {
                var c = _wm.FindClassByName( "test1" );
                var a = c.FindTargetAssociationEndByName( "test2" );
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
                var a = c.FindTargetAssociationEndByName( "test2" );
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
                var a = c.FindTargetAssociationEndByName( "test2" );
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
                var a = c.FindTargetAssociationEndByName( "test2" );
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
                var a = c.FindTargetAssociationEndByName( "test2" );
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
                var a = c.FindTargetAssociationEndByName( "test2" );
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
                var a = c.FindTargetAssociationEndByName( "test2" );
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
                var a = c.FindTargetAssociationEndByName( "test2" );
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