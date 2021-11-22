using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class Artis : BaseObject
    {

        public Artis ( IWebMemory wm , IWebCalls wc ) : base( wm , wc )
        {
            List<string> attributes = new() { "Vecums" , "Integer" , "Vards" , "String" , "IrStudents" , "Boolean" , "Nauda" , "Real" };
            checkClass( attributes , "Artis" );
        }

        public Artis ( IWebMemory wm, IWebCalls wc , long rObject ) : base( wm , wc , rObject )
        {
            List<string> attributes = new() { "Vecums" , "Integer" , "Vards" , "String" , "IrStudents" , "Boolean" , "Nauda" , "Real" };
            checkClass( attributes , "Artis" );
        }


        public int Vecums 
        {
            get
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                return Convert.ToInt32( _object["Vecums"] );
            }
            set
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                _object["Vecums"] = Convert.ToString( value );
            }
        }

        public string Vards 
        {
            get
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                return _object["Vards"];
            }
            set
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                _object["Vards"] = Convert.ToString( value );
            }
        }

        public bool IrStudents 
        {
            get
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                return Convert.ToBoolean( _object["IrStudents"] );
            }
            set
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                _object["IrStudents"] = Convert.ToString( value );
            }
        }

        public double Nauda 
        {
            get
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                return Convert.ToDouble( _object["Nauda"] );
            }
            set
            {
                if (_object == null) { _object = _wm.FindClassByName( "Artis" ).CreateObject(); }
                _object["Nauda"] = Convert.ToString( value );
            }
        }

        public int sum ( int a , int b )
        {
            string arguments = JsonSerializer.Serialize( new { a , b } );
            string result = _wc.WebCall( _wm , _object.GetReference() , "sum" , arguments );
            return 0;
        }
    }
}