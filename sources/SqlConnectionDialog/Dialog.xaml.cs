using System.Windows;
using System.Windows.Input;

namespace SqlConnectionDialog
{
	/// <summary>
	///     Sql connection window
	/// </summary>
	public partial class Dialog : Window
	{
		/// <summary>
		/// Ctor.
		/// </summary>
		public Dialog()
		{
			InitializeComponent();
			SetupFocus();
			InitDataContext();
			InitCommands();
		}

		public string ServerName { get; set; }

		public string DatabaseName { get; set; }

		public string Authentication { get; set; }

		public string UserName { get; set; }

		
		public ICommand TestCommand { get; set; }
		
		public ICommand CancelCommand { get; set; }
		
		public ICommand OkCommand { get; set; }


		private void SetupFocus()
		{
			
		}

		private void InitCommands()
		{
		}

		private void InitDataContext()
		{
			DataContext = this;
		}

		private void Ok()
		{
			
		}

		private void Cancel()
		{
			
		}

		private void Test()
		{
			
		}
	}
}