namespace WebAppOS
{
    public class WebAssociationEnd 
    {
        /// <summary>
        /// Mainīgie
        /// </summary>
        private readonly TDAKernel _k; // TDAKernel instance
        private readonly long _r; // Associācijas galapunkta atsauce
        private readonly IWebMemory _m; // WebMemory instance


        /// <summary>
        /// Konstruktors
        /// </summary>
        /// <param name="r">Associācijas galapunkta atsauce</param>
        /// <param name="k">TDAKernel instance</param>
        /// <param name="m">WebMemory instance</param>
        public WebAssociationEnd(long r, IWebMemory m)
        {
            _r = r;
            _k = m.GetTDAKernel();
            _m = m;
        }

        /// <summary>
        /// Associācijas galapunkta lomas vārds
        /// </summary>
        public string Name
        {
            get { return _k.getRoleName(_r); }
        }

        /// <summary>
        /// Vai asociācijas galapunkts ir kompozīcija
        /// </summary>
        public bool IsComposition
        {
            get { return _k.IsComposition(_r); }
        }

        /// <summary>
        /// Atgriež avotklasi
        /// </summary>
        public WebClass SourceClass
        {
            get 
            { 
                var sClass = _k.getSourceClass(_r);
                return _m.FindClassByReference(sClass);
            }
        }

        /// <summary>
        /// Atgriež Merķklasi
        /// </summary>
        public WebClass TargetClass
        {
            get
            {
                var tClass = _k.getSourceClass(_r);
                return _m.FindClassByReference(tClass);
            }
        }

        /// <summary>
        /// Atgriež asociācijas atsauci
        /// </summary>
        public long GetReference() { return _r; }
    }
}
