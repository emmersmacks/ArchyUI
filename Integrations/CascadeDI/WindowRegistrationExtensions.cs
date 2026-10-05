using CascadeDI.Container;
using Plugins.ArchyUI.Core;

namespace Plugins.ArchyUI.Integrations.CascadeDI
{
    public static class WindowRegistrationExtensions
    {
        public static IRegistrationBuilder<TImplementation> ToUIManager<TImplementation>(this IRegistrationBuilder<TImplementation> builder, IContainer container) where TImplementation : IWindowDI
        {
            return builder.ToInterface<IWindow>();
        }
    }
}