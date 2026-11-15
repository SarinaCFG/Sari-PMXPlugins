using PEPlugin;
using PEPlugin.Pmd;
using PEPlugin.Pmx;
using PEPlugin.View;
using PMXExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sari_PMXPlugins
{
    /// <summary>
    /// Adjust capsule rigid body length based on bone and radius, so it can overlap with other rigid bodies on the chain.
    /// </summary>
    public class AdjustCapsule : BaseMenuPlugin
    {
        public override string Name => "[Rigid Body] Adjust capsule";
        public override string Version => "1.0";

        public override string Description => "Adjust capsule rigid body based on bone and radius";

        protected override void RunMenuPlugin(IPEConnector connector, IPXPmxBuilder builder, IPXPmxViewConnector pmxView)
        {
            int[] bodyIndexes = pmxView.GetSelectedBodyIndices();
            IPXPmx pmx = connector.Pmx.GetCurrentState();

            IEnumerable<IPXBody> capsuleBodies = pmx.GetBodyFromIndexes(bodyIndexes)
                                                    .Where(body => body.BoxKind == PEPlugin.Pmd.BodyBoxKind.Capsule);

            if (capsuleBodies.Count() == 0)
                MessageBox.Show("No capsule rigid bodies were selected.");

            foreach (IPXBody body in capsuleBodies)
            {
                IPXBone bone = body.Bone;
                if (bone == null)
                    continue;

                float boneLength = bone.GetLength();
                float capsuleRadius = body.BoxSize.X;

                body.Position = bone.GetPosition(0.5f);
                body.BoxSize.Y = boneLength;
                body.Rotation = bone.GetBoneLocalRotation().ToRad();
            }

            connector.Pmx.Update(pmx);
            connector.Form.UpdateList(UpdateObject.Body);
            connector.View.PmxView.UpdateView();
            connector.View.PmxView.UpdateModel();
        }
    }
}
