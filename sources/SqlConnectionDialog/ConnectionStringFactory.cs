namespace SqlConnectionDialog
{
	/// <summary>
	/// Constructs Sql Server connection string.
	/// </summary>
	public sealed class ConnectionStringFactory
	{
		/// <summary>
		/// Construct connection string.
		/// </summary>
		/// <returns>Connection string or empty string.</returns>
		public string BuildConnectionString()
		{
			var dialog = new Dialog();
			var dialogResult = dialog.ShowDialog();
			return dialogResult.GetValueOrDefault() ? dialog.ConnectionString : string.Empty;
		}
	}
}