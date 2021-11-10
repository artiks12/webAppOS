using WebAppOS;

namespace test
{
    class _Art_i123s
    {
        public IWebMemory _wm;
        public WebObject _object;

        public _Art_i123s ( IWebMemory wm )
        {
            _wm = wm;
            _object = null;
        }

        public _Art_i123s ( IWebMemory wm, long rObject )
        {
            _wm = wm;
            _object = new( rObject, wm );
        }

        public int Vecums 
        {
            get
            {
                BaseObject.checkObject( "Vecums" , "Integer" , "_Art_i123s" );
                return _object["Vecums"];
            }
            set
            {
                BaseObject.checkObject( "Vecums" , "Integer" , "_Art_i123s" );
                _object["Vecums"] = Convert.ToString( value );
            }
        }

        public List<Raivis> target1
        {
            get
            {
                var a = BaseObject.checkAssociation( "source1" , "target1" , "_Art_i123s" , "Raivis" , "False" );
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
                var a = BaseObject.checkAssociation( "source1" , "target1" , "_Art_i123s" , "Raivis" , "False");
                var list = value
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.add( l._object );
                }
            }
        }

        public List<Raivis> person
        {
            get
            {
                var a = BaseObject.checkAssociation( "person" , "account" , "_Art_i123s" , "_Art_i123s" , "True" );
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
                var a = BaseObject.checkAssociation( "person" , "account" , "_Art_i123s" , "_Art_i123s" , "True");
                var list = value
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.add( l._object );
                }
            }
        }

        public int sum1 ( )
        {
            return 0;
        }

        public string sum2 ( )
        {
            return null;
        }

        public bool sum3 ( )
        {
            return false;
        }

        public int sum4 ( )
        {
            return 0;
        }
    }
}