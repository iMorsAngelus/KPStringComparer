using System;

namespace KPStringComparer.PresentationLayer.Command
{
    /// <summary>
    /// Class, who implemented ICommand interface
    /// </summary>
    public class ActionCommand : BaseCommand
    {
        private readonly Action<object> _execute;
        private Predicate<object> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="execute">The actor to be executed at a command execution.</param>
        public ActionCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="execute">The actor to be executed at a command execution.</param>
        /// <param name="canExecute">The predicate to check command execution possibility</param>
        public ActionCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        #region Overriding of BaseCommand

        /// <summary>
        /// When overriden in derived classes executes the command.
        /// </summary>
        protected override void ExecuteCore(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// When overriden in derived classes checks whether command can be executed.
        /// </summary>
        protected override bool CanExecuteCore(object parameter)
        {
            return _canExecute(parameter);
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
        #endregion
    }
}
