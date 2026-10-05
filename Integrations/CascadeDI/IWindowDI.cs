using CascadeDI.Container;

namespace Plugins.ArchyUI.Integrations.CascadeDI
{
    public interface IWindowDI
    {
        void Compose(IContainer builder);
    }
}
