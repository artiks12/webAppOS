using WebAppOS;

namespace test
{
    class BaseObject
    {
        protected IWebMemory _wm;
        public WebObject _object;

        public BaseObject ( IWebMemory wm )
        {
            _wm = wm;
            _object = null;
        }

        public BaseObject ( IWebMemory wm, long rObject )
        {
            _wm = wm;
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

    class _Art_i123s : BaseObject
    {
        public string Vards 
        {
            get
            {
                checkObject( "Vards" , "String" , "_Art_i123s" );
                return _object["Vards"];
            }
            set
            {
                checkObject( "Vards" , "String" , "_Art_i123s" );
                _object["Vards"] = Convert.ToString( value );
            }
        }
    }

    class Raivis : BaseObject, _Art_i123s
    {
        public int sum1 ( int a , int b , int c  )
        {
            return 0;
        }
    }
}