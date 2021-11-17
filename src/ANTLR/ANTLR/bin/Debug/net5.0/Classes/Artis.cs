using WebAppOS;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    class Artis : BaseObject
    {
        public Artis ( IWebMemory wm , IWebCalls wc ) : base( wm , wc ) { }

        public Artis ( IWebMemory wm, IWebCalls wc , long rObject ) : base( wm , wc , rObject ) { }

        public int Vecums 
        {
            get
            {
                checkObject( "Vecums" , "Integer" , "Artis" );
                return Convert.ToInt32( _object["Vecums"] );
            }
            set
            {
                checkObject( "Vecums" , "Integer" , "Artis" );
                _object["Vecums"] = Convert.ToString( value );
            }
        }

        public string Vards 
        {
            get
            {
                checkObject( "Vards" , "String" , "Artis" );
                return _object["Vards"];
            }
            set
            {
                checkObject( "Vards" , "String" , "Artis" );
                _object["Vards"] = Convert.ToString( value );
            }
        }

        public bool IrStudents 
        {
            get
            {
                checkObject( "IrStudents" , "Boolean" , "Artis" );
                return Convert.ToBoolean( _object["IrStudents"] );
            }
            set
            {
                checkObject( "IrStudents" , "Boolean" , "Artis" );
                _object["IrStudents"] = Convert.ToString( value );
            }
        }

        public double Nauda 
        {
            get
            {
                checkObject( "Nauda" , "Real" , "Artis" );
                return Convert.ToDouble( _object["Nauda"] );
            }
            set
            {
                checkObject( "Nauda" , "Real" , "Artis" );
                _object["Nauda"] = Convert.ToString( value );
            }
        }
    }
}