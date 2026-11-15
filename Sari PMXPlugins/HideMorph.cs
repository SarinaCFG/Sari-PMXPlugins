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
    public class HideMorph : BaseMenuPlugin
    {
        public override string Name => "[Morph] Hide Morph";
        public override string Version => "1.1";
        public override string Description =>
            "Hides Morph from from facial manipulation panel in MMD";

        protected override void RunMenuPlugin(IPEConnector connector, IPXPmxBuilder builder, IPXPmxViewConnector pmxView)
        {
            int morphIndex = connector.Form.SelectedExpressionIndex;

            IPXPmx pmx = connector.Pmx.GetCurrentState();

            IPXMorph morph = pmx.GetMorphFromIndex(morphIndex);
            if (morph != null)
                morph.Panel = 0;
            connector.Pmx.Update(pmx);
            connector.Form.UpdateList(UpdateObject.Morph);
        }
    }
}
