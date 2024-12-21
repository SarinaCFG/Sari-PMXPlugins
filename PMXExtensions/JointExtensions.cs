using PEPlugin;
using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;

namespace PMXExtensions
{
    public static class JointExtensions
    {
        /// <summary>
        /// Creates joint based on a bone.
        /// </summary>
        /// <param name="builder">Builder object</param>
        /// <param name="bone">Bone</param>
        /// <param name="bodyA">Rigid body A.</param>
        /// <param name="bodyB">Rigid body B.</param>
        /// <returns><see cref="IPXJoint"/></returns>
        /// <exception cref="ArgumentNullException">Bone should be not null.</exception>
        public static IPXJoint CreateJoint(this IPXPmxBuilder builder, IPXBone bone, IPXBody bodyA, IPXBody bodyB)
        {
            if (bone == null)
                throw new ArgumentNullException(nameof(bone));

            V3 x, y, z;
            bone.GetLocalAxis(out x, out y, out z);

            Q rotation = new Q();
            rotation.FromDirection(-z, y);

            IPXJoint joint = builder.Joint();
            joint.Name = bone.Name;
            joint.NameE = bone.NameE;
            joint.Position = bone.Position;
            joint.Rotation = rotation.ToRad();
            joint.BodyA = bodyA;
            joint.BodyB = bodyB;
            return joint;
        }

        /// <summary>
        /// Creates a joint based on two bodies
        /// </summary>
        /// <param name="builder">Builder object</param>
        /// <param name="bodyA">Rigid body A.</param>
        /// <param name="bodyB">Rigid body B.</param>
        /// <returns><see cref="IPXJoint"/></returns>
        public static IPXJoint CreateJoint(this IPXPmxBuilder builder, IPXBody bodyA, IPXBody bodyB)
        {
            IPXJoint joint = builder.Joint();
            joint.Name = bodyA.Name + " ~ " + bodyB.Name;
            joint.NameE = bodyA.NameE + " ~ " + bodyB.NameE;
            joint.Position = (bodyA.Position + bodyB.Position) / 2;
            joint.Rotation = (bodyA.Rotation + bodyB.Rotation) / 2;
            joint.BodyA = bodyA;
            joint.BodyB = bodyB;
            return joint;
        }
    }
}
