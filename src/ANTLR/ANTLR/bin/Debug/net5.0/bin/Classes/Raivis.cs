using WebAppOS;
using System.Text.Json;

namespace Test
{
    class Raivis : _Art_i123s
    {
        public Raivis ( IWebMemory wm , IWebCalls wc ) : base( _wm , _wc ) { }

        public Raivis ( IWebMemory wm, IWebCalls wc , long rObject ) : base( _wm , _wc , rObject ) { }

        public List<_Art_i123s> source1
        {
            get
            {
                var a = checkAssociation( "source1" , "target1" , "Raivis" , "Raivis" , "False" );
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
                var a = checkAssociation( "source1" , "target1" , "Raivis" , "Raivis" , "False");
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
                var a = checkAssociation( "person" , "account" , "Raivis" , "_Art_i123s" , "True" );
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
                var a = checkAssociation( "person" , "account" , "Raivis" , "_Art_i123s" , "True");
                var list = value;
                List<WebObject> result = new();
                foreach (var l in list)
                {
                    result.add( l._object );
                }
            }
        }

        public int sum1 ( int a , int b , int c )
        {
            string json = JsonSerializer.Serialize( new { a , b , c } );
            string result = wc.webCall( _wm , _object.getReference() , "sum1" , json );
            return 0;
        }
    }
}