using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WrapUp
{
	internal static class DataAccess
	{
		internal static event EventHandler<string> BadEntryFound;

		internal static void SaveToCSV<T>(this List<T> items, string filePath) where T : new()
		{
			List<string> rows = new List<string>();

			T entry = new T();
			PropertyInfo[] cols = entry.GetType().GetProperties();

			string row = "";
			foreach ( PropertyInfo col in cols )
			{
				row += $",{col.Name}";
			}

			row = row.Substring(1);
			rows.Add(row);

			foreach ( T item in items )
			{
				row = "";
				bool badworddetected = false;

				foreach ( PropertyInfo col in cols )
				{
					string val = col.GetValue(item).ToString();
					badworddetected = BadWordDetector(val);
					if ( badworddetected )
					{
						BadEntryFound?.Invoke(item, item.GetType().ToString());
						break;
					}

					row += $",{val}";
				}

				if ( !badworddetected )
				{
					row = row.Substring(1);
					rows.Add(row);
				}
			}

			File.WriteAllLines(filePath, rows);
		}

		private static bool BadWordDetector(string stringToTest)
		{
			bool output = false;
			string lowercasetest = stringToTest.ToLower();

			if ( lowercasetest.Contains("heck") || lowercasetest.Contains("darn") )
			{
				output = true;
			}

			return output;
		}
	}
}
