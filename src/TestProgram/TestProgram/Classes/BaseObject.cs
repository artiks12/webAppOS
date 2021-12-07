using WebAppOS;
using System.Collections.Generic;

namespace Test
{
    public class BaseObject
    {
        protected static IWebMemory _wm;
        protected static IRemoteWebCalls _wc;
        public WebObject _object;

        public BaseObject ( IWebMemory wm , IRemoteWebCalls wc )
        {
            _wm = wm;
            _wc = wc;
        }

        public BaseObject ( IWebMemory wm , IRemoteWebCalls wc , long rObject )
        {
            _wm = wm;
            _wc = wc;
        }

        protected void checkClass( List<string> attributes , string className )
        {
            var c = _wm.FindClassByName( className );
            if (c == null)
            {
                c = _wm.CreateClass( className );
            }
            for(int x=0; x<attributes.Count; x+=2)
            {
                var a = c.FindAttribute( attributes[x] );
                if (a == null)
                {
                    c.CreateAttribute( attributes[x] , attributes[x+1] );
                }
            }
        }

        protected void checkAssociations( List<string> associations , string className )
        {
            for (int x = 0; x < associations.Count; x += 4)
            {
                var cSource = _wm.FindClassByName( className );
                if (cSource == null)
                {
                    cSource = _wm.CreateClass( className );
                }
                var cTarget = _wm.FindClassByName( associations[x+2] );
                if (cTarget == null)
                {
                    cTarget = _wm.CreateClass( associations[x+2] );
                }
                var a = cSource.FindAssociationEnd( associations[x+1] );
                if (a == null)
                {
                    bool isComposition;
                    if (associations[x+3] == "true") { isComposition = true; }
                    else { isComposition = false; }
                    cSource.CreateAssociationEnd(cSource, cTarget, associations[x], associations[x + 1], isComposition);
                }
            }
        }
    }
}