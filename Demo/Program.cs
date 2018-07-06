using System.Collections.Generic;
using System;
using System.Linq;
using ConsoleInputSuite.Input;

namespace Demo {
  class Program {
    static void Main(string[] args) {

      var selected = new InputMultiSelect("Select the shizzle",
                                          new List<InputMultiSelectOption> {
                                            new InputMultiSelectOption(1, "Option 1"),
                                            new InputMultiSelectOption(2, "Option 2", new List<InputMultiSelectOption> {
                                              new InputMultiSelectOption(21, "Option 2.1"),
                                              new InputMultiSelectOption(22, "Option 2.2"),
                                            }),
                                            new InputMultiSelectOption(3, "Option 3"),
                                          }).Ask();

      Console.WriteLine();
      Console.WriteLine("Answers:");
      selected.ToList().ForEach(x => Console.WriteLine($" the value {x}"));
      Console.ReadLine();
    }
  }
}
