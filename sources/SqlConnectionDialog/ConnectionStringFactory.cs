namespace SqlConnectionDialog
{
	/// <summary>
	/// Constructs Sql Server connection string.
	/// </summary>
	public sealed class ConnectionStringFactory
	{
        private const string FILE_NAME = "ConnectionString.txt";

        /// <summary>
        /// Construct connection string.
        /// </summary>
        /// <returns>Connection string or empty string.</returns>
        public string BuildConnectionString()
        {
            var Conn = Read();
            var CanConnect = Conn != string.Empty;
            if (CanConnect)
                CanConnect &= Dialog.TestConnection(Conn);

            if (CanConnect)
            {
                return Conn;
            }
            else
            {
                var dialog = new Dialog();
                var dialogResult = dialog.ShowDialog();
                if (dialogResult.GetValueOrDefault())
                {
                    var _cconnstring = dialog.ConnectionString;
                    Write(_cconnstring);
                    return _cconnstring;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string Read()
        {

            var BaseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var ConfigFile = System.IO.Path.Combine(BaseDirectory, FILE_NAME);
            if (System.IO.File.Exists(ConfigFile))
            {
                using (var str = System.IO.File.OpenText(ConfigFile))
                {
                    return str.ReadLine();
                }
            }
            return string.Empty;
        }

        private void Write(string connectionStrngModel)
        {
            var BaseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var ConfigFile = System.IO.Path.Combine(BaseDirectory, FILE_NAME);
            if (!System.IO.File.Exists(ConfigFile))
            {
                using (var str = System.IO.File.CreateText(ConfigFile))
                {
                    str.WriteLine(connectionStrngModel);
                }
            }
            else
            {
                System.IO.File.WriteAllText(ConfigFile, connectionStrngModel);
            }
        }
    }
}