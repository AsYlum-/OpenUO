﻿using System;
using System.Collections.Generic;
using System.Windows.Input;

using OpenUO.Core.PresentationFramework;

namespace OpenUO.Core.PresentationOpenUO.Core.Input
{
    public abstract class CommandBase : ICommand, IRaiseCanExecuteChanged
    {
        private readonly Func<object, bool> _canExecuteMethod;
        private readonly Action<object> _executeMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        protected CommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            Guard.RequireIsNotNull(executeMethod, "executeMethod");
            Guard.RequireIsNotNull(canExecuteMethod, "canExecuteMethod");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { WeakReferenceManager.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2); }
            remove { WeakReferenceManager.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value); }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return ((_canExecuteMethod == null) ? true : _canExecuteMethod(parameter));
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }

        protected virtual void OnCanExecuteChanged()
        {
            WeakReferenceManager.CallWeakReferenceHandlers(this, _canExecuteChangedHandlers);
        }
    }
}