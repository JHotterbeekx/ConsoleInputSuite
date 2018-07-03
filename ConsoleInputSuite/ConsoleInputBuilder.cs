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
      _Questions.Add(new InputMultiSelect(question, options));
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