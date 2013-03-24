using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xander.PasswordValidator;

namespace ConsoleDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Please enter a password to see if it could be valid.");
      Console.Write("> ");
      string password = Console.ReadLine();

      Validator validator = new Validator();
      bool result = validator.Validate(password);
      if (result)
        Console.WriteLine("This password can be used.");
      else
        Console.WriteLine("This password failed the validation.");

      Console.WriteLine("Press a key to exit the console application.");
      Console.ReadKey();
    }
  }
}
