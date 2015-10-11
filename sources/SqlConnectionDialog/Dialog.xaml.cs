using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SqlConnectionDialog
{
	/// <summary>
	///     Sql connection window
	/// </summary>
	public partial class Dialog : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private readonly string IsValidPropertyName = "IsValid";
		private string authentication;
		private string databaseName;
		private string serverName;
		private string userName;


		/// <summary>
		///     Ctor.
		/// </summary>
		public Dialog()
		{
			InitializeComponent();
			InitDataContext();
			SetupControls();
			InitCommands();
		}

		public string ServerName
		{
			get { return serverName; }
			set
			{
				serverName = value;
				OnPropertyChanged(IsValidPropertyName);
			}
		}

		public string DatabaseName
		{
			get { return databaseName; }
			set
			{
				databaseName = value;
				NotifyIsValid();
			}
		}

		public string Authentication
		{
			get { return authentication; }
			set
			{
				authentication = value;
				NotifyIsValid();
			}
		}

		public string UserName
		{
			get { return userName; }
			set
			{
				userName = value;
				NotifyIsValid();
			}
		}

		public string[] AuthenticationModes { get; } = {"Windows", "SQL server autentication"};

		public bool IsValid => Validate();

		public bool IsCredentialInputEnabled => Authentication == "SQL server autentication";
	

		public ICommand TestCommand { get; set; }

		public ICommand CancelCommand { get; set; }

		public ICommand OkCommand { get; set; }


		private void SetupControls()
		{
			ServerNameTextBox.Focus();
			PasswordBox.PasswordChanged += (sender, args) => { OnPropertyChanged(IsValidPropertyName); };
			Authentication = "Windows";
		}

		private void InitCommands()
		{
			TestCommand = new Command(x => Test());
			OkCommand = new Command(x => Ok());
			CancelCommand = new Command(x => Cancel());
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
			DialogResult = false;
		}

		private void Test()
		{
		}

		private void NotifyIsValid()
		{
			OnPropertyChanged(IsValidPropertyName);
			OnPropertyChanged("IsCredentialInputEnabled");
		}

		private bool Validate()
		{
			return !string.IsNullOrEmpty(DatabaseName)
			       && !string.IsNullOrEmpty(ServerName)
			       && ((Authentication == "Windows" ||
			            (Authentication == "SQL server autentication" && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PasswordBox.Password))));
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}