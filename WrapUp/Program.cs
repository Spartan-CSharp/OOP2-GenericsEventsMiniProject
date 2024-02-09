using System;
using System.Collections.Generic;

namespace WrapUp
{
	internal class Program
	{
		private static void Main()
		{
			DataAccess.BadEntryFound += DataAccess_BadEntryFound;

			List<PersonModel> people = new List<PersonModel> {
				new PersonModel { FirstName = "Tim", LastName = "Coreydarnit", Email = "tim@iamtimcorey.com"},
				new PersonModel { FirstName = "Sue", LastName = "Storm", Email = "sue@stormy.com"},
				new PersonModel { FirstName = "John", LastName = "Smith", Email = "John4537@aol.com"}
			};

			List<CarModel> cars = new List<CarModel> {
				new CarModel { Manufacturer = "Toyota", Model = "DARNCorolla" },
				new CarModel { Manufacturer = "Toyota", Model = "Highlander" },
				new CarModel { Manufacturer = "Ford", Model = "heckMustang" }
			};

			people.SaveToCSV(@"D:\Pierre\source\repos\Complete Foundation in C# Course Series\6 Object Oriented Programming Part 2\Homework\WrapUpHomeworkApp\people.csv");
			cars.SaveToCSV(@"D:\Pierre\source\repos\Complete Foundation in C# Course Series\6 Object Oriented Programming Part 2\Homework\WrapUpHomeworkApp\cars.csv");

			_ = Console.ReadLine();
		}

		private static void DataAccess_BadEntryFound(object sender, string e)
		{
			if ( e == "WrapUpHomework.PersonModel" )
			{
				PersonModel person = (PersonModel)sender;
				Console.WriteLine($"Bad Word(s) found in {e} {person.FirstName} {person.LastName}!");
			}
			else if ( e == "WrapUpHomework.CarModel" )
			{
				CarModel car = (CarModel)sender;
				Console.WriteLine($"Bad Word(s) found in {e} {car.Manufacturer} {car.Model}!");
			}
			else
			{
				Console.WriteLine($"Bad Word(s) found in {e}!");
			}

			Console.WriteLine();
		}
	}
}
