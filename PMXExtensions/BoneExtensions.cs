using PEPlugin.Pmx;
using System.Collections.Generic;
using System.Linq;

namespace PMXExtensions
{
    public static class BoneExtensions
    {
        /// <summary>
        /// Gets bones based on indexes.
        /// </summary>
        /// <param name="pmx">PMX</param>
        /// <param name="indexes">Indexes</param>
        /// <returns>List of <see cref="IPXBone"/></returns>
        public static IList<IPXBone> GetBonesFromIndexes(this IPXPmx pmx, int[] indexes)
        {
            IList<IPXBone> allBones = pmx.Bone;
            IList<IPXBone> result = new List<IPXBone>();
            foreach (int index in indexes)
            {
                IPXBone bone = allBones.ElementAtOrDefault(index);
                if (bone != null)
                    result.Add(bone);
            }
            return result;
        }
    }
}
