using WebAppOS;
using System.Collections.Generic;

namespace sniegs
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

        protected void _constructor__class1()
        {
            List<string> attributes = new() { "_int" , "Integer" , "_str" , "String" };
            var o = checkClass( attributes , "_class1" );
        }

        protected void _constructor__class2()
        {
            List<string> attributes = new() { "_int" , "Integer" , "_bool" , "Boolean" };
            var o = checkClass( attributes , "_class2" );
            if(o == false)
            {
               // SuperClass Check
               _constructor__class1();
               var c = _wm.FindClassByName( "_class2");
               c.CreateGeneralization( "_class1");

               // Association classes Check
               List<string> associations = new() { "source1" , "target1" , "_class6" , "false" };
               _constructor__class6();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class2" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor__class3()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "_class3" );
            if(o == false)
            {
               // SuperClass Check
               _constructor__class1();
               var c = _wm.FindClassByName( "_class3");
               c.CreateGeneralization( "_class1");
            }
        }

        protected void _constructor__class4()
        {
            List<string> attributes = new() { "_real" , "Real" };
            var o = checkClass( attributes , "_class4" );
            if(o == false)
            {
               // SuperClass Check
               _constructor__class2();
               var c = _wm.FindClassByName( "_class4");
               c.CreateGeneralization( "_class2");
            }
        }

        protected void _constructor__class5()
        {
            List<string> attributes = new() { "_real" , "Real" };
            var o = checkClass( attributes , "_class5" );
            if(o == false)
            {
               // SuperClass Check
               _constructor__class2();
               var c = _wm.FindClassByName( "_class5");
               c.CreateGeneralization( "_class2");
            }
        }

        protected void _constructor__class6()
        {
            List<string> attributes = new() { "_int" , "Integer" };
            var o = checkClass( attributes , "_class6" );
            if(o == false)
            {
               // SuperClass Check
               _constructor__class3();
               var c = _wm.FindClassByName( "_class6");
               c.CreateGeneralization( "_class3");

               // Association classes Check
               List<string> associations = new() { "target1" , "source1" , "_class2" , "false" };
               _constructor__class2();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class6" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor__class7()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "_class7" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "source1" , "target1" , "_class8" , "false" };
               _constructor__class8();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class7" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor__class8()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "_class8" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "target1" , "source1" , "_class7" , "false" , "source2" , "target2" , "_class9" , "false" };
               _constructor__class7();
               _constructor__class9();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class8" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor__class9()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "_class9" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "target2" , "source2" , "_class8" , "false" };
               _constructor__class8();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class9" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor__class10()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "_class10" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "source1" , "target1" , "_class11" , "false" , "target3" , "source3" , "_class12" , "false" };
               _constructor__class11();
               _constructor__class12();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class10" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor__class11()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "_class11" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "target1" , "source1" , "_class10" , "false" , "source2" , "target2" , "_class12" , "false" };
               _constructor__class10();
               _constructor__class12();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class11" , associations[x+2] ,  associations[x+3] );
               }
            }
        }

        protected void _constructor__class12()
        {
            List<string> attributes = new() {  };
            var o = checkClass( attributes , "_class12" );
            if(o == false)
            {
               // Association classes Check
               List<string> associations = new() { "target2" , "source2" , "_class11" , "false" , "source3" , "target3" , "_class10" , "false" };
               _constructor__class11();
               _constructor__class10();
               for(int x=0; x<associations.Count; x+=4)
               {
                   checkAssociationEnd( associations[x] , associations[x+1] , "_class12" , associations[x+2] ,  associations[x+3] );
               }
            }
        }
    }
}