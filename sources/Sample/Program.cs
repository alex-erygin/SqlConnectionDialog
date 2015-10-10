using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var dialog = new SqlConnectionDialog.Dialog();
			dialog.ShowDialog();
		}
	}
}
