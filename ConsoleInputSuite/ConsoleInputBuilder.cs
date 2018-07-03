using System;
using System.Collections.Generic;

namespace ConsoleInputSuite {
  public class ConsoleInputBuilder {

    private List<string> _Questions = new List<string>();
    private List<string> _Answers = new List<string>();

    public ConsoleInputBuilder AddQuestion(string text) {
      _Questions.Add(text);
      return this;
    }

    public void Render() {
      Console.Clear();
      _Answers = new List<string>();

      foreach (var question in _Questions) {
        Console.Write($"{question} ");
        _Answers.Add(Console.ReadLine());
      }

      _ShowAnwers();
    }

    private void _ShowAnwers() {
      Console.Clear();
      for (int i = 0; i < _Questions.Count; i++) {
        Console.WriteLine($"{_Questions[i]} ");
        Console.WriteLine(_Answers[i]);
      }
    }

  }
}