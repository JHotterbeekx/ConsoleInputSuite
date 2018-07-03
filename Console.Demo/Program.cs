using ConsoleInputSuite;

namespace Console.Demo {
  class Program {
    static void Main(string[] args) {

      new ConsoleInputBuilder()
        .AddTextQuestion("Who are you?")
        .AddNumericQuestion("How old are you?")
        .AddTextQuestion("Where do you live?")
        .Render();

      System.Console.ReadLine();
    }
  }
}
