using System;

namespace ConsoleInputSuite.Input {
  public class InputQuestion {

    private string _Question;
    private string _Answer;

    public InputQuestion(string question) {
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