using System;
using SqlConnectionDialog;

namespace Sample
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var factory = new ConnectionStringFactory();
			var connectionString = factory.BuildConnectionString();
			Console.WriteLine(connectionString);
			Console.ReadKey();
		}
	}
}