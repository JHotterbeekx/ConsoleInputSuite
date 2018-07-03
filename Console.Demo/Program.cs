using ConsoleInputSuite;

namespace Console.Demo {
  class Program {
    static void Main(string[] args) {

      new ConsoleInputBuilder()
        .AddQuestion("Who are you?")
        .AddQuestion("How old are you?")
        .AddQuestion("Where do you live?")
        .Render();

      System.Console.ReadLine();
    }
  }
}
