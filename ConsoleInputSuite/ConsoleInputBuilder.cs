using System;
using System.Collections.Generic;
using ConsoleInputSuite.Input;
using ConsoleInputSuite.Input.Interface;

namespace ConsoleInputSuite {
  public class ConsoleInputBuilder {

    private readonly List<IQuestion> _Questions = new List<IQuestion>();

    public ConsoleInputBuilder AddTextQuestion(string question) {
      _Questions.Add(new InputQuestionText(question));
      return this;
    }

    public ConsoleInputBuilder AddNumericQuestion(string question) {
      _Questions.Add(new InputQuestionNumeric(question));
      return this;
    }

    public ConsoleInputBuilder AddMultiSelect(string question, List<string> options) {
      _Questions.Add(new InputMultiSelect(question, new List<InputMultiSelectOption> {
        new InputMultiSelectOption("1", "Option 1"),
        new InputMultiSelectOption("2", "Option 2", new List<InputMultiSelectOption> {
          new InputMultiSelectOption("2a", "Option 2a"),
          new InputMultiSelectOption("2b", "Option 2b"),
          new InputMultiSelectOption("2c", "Option 2c"),
        }),
        new InputMultiSelectOption("3", "Option 3"),
        new InputMultiSelectOption("4", "Option 4"),
        new InputMultiSelectOption("5", "Option 5"),
      }));
      return this;
    }

    public void Render() {
      Console.Clear();
      _Questions.ForEach(x => x.Ask());
      _ShowAnwers();
    }

    private void _ShowAnwers() {
      Console.Clear();
      _Questions.ForEach(x => x.ShowAnswer());
    }

  }
}