using WebAppOS;

namespace Classes
{
    class Raivis : _Art_i123s
    {
        public IWebMemory _wm;
        public WebObject _object;

        public Raivis ( IWebMemory wm )
        {
            _wm = wm;
            _object = null;
        }

        public Raivis ( IWebMemory wm, long rObject )
        {
            _wm = wm;
            _object = new( rObject, wm );
        }

        public List<_Art_i123s> source1
        {
            get
            {
                var a = BaseObject.checkAssociation( "source1" , "target1" , "Raivis" , "Raivis" , "False" );
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
                var a = BaseObject.checkAssociation( "source1" , "target1" , "Raivis" , "Raivis" , "False");
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.add( l._object );
                }
            }
        }

        public List<_Art_i123s> account
        {
            get
            {
                var a = BaseObject.checkAssociation( "person" , "account" , "Raivis" , "_Art_i123s" , "True" );
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
                var a = BaseObject.checkAssociation( "person" , "account" , "Raivis" , "_Art_i123s" , "True");
                var list = value;
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
    }
}