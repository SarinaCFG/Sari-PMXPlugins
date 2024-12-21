using PEPlugin.Pmd;
using PEPlugin.Pmx;
using PEPlugin.View;
using PEPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using PMXExtensions;
using System.Windows.Forms;

namespace Sari_PMXPlugins
{
    /// <summary>
    /// Creates RigidBody and joint based on bone or sequence of bones. The main difference from default behavior: the joint is aligned to bones axes.
    /// </summary>
    public class CreateBodyLinkedAlignedJoint : PEPluginClass
    {
        public override string Name => "[Bone] Create RigidBody and linked Joint(aligned)";
        public override string Version => "1.0";
        public override string Description =>
            "Creates RigidBody and Joint based on a bone or sequence of bones";

        public CreateBodyLinkedAlignedJoint()
        {
            m_option = new PEPluginOption(
                false,
                true,
                "[Bone] Create RigidBody and linked Joint(aligned)"
            );
        }

        public override void Run(IPERunArgs args)
        {
            base.Run(args);
            try
            {
                IPEConnector connector = args.Host.Connector;
                IPXPmxBuilder builder = args.Host.Builder.Pmx;
                IPXPmxViewConnector pmdView = connector.View.PmxView;

                int[] boneIndexes = pmdView.GetSelectedBoneIndices();
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
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            }
        }
    }
}
