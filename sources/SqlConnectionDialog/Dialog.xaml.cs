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
			get { return this.serverName; }
			set
			{
				this.serverName = value;
				OnPropertyChanged(IsValidPropertyName);
			}
		}

		public string DatabaseName
		{
			get { return this.databaseName; }
			set
			{
				this.databaseName = value;
				this.OnPropertyChanged(this.IsValidPropertyName);
			}
		}

		public string Authentication
		{
			get { return this.authentication; }
			set
			{
				this.authentication = value;
				this.NotifyIsValid();
			}
		}

		public string UserName
		{
			get { return this.userName; }
			set
			{
				this.userName = value;
				this.OnPropertyChanged(this.IsValidPropertyName);
			}
		}

		public string[] AuthenticationModes { get; } = {"Windows", "SQL server autentication"};

		public bool IsValid => this.Validate();

		public bool IsCredentialInputEnabled => Authentication == "Windows";

		public ICommand TestCommand { get; set; }

		public ICommand CancelCommand { get; set; }

		public ICommand OkCommand { get; set; }


		private void SetupControls()
		{
			ServerNameTextBox.Focus();
			PasswordBox.PasswordChanged += (sender, args) => { this.OnPropertyChanged(IsValidPropertyName); };
			Authentication = "Windows";
		}

		private void InitCommands()
		{
			TestCommand = new Command(x => this.Test());
			OkCommand = new Command(x => this.Ok());
			CancelCommand = new Command(x => this.Cancel());
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

		private void NotifyIsValid()
		{
			this.OnPropertyChanged(this.IsValidPropertyName);
			this.OnPropertyChanged("IsCredentialInputEnabled");
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
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}