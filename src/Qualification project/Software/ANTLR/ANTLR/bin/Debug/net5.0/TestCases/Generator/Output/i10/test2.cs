using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class test2 : BaseObject
    {
        private void _constructor()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "test2" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "target1" , "source1" , "test1" , "false" , "source2" , "target2" , "test1" , "false" , "source3" , "target3" , "test1" , "true" , "target4" , "source4" , "test1" , "true" };
               test1 test1 = new( _wm );
               test1 test1 = new( _wm );
               test1 test1 = new( _wm );
               test1 test1 = new( _wm );
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "test2" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        public test2 ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            _constructor();
            _object = _wm.FindClassByName( "test2" ).CreateObject();
        }

        public test2 ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)
        {
            _constructor();
            _object = new( rObject, wm );
        }

        public test2 ( IWebMemory wm ) : base( wm )
        {
            _constructor();
        }


        public List<test1> source1
        {
            get
            {
                var c = _wm.FindClassByName( "test2" );
                var a = c.FindTargetAssociationEndByName( "test1" );
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
                var a = c.FindTargetAssociationEndByName( "test1" );
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
                var a = c.FindTargetAssociationEndByName( "test1" );
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
                var a = c.FindTargetAssociationEndByName( "test1" );
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
                var a = c.FindTargetAssociationEndByName( "test1" );
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
                var a = c.FindTargetAssociationEndByName( "test1" );
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
                var a = c.FindTargetAssociationEndByName( "test1" );
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
                var a = c.FindTargetAssociationEndByName( "test1" );
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