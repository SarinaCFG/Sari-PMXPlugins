using PEPlugin.Pmx;
using System.Collections.Generic;
using System.Linq;

namespace PMXExtensions
{
    public static class MorphExtensions
    {
        /// <summary>
        /// Get Morph from PMX by index.
        /// </summary>
        /// <param name="pmx">PMX</param>
        /// <param name="index">Index</param>
        /// <returns><see cref="IPXMorph"/></returns>
        public static IPXMorph GetMorphFromIndex(this IPXPmx pmx, int index)
        {
            IList<IPXMorph> allMorphs = pmx.Morph;
            return allMorphs.ElementAtOrDefault(index);
        }
    }
}
