using System;
using System.Windows.Input;

namespace KPStringComparer.PresentationLayer.Command
{
    /// <summary>
    /// Represents a base command for all other commands.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return CanExecuteCore(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (!CanExecuteCore(parameter))
            {
                var errorMessage =
                    $"Command '{this}' cannot be executed with the argument specified '{parameter ?? "<null>"}'";
                throw new InvalidOperationException(errorMessage);
            }

            ExecuteCore(parameter);
        }

        #region Abstract members

        protected abstract void ExecuteCore(object parameter);

        protected abstract bool CanExecuteCore(object parameter);

        #endregion
    }
}