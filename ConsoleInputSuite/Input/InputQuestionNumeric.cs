using System;
using ConsoleInputSuite.Input.Interface;

namespace ConsoleInputSuite.Input {
  public class InputQuestionNumeric : IQuestion {

    private string _Question;
    private int _Answer;

    public InputQuestionNumeric(string question) {
      _Question = question;
    }

    public void Ask() {
      Console.Write($"{_Question} ");
      _Answer = _ReadAnswer();
    }

    private int _ReadAnswer() {
      string input = string.Empty;

      while (true) {
        var inputCharacter = Console.ReadKey(true);
        if (char.IsDigit(inputCharacter.KeyChar.ToString(), 0)) {
          input += inputCharacter.KeyChar;
          Console.Write(inputCharacter.KeyChar);
        }

        if (inputCharacter.Key == ConsoleKey.Enter) {
          Console.WriteLine();
          return Int32.Parse(input);
        }

        if (inputCharacter.Key == ConsoleKey.Backspace) {
          if (input.Length == 0) continue;
          Console.CursorLeft -= 1;
          Console.Write(" ");
          Console.CursorLeft -= 1;
          input = input.Substring(0, input.Length - 1);
        }
      }
    }

    public void ShowAnswer() {
      Console.WriteLine(_Question);
      Console.WriteLine(_Answer);
    }
  }
}