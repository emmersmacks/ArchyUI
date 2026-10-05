using CascadeDI.Container;
using Plugins.ArchyUI.Core;

namespace Plugins.ArchyUI.Integrations.CascadeDI
{
    public abstract class WindowBaseDI<WindowViewBaseT> : WindowBase<WindowViewBaseT>, IWindowDI where WindowViewBaseT : WindowViewBase
    {
        public WindowBaseDI(WindowViewBaseT windowView) : base(windowView) { }
        
        public abstract void Compose(IContainer builder);
    }
}