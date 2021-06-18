using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TextToSpeechV3.Utility
{
	public class RelayCommand<T> : ICommand
	{

		private Action<T> _execute;
		private Predicate<object> _canExecute;

		public RelayCommand(Action<T> execute) : this(execute, null) { }

		public RelayCommand(Action<T> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute != null ? _canExecute(parameter) : true;
		}

		public void Execute(object parameter)
		{
			if (_execute != null)
			{
				_execute((T)parameter);
			}
		}

	}
}
