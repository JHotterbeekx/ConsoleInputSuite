using System;
using ConsoleInputSuite.Input.Interface;

namespace ConsoleInputSuite.Input {
  public class InputQuestionText : IQuestion {

    private string _Question;
    private string _Answer;

    public InputQuestionText(string question) {
      _Question = question;
    }

    public void Ask() {
      Console.Write($"{_Question} ");
      _Answer = Console.ReadLine();
    }

    public void ShowAnswer() {
      Console.WriteLine(_Question);
      Console.WriteLine(_Answer);
    }
  }
}