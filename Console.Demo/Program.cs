using System.Collections.Generic;
using System;
using System.Linq;
using ConsoleInputSuite.Input;

namespace Console.Demo {
  class Program {
    static void Main(string[] args) {

      var selected = new InputMultiSelect("Select the shizzle",
                                          new List<InputMultiSelectOption> {
                                            new InputMultiSelectOption(1, "Option 1"),
                                            new InputMultiSelectOption(2, "Option 2"),
                                            new InputMultiSelectOption(3, "Option 3"),
                                          }).Ask();

      System.Console.WriteLine();
      System.Console.WriteLine("Answers:");
      selected.ToList().ForEach(x => System.Console.WriteLine($" the value {x}"));
      System.Console.ReadLine();
    }
  }
}
