namespace Plugins.ArchyUI.Core
{
    /// <summary>
    /// Optional settings for UIManager. If not provided, default settings are used.
    /// </summary>
    public class UIManagerSettings
    {
        public StackMode StackMode { get; set; } = StackMode.Overlay;
    }
}
