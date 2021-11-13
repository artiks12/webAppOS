using WebAppOS;
using System.Text.Json;

namespace Test
{
    class _Art_i123s : BaseObject
    {
        public _Art_i123s ( IWebMemory wm , IWebCalls wc ) : base( _wm , _wc ) { }

        public _Art_i123s ( IWebMemory wm, IWebCalls wc , long rObject ) : base( _wm , _wc , rObject ) { }

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
                var a = checkAssociation( "source1" , "target1" , "_Art_i123s" , "Raivis" , "False" );
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
                var a = checkAssociation( "source1" , "target1" , "_Art_i123s" , "Raivis" , "False");
                var list = value;
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
                var a = checkAssociation( "person" , "account" , "_Art_i123s" , "_Art_i123s" , "True" );
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
                var a = checkAssociation( "person" , "account" , "_Art_i123s" , "_Art_i123s" , "True");
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

        public string sum2 ( int a )
        {
            string json = JsonSerializer.Serialize( new { a } );
            string result = wc.webCall( _wm , _object.getReference() , "sum2" , json );
            return null;
        }

        public bool sum3 ( )
        {
            string json = JsonSerializer.Serialize( new {  } );
            string result = wc.webCall( _wm , _object.getReference() , "sum3" , json );
            return false;
        }

        public int sum4 ( )
        {
            string json = JsonSerializer.Serialize( new {  } );
            string result = wc.webCall( _wm , _object.getReference() , "sum4" , json );
            return 0;
        }
    }
}