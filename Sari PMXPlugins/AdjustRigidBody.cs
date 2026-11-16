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
    public class AdjustRigidBody : BaseMenuPlugin
    {
        public override string Name => "[Rigid Body] Adjust rigid body";
        public override string Version => "1.0.1";

        public override string Description => "Adjust rigid body based on bone and radius";

        protected override void RunMenuPlugin(IPEConnector connector, IPXPmxBuilder builder, IPXPmxViewConnector pmxView)
        {
            int[] bodyIndexes = pmxView.GetSelectedBodyIndices();
            IPXPmx pmx = connector.Pmx.GetCurrentState();

            IEnumerable<IPXBody> capsuleBodies = pmx.GetBodyFromIndexes(bodyIndexes);

            if (capsuleBodies.Count() == 0)
                MessageBox.Show("No rigid bodies were selected.");

            foreach (IPXBody body in capsuleBodies)
            {
                IPXBone bone = body.Bone;
                if (bone == null)
                    continue;

                float boneLength = bone.GetLength();
                float capsuleRadius = body.BoxSize.X;

                body.Position = bone.GetPosition(0.5f);
                body.Rotation = bone.GetBoneLocalRotation().ToRad();

                switch (body.BoxKind)
                {
                    case BodyBoxKind.Sphere:
                        // do nothing
                        break;
                    case BodyBoxKind.Box:
                    case BodyBoxKind.Capsule:
                        body.BoxSize.Y = boneLength;
                        break;
                }
            }

            connector.Pmx.Update(pmx);
            connector.Form.UpdateList(UpdateObject.Body);
            connector.View.PmxView.UpdateView();
            connector.View.PmxView.UpdateModel();
        }
    }
}
