using System;
using SqlConnectionDialog;

namespace Sample
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var connectionStringConstructor = new ConnectionStringFactory();
			var connectionString = connectionStringConstructor.BuildConnectionString();
			Console.WriteLine(connectionString);
			Console.ReadKey();
		}
	}
}
