namespace Plugins.ArchyUI.Core
{
    /// <summary>
    /// Defines how UIManager treats windows below the top of the stack.
    /// </summary>
    public enum StackMode
    {
        /// <summary>Push shows new window on top, previous windows stay visible.</summary>
        Overlay,

        /// <summary>Push hides previous window, only the top window is visible.</summary>
        Replace
    }
}
