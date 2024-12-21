using PEPlugin.Pmd;
using PEPlugin.Pmx;
using PEPlugin.View;
using PEPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PMXExtensions;

namespace Sari_PMXPlugins
{
    /// <summary>
    /// Adds a joint based on pair of rigid bodies or two groups of bodies.
    /// </summary>
    public class AddSideJoint : PEPluginClass
    {
        public override string Name => "[Joint] AddSideJoint";
        public override string Version => "1.0";
        public override string Description => "Adds side joint";

        public AddSideJoint()
        {
            m_option = new PEPluginOption(false, true, "[Joint] AddSideJoint");
        }

        public override void Run(IPERunArgs args)
        {
            base.Run(args);
            try
            {
                IPEConnector connector = args.Host.Connector;
                IPXPmxBuilder builder = args.Host.Builder.Pmx;
                IPXPmxViewConnector pmxView = connector.View.PmxView;

                int[] bodyIndexes = pmxView.GetSelectedBodyIndices();
                IPXPmx pmx = connector.Pmx.GetCurrentState();

                IEnumerable<IPXBody> bodies = pmx.GetBodyFromIndexes(bodyIndexes);
                if (bodies.Count() % 2 == 0)
                {
                    IList<(IPXBody, IPXBody)> pairs = new List<(IPXBody, IPXBody)>();
                    for (int i = 0; i < bodies.Count() / 2; i++)
                    {
                        IPXBody bodyA = bodies.ElementAt(i);
                        IPXBody bodyB = bodies.ElementAt(i + bodies.Count() / 2);
                        pairs.Add((bodyA, bodyB));
                    }
                    string pairsString = string.Join(
                        Environment.NewLine,
                        pairs
                            .Select(a => string.Format("{0} ~ {1}", a.Item1.Name, a.Item2.Name))
                            .ToArray()
                    );
                    DialogResult answer = MessageBox.Show(
                        string.Format(
                            "Joint(s) will be created for the following pairs of bodies:{1}{0}", pairsString, Environment.NewLine),
                                                          "Info",
                                                          MessageBoxButtons.OKCancel);
                    if (DialogResult.OK == answer)
                    {
                        foreach ((IPXBody, IPXBody) pair in pairs)
                        {
                            IPXJoint joint = builder.CreateJoint(pair.Item1, pair.Item2);
                            pmx.Joint.Add(joint);
                        }
                        connector.Pmx.Update(pmx);
                        connector.Form.UpdateList(UpdateObject.All);
                        connector.View.PMDView.UpdateModel();
                        connector.View.PMDView.UpdateView();
                    }
                    else
                    {
                        MessageBox.Show("Plugin operation was canceled.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select even number of bodies");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
