using System.Collections.Generic;
using ConsoleInputSuite;

namespace Console.Demo {
  class Program {
    static void Main(string[] args) {

      new ConsoleInputBuilder()
        //.AddTextQuestion("Who are you?")
        //.AddNumericQuestion("How old are you?")
        //.AddTextQuestion("Where do you live?")
        .AddMultiSelect("What options?", new List<string> { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5"})
        .Render();

      System.Console.ReadLine();
    }
  }
}
