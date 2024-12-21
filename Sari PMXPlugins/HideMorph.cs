using PEPlugin;
using PEPlugin.Pmx;
using PEPlugin.View;
using System;
using PMXExtensions;
using System.Windows.Forms;
using PEPlugin.Pmd;

namespace Sari_PMXPlugins
{
    /// <summary>
    /// Hides Morph from facial manipulation panel in MMD by setting panel index to 0 <see cref="IPXMorph.Panel"/>
    /// </summary>
    public class HideMorph : PEPluginClass
    {
        public override string Name => "[Morph] Hide Morph";
        public override string Version => "1.0";
        public override string Description =>
            "Hides Morph from from facial manipulation panel in MMD";

        public HideMorph()
        {
            m_option = new PEPluginOption(false, true, "[Morph] Hide Morph");
        }

        public override void Run(IPERunArgs args)
        {
            base.Run(args);
            try
            {
                IPEConnector connector = args.Host.Connector;
                IPXPmxBuilder builder = args.Host.Builder.Pmx;
                IPXPmxViewConnector pmxView = connector.View.PmxView;

                int morphIndex = connector.Form.SelectedExpressionIndex;

                IPXPmx pmx = connector.Pmx.GetCurrentState();

                IPXMorph morph = pmx.GetMorphFromIndex(morphIndex);
                if (morph != null)
                    morph.Panel = 0;
                connector.Pmx.Update(pmx);
                connector.Form.UpdateList(UpdateObject.Morph);
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
