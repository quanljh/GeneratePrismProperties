using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GeneratePrismProperties.Properties;

namespace GeneratePrismProperties
{
    public class DelegateCommand : DelegateCommandBase
    {
        #region Private Members

        private Action _executeMethod;
        private Func<bool> _canExecuteMethod;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="DelegateCommand"/> with the <see cref="Action"/> to invoke on execution.
        /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute(object)"/> is called.</param>
        /// </summary>
        public DelegateCommand(Action executeMethod) : this(executeMethod, () => true)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="DelegateCommand"/> with the <see cref="Action"/> to invoke on execution
        /// and a <see langword="Func"/> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        /// <param name="canExecuteMethod">The <see cref="Func{TResult}"/> to invoke when <see cref="ICommand.CanExecute"/> is called</param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), Resources.DelegateCommand_DelegateCommandDelegatesCannotBeNull);

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        #endregion


        public bool CanExecute()
        {
            return _canExecuteMethod();
        }

        public void Execute()
        {
            _executeMethod();
        }


        /// <inheritdoc />
        protected override void Execute(object parameter)
        {
            Execute();
        }

        /// <inheritdoc />
        protected override bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <summary>
        /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public DelegateCommand ObservesProperty<T>(Expression<Func<T>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        /// Observes a property that is used to determine if this command can execute, and if it implements INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public DelegateCommand ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            _canExecuteMethod = canExecuteExpression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }
    }
}
