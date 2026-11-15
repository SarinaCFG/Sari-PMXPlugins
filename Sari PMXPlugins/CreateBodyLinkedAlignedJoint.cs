using PEPlugin;
using PEPlugin.Pmd;
using PEPlugin.Pmx;
using PEPlugin.View;
using PMXExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Sari_PMXPlugins
{
    /// <summary>
    /// Creates RigidBody and joint based on bone or sequence of bones. The main difference from default behavior: the joint is aligned to bones axes.
    /// </summary>
    public class CreateBodyLinkedAlignedJoint : BaseMenuPlugin
    {
        public override string Name => "[Bone] Create RigidBody and linked Joint(aligned)";
        public override string Version => "1.1";
        public override string Description =>
            "Creates RigidBody and Joint based on a bone or sequence of bones";

        protected override void RunMenuPlugin(IPEConnector connector, IPXPmxBuilder builder, IPXPmxViewConnector pmxView)
        {
            int[] boneIndexes = pmxView.GetSelectedBoneIndices();
            IPXPmx pmx = connector.Pmx.GetCurrentState();

            IEnumerable<IPXBone> bones = pmx.GetBonesFromIndexes(boneIndexes);
            foreach (IPXBone bone in bones)
            {
                IPXBone parentBone = bone.Parent;
                IPXBody bodyA = pmx.Body.FirstOrDefault(body => body.Bone == parentBone);
                IPXBody bodyB = builder.CreateBoxBodyFromBone(bone, BodyMode.Dynamic);

                IPXJoint joint = builder.CreateJoint(bone, bodyA, bodyB);
                pmx.Body.Add(bodyB);
                pmx.Joint.Add(joint);
            }

            connector.Pmx.Update(pmx);
            connector.Form.UpdateList(UpdateObject.Body);
            connector.Form.UpdateList(UpdateObject.Joint);
            connector.View.PmxView.UpdateView();
            connector.View.PmxView.UpdateModel();
        }
    }
}
