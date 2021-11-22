using WebAppOS;
using System.Collections.Generic;

namespace Test
{
    public class BaseObject
    {
        protected static IWebMemory _wm;
        protected static IWebCalls _wc;
        protected WebObject _object;

        public BaseObject ( IWebMemory wm , IWebCalls wc )
        {
            _wm = wm;
            _wc = wc;
            _object = null;
        }

        public BaseObject ( IWebMemory wm , IWebCalls wc , long rObject )
        {
            _wm = wm;
            _wc = wc;
            _object = new( rObject, wm );
        }

        protected void checkClass( List<string> attributes , string className )
        {
            var c = _wm.FindClassByName( className );
            if (c == null)
            {
                c = _wm.CreateClass( className );
            }
            else
            {
                for(int x=0; x<attributes.Count; x+=2)
                {
                    var a = c.FindAttribute( attributes[x] );
                    if (a == null)
                    {
                        a = c.CreateAttribute( attributes[x] , attributes[x+1] );
                    }
                }
            }
        }

        protected WebAssociationEnd checkAssociation( string associationNameSource, string associationNameTarget, string sourceClass, string targetClass, bool IsComposition )
        {
            var cSource = _wm.FindClassByName( sourceClass );
            if (cSource == null)
            {
                cSource = _wm.CreateClass( sourceClass );
            }
            var cTarget = _wm.FindClassByName( targetClass );
            if (cTarget == null)
            {
                cTarget = _wm.CreateClass( targetClass );
            }
            var a = cSource.FindAssociationEnd( associationNameTarget );
            if (a == null)
            {
                a = cSource.CreateAssociationEnd( cSource , cTarget , associationNameSource , associationNameTarget , IsComposition );
            }
            return a;
        }
    }
}