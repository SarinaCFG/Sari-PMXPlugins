using PEPlugin.Pmd;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using PEPlugin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PMXExtensions
{
    public static class BodyExtensions
    {
        /// <summary>
        /// Gets rigid bodies based on indexes.
        /// </summary>
        /// <param name="pmx">PMX</param>
        /// <param name="indexes">Indexes</param>
        /// <returns>List of <see cref="IPXBody"/></returns>
        public static IList<IPXBody> GetBodyFromIndexes(this IPXPmx pmx, int[] indexes)
        {
            IList<IPXBody> allBones = pmx.Body;
            IList<IPXBody> result = new List<IPXBody>();
            foreach (int index in indexes)
            {
                IPXBody body = allBones.ElementAtOrDefault(index);
                if (body != null)
                    result.Add(body);
            }
            return result;
        }

        /// <summary>
        /// Creates rigid body based on bone.
        /// </summary>
        /// <param name="builder">Builder object.</param>
        /// <param name="bone">Bone</param>
        /// <param name="boxKind">Kind of rigid body.</param>
        /// <param name="position">Position of rigid body.</param>
        /// <param name="size">Size of rigid body</param>
        /// <param name="mode">Rigid body mode</param>
        /// <returns><see cref="IPXBody"/></returns>
        /// <exception cref="ArgumentNullException">Bone, size and position shouldn't be <see langword="null"/></exception>
        public static IPXBody CreateBodyFromBone(this IPXPmxBuilder builder, IPXBone bone, BodyBoxKind boxKind, V3 position, V3 size, BodyMode mode)
        {
            if (bone == null)
                throw new ArgumentNullException(nameof(bone));
            if (size == null)
                throw new ArgumentNullException(nameof(size));
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            V3 x, y, z;
            bone.GetLocalAxis(out x, out y, out z);

            Q rotation = new Q();
            rotation.FromDirection(-z, y);

            IPXBody body = builder.Body();
            body.Name = bone.Name;
            body.NameE = bone.NameE;
            body.Bone = bone;
            body.Position = position;
            body.Rotation = rotation.ToRad();

            body.BoxKind = boxKind;
            body.BoxSize = size;
            body.Mode = mode;
            return body;
        }

        /// <summary>
        /// Creates rigid body based on bone. Uses box shape with dimentions from bone length.
        /// </summary>
        /// <param name="builder">Builder object.</param>
        /// <param name="bone">Bone</param>
        /// <param name="mode">Rigid body mode</param>
        /// <returns>Box <see cref="IPXBody"/> with dimentions from bone length.</returns>
        public static IPXBody CreateBoxBodyFromBone(this IPXPmxBuilder builder, IPXBone bone, BodyMode mode)
        {
            float size;
            V3 position;
            IPXBone childBone = bone.ToBone;
            if (childBone != null)
            {
                size = (childBone.Position - bone.Position).Length() / 2;
                position = (childBone.Position + bone.Position) / 2;
            }
            else
            {
                size = bone.ToOffset.Length() / 2;
                position = bone.Position + bone.ToOffset / 2;
            }
            return builder.CreateBodyFromBone(bone, BodyBoxKind.Box, position, new V3(size, size, size), mode);
        }
    }
}
