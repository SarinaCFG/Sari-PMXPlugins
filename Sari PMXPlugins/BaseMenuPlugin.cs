using PEPlugin;
using PEPlugin.View;
using System;
using System.Windows.Forms;

namespace Sari_PMXPlugins
{
    /// <summary>
    /// Base class for menu plugin
    /// </summary>
    public abstract class BaseMenuPlugin : PEPluginClass
    {
        public BaseMenuPlugin() : base()
        {
            m_option = new PEPluginOption(bootup: false, regMenu: true, Name);
        }

        public override void Run(IPERunArgs args)
        {
            base.Run(args);
            try
            {
                IPEConnector connector = args.Host.Connector;
                IPXPmxBuilder builder = args.Host.Builder.Pmx;
                IPXPmxViewConnector pmxView = connector.View.PmxView;
                RunMenuPlugin(connector, builder, pmxView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Executes plugin from a menu
        /// </summary>
        /// <param name="connector">Connector</param>
        /// <param name="builder">Builder</param>
        /// <param name="pmxView">View</param>
        protected abstract void RunMenuPlugin(IPEConnector connector, IPXPmxBuilder builder, IPXPmxViewConnector pmxView);
    }
}
