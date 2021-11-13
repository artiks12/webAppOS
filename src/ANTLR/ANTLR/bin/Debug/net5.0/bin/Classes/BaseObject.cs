using WebAppOS;

namespace Test
{
    public class BaseObject
    {
        protected IWebMemory _wm;
        protected IWebCalls _wc;
        protected WebObject _object;

        public BaseObject ( IWebMemory wm , IWebCalls wc )
        {
            _wm = wm;
            _wc = wc;
            _object = null;
        }

        public BaseObject ( IWebMemory wm , IWebCalls wc , long rObject )
        {
            _wc = wc;
            _object = new( rObject, wm );
        }

        protected void checkObject( string attributeName, string dataType, string className )
        {
            var c = _wm.findClassByName( className );
            if (c == null)
            {
                c = _wm.createClass( className );
            }
            var a = c.findAttribute( attributeName );
            if (a == null)
            {
                a = _wm.createAttribute( attributeName , dataType );
            }
            if (_object == null)
            {
                _object = c.createObject();
            }
        }

        protected WebAssociationEnd checkAssociation( string associationNameSource, string associationNameTarget, string sourceClass, string targetClass )
        {
            var cSource = _wm.findClassByName( sourceClass );
            if (cSource == null)
            {
                cSource = _wm.createClass( sourceClass );
            }
            var cTarget = _wm.findClassByName( targetClass );
            if (cTarget == null)
            {
                cTarget = _wm.createClass( targetClass );
            }
            var a = c.findAssociationEnd( associationNameTarget );
            if (a == null)
            {
                a = _wm.createAssociationEnd( cSource , cTarget , associationNameSource , associationNameTarget );
            }
            return a;
        }
    }
}