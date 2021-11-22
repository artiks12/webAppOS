using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class Artis : BaseObject
    {

        public Artis ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )
        {
            List<string> attributes = new() { "Vecums" , "Integer" , "Vards" , "String" , "IrStudents" , "Boolean" , "Nauda" , "Real" };
            checkClass( attributes , "Artis" );
        }

        public Artis ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )
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
            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference() , "sum" , arguments );
            var json = JsonDocument.Parse(result);
            JsonElement errorMessage;
            if (json.RootElement.TryGetProperty("error", out errorMessage) == true)
            {
                throw new Exception(errorMessage.GetString());
            }
            else
            {
                var r = json.RootElement.GetProperty("result");
                return r.GetInt32();
            }
        }
    }
}