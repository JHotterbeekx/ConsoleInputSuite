using System;
using System.Collections.Generic;
using ConsoleInputSuite.Input;

namespace ConsoleInputSuite {
  public class ConsoleInputBuilder {

    private readonly List<InputQuestion> _Questions = new List<InputQuestion>();

    public ConsoleInputBuilder AddQuestion(string text) {
      _Questions.Add(new InputQuestion(text));
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