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
        public int Vecums 
        {
            get
            {
                checkObject( "Vecums" , "Integer" , "_Art_i123s" );
                return _object["Vecums"];
            }
            set
            {
                checkObject( "Vecums" , "Integer" , "_Art_i123s" );
                _object["Vecums"] = Convert.ToString( value );
            }
        }

        public List<Raivis> target1
        {
            get
            {
                var a = checkAssociation( "source1" , "target1" , "_Art_i123s" , "Raivis" );
                var list = _object.LinkedObjects(a);
                List<Raivis> result = _object.LinkedObjects(a);
                foreach (var l in list)
                {
                    result.add( new Raivis( _vm , l.getReference() ));
                }
                return result;
            }
            set
            {
                var a = checkAssociation( "source1" , "target1" , "_Art_i123s" , "Raivis" );
                var list = value
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.add( l._object );
                }
            }
        }

        public int sum1 ( int a , int b , int c  )
        {
            return 0;
        }

        public string sum2 ( int a  )
        {
            return null;
        }

        public bool sum3 ( )
        {
            return false;
        }
    }

    class Raivis : BaseObject, _Art_i123s
    {
        public List<_Art_i123s> target2
        {
            get
            {
                var a = checkAssociation( "source2" , "target2" , "Raivis" , "_Art_i123s" );
                var list = _object.LinkedObjects(a);
                List<_Art_i123s> result = _object.LinkedObjects(a);
                foreach (var l in list)
                {
                    result.add( new _Art_i123s( _vm , l.getReference() ));
                }
                return result;
            }
            set
            {
                var a = checkAssociation( "source2" , "target2" , "Raivis" , "_Art_i123s" );
                var list = value
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.add( l._object );
                }
            }
        }
    }
}