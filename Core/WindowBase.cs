using System.Collections.Generic;

namespace Plugins.ArchyUI.Core
{
    public abstract class WindowBase<TView> : IWindow where TView : WindowViewBase
    {
        protected readonly TView _windowView;
        public List<IWindowController> Controllers { get; set; }

        public WindowBase(TView windowView)
        {
            _windowView = windowView;
            _windowView.gameObject.SetActive(false);
        }

        public void Show(object payload = null)
        {
            _windowView.gameObject.SetActive(true);
            foreach (var controller in Controllers)
            {
                controller.Show(payload);
            }
        }

        public void Hide()
        {
            foreach (var controller in Controllers)
            {
                controller.Hide();
            }
            _windowView.gameObject.SetActive(false);
        }
    }
}