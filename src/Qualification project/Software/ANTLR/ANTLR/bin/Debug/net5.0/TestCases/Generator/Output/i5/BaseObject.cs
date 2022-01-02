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
            List<string> attributes = new() { "_int" , "Integer" , "_str" , "String" , "_bool" , "Boolean" , "_double" , "Real" };
            var o = checkClass( attributes , "test1" );
        }
    }
}