using WebAppOS;
using System.Collections.Generic;

namespace Test
{
    public class BaseObject
    {
        protected static IWebMemory _wm;
        protected static IRemoteWebCalls _wc;
        public WebObject _object;

        public BaseObject ( IWebMemory wm , IRemoteWebCalls wc )
        {
            _wm = wm;
            _wc = wc;
        }

        public BaseObject ( IWebMemory wm , IRemoteWebCalls wc , long rObject )
        {
            _wm = wm;
            _wc = wc;
        }

        public BaseObject ()
        {
            
        }

        protected void checkClass( List<string> attributes , List<string> associations , string className )
        {
            var c = _wm.FindClassByName( className );
            if (c == null)
            {
                c = _wm.CreateClass( className );
            }
            for(int x=0; x<attributes.Count; x+=2)
            {
                checkAttribute( attributes[x] , attributes[x+1] , c );
            }
            for(int x=0; x<associations.Count; x+=4)
            {
                checkAssociationEnd( associations[x] , associations[x+1] , className , associations[x+2] ,  associations[x+3] );
            }
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
            if (cTarget == null)
            {
                cTarget = _wm.CreateClass( targetClass );
            }
            var a = cSource.FindTargetAssociationEndByName( targetName );
            if (a == null)
            {
                bool isComposition;
                if (Composition == "true") { isComposition = true; }
                else { isComposition = false; }
                cSource.CreateAssociation( cTarget, sourceName, targetName, isComposition);
            }
        }
    }
}