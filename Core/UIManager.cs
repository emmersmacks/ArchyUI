using System;
using System.Collections.Generic;

namespace Plugins.ArchyUI.Core
{
    /// <summary>
    /// Central manager for registered windows. Does NOT instantiate windows — it only manages already-created controllers.
    /// Windows are registered by a strongly-typed identifier TWindow.
    /// </summary>
    public class UIManager : IDisposable
    {
        private readonly Dictionary<Type, IWindow> _registry = new();
        private readonly List<Type> _stack = new();

        public UIManager(List<IWindow> windows)
        {
            foreach (var window in windows)
            {
                Register(window);
            }
        }
        
        public void Register<TWindow>(TWindow window) where TWindow : IWindow
        {
            var type = window.GetType();
            if (!_registry.TryAdd(type, window))
                throw new InvalidOperationException($"Window type {type} already registered");
        }

        public void Unregister<TWindow>() where TWindow : IWindow
        {
            var key = typeof(TWindow);
            if (!_registry.ContainsKey(key))
                throw new Exception($"Window {key} is not registered");

            _registry.Remove(key);
            _stack.Remove(key);
        }

        // Push - show requested window on top of current ones (they stay visible), push it to stack
        public void Push(Type windowType, object payload = null)
        {
            if (!_registry.TryGetValue(windowType, out var reg))
                throw new KeyNotFoundException($"Window {windowType} is not registered");

            // if already top - simply show with payload
            if (_stack.Count > 0 && _stack[_stack.Count - 1] == windowType)
            {
                reg.Show(payload);
                return;
            }

            // remove previous occurrence if exists (bring to top)
            var existingIndex = _stack.IndexOf(windowType);
            if (existingIndex >= 0)
                _stack.RemoveAt(existingIndex);

            _stack.Add(windowType);
            reg.Show(payload);
        }

        // Open - hide all windows in stack and show requested window as the only one in stack
        public void Open(Type windowType, object payload = null)
        {
            if (!_registry.TryGetValue(windowType, out var reg))
                throw new KeyNotFoundException($"Window {windowType} is not registered");

            // hide from top to bottom; requested window is not hidden to avoid redundant Hide/Show
            for (var i = _stack.Count - 1; i >= 0; i--)
            {
                var type = _stack[i];
                if (type == windowType)
                    continue;

                if (_registry.TryGetValue(type, out var stackReg))
                    stackReg.Hide();
            }

            _stack.Clear();
            _stack.Add(windowType);
            reg.Show(payload);
        }

        // Pop - hide top and show previous if exists
        public void Pop()
        {
            if (_stack.Count == 0)
                return;

            var top = _stack[_stack.Count - 1];
            if (_registry.TryGetValue(top, out var topReg))
            {
                topReg.Hide();
            }

            _stack.RemoveAt(_stack.Count - 1);

            if (_stack.Count > 0)
            {
                var newTop = _stack[^1];
                if (_registry.TryGetValue(newTop, out var newTopReg))
                {
                    newTopReg.Show();
                }
            }
        }

        // PopTo - drop everything above target so it becomes top (inclusive behavior: target remains visible)
        public void PopTo(Type windowType)
        {
            var idx = _stack.LastIndexOf(windowType);
            if (idx < 0)
                return; // target not in stack

            var topIdx = _stack.Count - 1;
            if (idx == topIdx)
                return; // already at top

            // hide current top only - windows between target and top were never visible in the first place
            var top = _stack[topIdx];
            if (_registry.TryGetValue(top, out var topReg))
                topReg.Hide();

            _stack.RemoveRange(idx + 1, topIdx - idx);

            if (_registry.TryGetValue(windowType, out var reg))
                reg.Show();
        }

        public void Dispose()
        {
            _registry.Clear();
            _stack.Clear();
        }
    }
}




