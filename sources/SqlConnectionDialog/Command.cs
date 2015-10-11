using System;
using System.Windows.Input;

namespace SqlConnectionDialog
{
	internal sealed class Command : ICommand
	{
		private readonly Predicate<object> canExecute;
		private readonly Action<object> execute;

		/// <summary>
		/// Ctor.
		/// </summary>
		/// <param name="execute">Action to execute.</param>
		public Command(Action<object> execute) : this(execute, null)
		{
		}

		/// <summary>
		/// Ctor.
		/// </summary>
		/// <param name="canExecute">Can execute.</param>
		/// <param name="execute">Action to execute.</param>
		public Command(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));
			this.canExecute = canExecute;
			this.execute = execute;
		}


		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public void Execute(object parameter)
		{
			if (canExecute != null && canExecute(parameter))
			{
				execute(parameter);
				return;
			}

			execute(parameter);
		}

		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <returns>
		/// true if this command can be executed; otherwise, false.
		/// </returns>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public bool CanExecute(object parameter)
		{
			if (canExecute != null)
			{
				return canExecute(parameter);
			};

			return true;
		}

		public void OnCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		public event EventHandler CanExecuteChanged;
	}


}
