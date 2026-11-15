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

            Q rotation = GetBoneLocalRotation(bone);

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
        /// Creates rigid body based on bone. Uses box shape with dimensions from bone length.
        /// </summary>
        /// <param name="builder">Builder object.</param>
        /// <param name="bone">Bone</param>
        /// <param name="mode">Rigid body mode</param>
        /// <returns>Box <see cref="IPXBody"/> with dimensions from bone length.</returns>
        public static IPXBody CreateBoxBodyFromBone(this IPXPmxBuilder builder, IPXBone bone, BodyMode mode)
        {
            float size = bone.GetLength() / 2;
            V3 position = bone.GetPosition(0.5f);
            return builder.CreateBodyFromBone(bone, BodyBoxKind.Box, position, new V3(size, size, size), mode);
        }

        /// <summary>
        /// Gets length of the bone.
        /// </summary>
        /// <param name="bone">Bone</param>
        /// <returns>Length of the <paramref name="bone"/>.</returns>
        public static float GetLength(this IPXBone bone)
        {
            if (bone == null)
                return 0;
            IPXBone childBone = bone.ToBone;
            if (childBone != null)
                return (childBone.Position - bone.Position).Length();
            else
                return bone.ToOffset.Length();
        }

        /// <summary>
        /// Gets position along the bone
        /// </summary>
        /// <param name="bone">Bone</param>
        /// <param name="position">Position on the bone: 0 - at root, 1 - at tip.</param>
        /// <returns>Position on the bone.</returns>
        public static V3 GetPosition(this IPXBone bone, float position)
        {
            if (bone == null)
                return new V3();
            IPXBone childBone = bone.ToBone;
            if (childBone != null)
                return (childBone.Position + bone.Position) * position;
            else
                return bone.Position + bone.ToOffset * position;
        }

        /// <summary>
        /// Returns bone quaternion of local rotation
        /// </summary>
        /// <param name="bone">Bone</param>
        /// <returns>Local rotation</returns>
        public static Q GetBoneLocalRotation(this IPXBone bone)
        {
            Q rotation = new Q();
            if (bone == null)
                return rotation;

            bone.GetLocalAxis(out V3 x, out V3 y, out V3 z);
            rotation.FromDirection(-z, y);

            return rotation;
        }
    }
}
