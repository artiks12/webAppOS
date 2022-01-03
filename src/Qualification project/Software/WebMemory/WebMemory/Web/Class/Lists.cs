using System.Collections.Generic;
using System.Linq;

namespace WebAppOS
{
    public partial class WebClass 
    {
        /// <summary>
        ///  Saraksts ar virsklasēm
        /// </summary>
        public IEnumerable<WebClass> SuperClasses()
        {
            var d = Dictionaries.D_GetSuperClasses(_r,_k,_m);
            IEnumerable<WebClass> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar apakšklasēm
        /// </summary>
        public IEnumerable<WebClass> SubClasses()
        {
            var d = Dictionaries.D_GetSubClasses(_r,_k,_m);
            IEnumerable<WebClass> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar atribūtiem
        /// </summary>
        public IEnumerable<WebAttribute> Attributes()
        {
            var d = Dictionaries.D_GetClassAttributes(_r,_k,_m);
            IEnumerable<WebAttribute> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar ieejošo associāciju galapunktiem
        /// </summary>
        public IEnumerable<WebAssociationEnd> IngoingAssociationEnds()
        {
            var d = Dictionaries.D_GetClassIngoingAssociationEnds(_r,_k,_m);
            IEnumerable<WebAssociationEnd> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar izejošo associāciju galapunktiem
        /// </summary>
        public IEnumerable<WebAssociationEnd> OutgoingAssociationEnds()
        {
            var d = Dictionaries.D_GetClassOutgoingAssociationEnds(_r, _k, _m);
            IEnumerable<WebAssociationEnd> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar izejošo associāciju galapunktiem
        /// </summary>
        public IEnumerable<WebObject> Objects()
        {
            var d = Dictionaries.D_GetClassObjects(_r, _k, _m);
            IEnumerable<WebObject> query = from i in d select i.Value;
            return query;
        }
    }
}