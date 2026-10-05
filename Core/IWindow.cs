using System.Collections.Generic;

namespace Plugins.ArchyUI.Core
{
    /// <summary>
    /// Marker interface for strongly-typed window identifiers.
    /// Each window/popup in the project should have a distinct type that implements this
    /// (often an empty class used only as an identifier).
    /// </summary>
    public interface IWindow
    {
        List<IWindowController> Controllers { get; set; }
        
        void Show(object payload = null);

        void Hide();
    }
}


