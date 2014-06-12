using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Toolkit.Mvvm
{
    //Inspired by the Prism DelegateCommand, and repeated in countless other places
    //such as http://wpftutorial.net/DelegateCommand.html
    //and http://relentlessdevelopment.wordpress.com/2010/03/30/simplified-mvvm-commanding-with-delegatecommand/
    public class DelegateCommand<T> : DelegateCommandBase
    {
        public DelegateCommand(Action<T> execute) : this(execute, o => true){ }
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute) : base((o) => execute((T)o), (o) => canExecute((T)o)) { }
    }

    public class DelegateCommand : DelegateCommandBase
    {

        public DelegateCommand(Action execute) : this(execute, () => true) { }
        public DelegateCommand(Action execute, Func<bool> canExecute) : base((o) => execute(), (o) => canExecute()) {  }
    }

    public class DelegateCommandBase : ICommand
    {

        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public DelegateCommandBase(Action<object> execute) : this(execute, (o) => true) { }

        public DelegateCommandBase(Action<object> execute, Func<object, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }

        private void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, new EventArgs());
            }
        }

        public void RaiseCanExecuteChanged()
        {
            this.OnCanExecuteChanged();
        }
    }
}
