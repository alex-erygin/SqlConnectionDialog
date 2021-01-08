using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SqlConnectionDialog
{
	/// <summary>
	///     Sql connection window
	/// </summary>
	internal partial class Dialog : INotifyPropertyChanged
	{
		private const string WindowsAuthentication = "Windows";
		private const string SqlServerAutentication = "SQL server authentication";
		public event PropertyChangedEventHandler PropertyChanged;

        private readonly string IsValidPropertyName = "IsValid";
		private string authentication;
		private string databaseName;
		private string serverName;
		private string userName;

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

		public string[] AuthenticationModes { get; } = {WindowsAuthentication, SqlServerAutentication};

		public bool IsValid => Validate();

		public bool IsCredentialInputEnabled => Authentication == SqlServerAutentication;

		public string ConnectionString { get; private set; }


		public ICommand TestCommand { get; set; }

		public ICommand CancelCommand { get; set; }

		public ICommand OkCommand { get; set; }


		private void SetupControls()
		{
			ServerNameTextBox.Focus();
			PasswordBox.PasswordChanged += (sender, args) => { OnPropertyChanged(IsValidPropertyName); };
			Authentication = WindowsAuthentication;

			IsVisibleChanged += (sender, args) =>
			{
				if (!IsVisible)
				{
					return;
				}

				//activate wpf window. see http://stackoverflow.com/questions/257587/bring-a-window-to-the-front-in-wpf
				Activate();
				Topmost = true;  // important
				Topmost = false; // important
				Focus();

				//set focus on textbox. see http://stackoverflow.com/questions/13955340/keyboard-focus-does-not-work-on-text-box-in-wpf
				Dispatcher.BeginInvoke(DispatcherPriority.Input,
					new Action(() =>
					{
						ServerNameTextBox.Focus(); // Set Logical Focus
						Keyboard.Focus(ServerNameTextBox); // Set Keyboard Focus
					}));
			};
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
			ConnectionString = this.BuildConnectionString();
			DialogResult = true;
		}

		private void Cancel()
		{
			DialogResult = false;
		}

		private void Test()
		{
			bool success = true;
			
			try
			{
				using (var connection = new SqlConnection(this.BuildConnectionString()))
				{
					connection.Open();
					connection.Close();
				}
			}
			catch
			{
				success = false;
			}

			MessageBox.Show(this, success ? "Connection succeeded" : "Connection failed");
		}

		private string BuildConnectionString()
		{
			var builder = new SqlConnectionStringBuilder
			{
				["Data Source"] = this.ServerName,
				["Initial Catalog"] = this.DatabaseName
			};

			if (Authentication == WindowsAuthentication)
			{
				builder["Integrated Security"] = true;
			}
			else
			{
				builder["User Id"] = this.UserName;
				builder["Password"] = this.PasswordBox.Password;
			}
			return builder.ConnectionString;
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
			       && ((Authentication == WindowsAuthentication ||
			            (Authentication == SqlServerAutentication && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PasswordBox.Password))));
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        internal static bool TestConnection(string conn)
        {
            bool success = true;

            try
            {
                using (var connection = new SqlConnection(conn))
                {
                    connection.Open();
                    connection.Close();
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }
    }
}