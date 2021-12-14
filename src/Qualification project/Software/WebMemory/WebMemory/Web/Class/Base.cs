namespace WebAppOS
{
    public partial class WebClass 
    {
        /// <summary>
        /// Mainīgie
        /// </summary>
        private readonly TDAKernel _k; // TDAKernel instance
        private readonly IWebMemory _m; // WebMemory instance
        private readonly long _r; // Klases atsauce

        /// <summary>
        /// Konstruktors
        /// </summary>
        /// <param name="r">Klases atsauce</param>
        /// <param name="k">TDAKernel instance</param>
        /// <param name="m">WebMemory instance</param>
        public WebClass(long r, IWebMemory m) 
        {
            _r = r;
            _k = m.GetTDAKernel();
            _m = m;
        }

        /// <summary>
        /// Atgriež klases atsauci
        /// </summary>
        /// <returns></returns>
        public long GetReference { get { return _r; } }

        /// <summary>
        /// Klases vārds
        /// </summary>
        public string Name { get { return _k.GetClassName(_r); } }

        /// <summary>
        /// Pārbauda, vai klase c ir apakšklase
        /// </summary>
        /// <param name="c">apakšklase</param>
        public bool IsSubClassOf(WebClass c) { return _k.isDirectSubClass(_r,c._r); }
    }
}