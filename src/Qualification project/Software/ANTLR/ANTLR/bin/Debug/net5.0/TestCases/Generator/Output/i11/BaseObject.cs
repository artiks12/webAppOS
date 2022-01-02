using WebAppOS;
using System.Collections.Generic;

namespace Test
{
    public class BaseObject
    {
        protected static IWebMemory _wm;
        protected static IRemoteWebCalls _wc;
        public WebObject _object;


        protected BaseObject ( IWebMemory wm , IRemoteWebCalls wc )
        {
            _wm = wm;
            _wc = wc;
        }

        protected BaseObject ( IWebMemory wm , IRemoteWebCalls wc , long rObject )
        {
            _wm = wm;
            _wc = wc;
        }

        protected bool checkClass( List<string> attributes , string className )
        {
            var c = _wm.FindClassByName( className );
            if (c == null)
            {
                c = _wm.CreateClass( className );
            }
            else { return true; }
            for(int x=0; x<attributes.Count; x+=2)
            {
                checkAttribute( attributes[x] , attributes[x+1] , c );
            }
            return false;
        }

        protected void checkAttribute( string name , string type , WebClass c )
        {
            var a = c.FindAttributeByName( name );
            if (a == null)
            {
                c.CreateAttribute( name , type );
            }
        }

        protected void checkAssociationEnd( string sourceName , string targetName , string sourceClass , string targetClass , string Composition )
        {
            var cSource = _wm.FindClassByName( sourceClass );
            var cTarget = _wm.FindClassByName( targetClass );
            var a = cSource.FindTargetAssociationEndByName( targetName );
            if (a == null)
            {
                bool isComposition;
                if (Composition == "true") { isComposition = true; }
                else { isComposition = false; }
                cSource.CreateAssociation( cTarget, sourceName, targetName, isComposition);
            }
        }

        protected void _constructor_test1()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "test1" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "target1" , "source1" , "test1" , "false" , "source1" , "target1" , "test1" , "false" , "target2" , "source2" , "test2" , "false" };
               _constructor_test2();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "test1" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor_test2()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "test2" );
            if(o == false)
            {
               // SuperClass Check
               _constructor_test1();
               var c = _wm.FindClassByName( "test2");
               c.CreateGeneralization( "test1");

               // Association classes Check
               List<string> associations = new() { "source2" , "target2" , "test1" , "false" };
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "test2" , associations[x+2] ,  associations[x+3] );
               }
            }
        }
    }
}