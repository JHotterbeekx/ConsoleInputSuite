using System;
using System.Collections.Generic;
using ConsoleInputSuite.Input.Interface;

namespace ConsoleInputSuite.Input {
  public class InputMultiSelect : IQuestion {

    private readonly string _Question;
    private readonly List<string> _Options = new List<string>();
    private List<int> _AnswerIndexes = new List<int>();

    public InputMultiSelect(string question, List<string> options) {
      _Question = question;
      _Options = options;
    }

    public void Ask() {
      _RenderText();
      _ReadAnswer();
    }

    private void _ReadAnswer() {
      var activeIndex = 0;
      _AnswerIndexes = new List<int>();

      while (true) {
        var inputCharacter = Console.ReadKey(true);

        if (inputCharacter.Key == ConsoleKey.Enter) {
          Console.WriteLine();
          return;
        }

        if (inputCharacter.Key == ConsoleKey.UpArrow) {
          activeIndex = Math.Max(0, activeIndex - 1);
        }

        if (inputCharacter.Key == ConsoleKey.DownArrow) {
          activeIndex = Math.Min(_Options.Count - 1, activeIndex + 1);
        }

        if (inputCharacter.Key == ConsoleKey.Spacebar) {
          if (_AnswerIndexes.Contains(activeIndex)) _AnswerIndexes.Remove(activeIndex);
          _AnswerIndexes.Add(activeIndex);
        }


        _RenderText(activeIndex);
      }
    }

    private void _RenderText(int selected = 0) {
      Console.CursorLeft = 0;
      Console.CursorTop = 0;
      Console.WriteLine(_Question);
      for (int i = 0; i < _Options.Count; i++) {
        if (i == selected) {
          Console.BackgroundColor = ConsoleColor.Gray;
          Console.ForegroundColor = ConsoleColor.Black;
        }

        Console.WriteLine($"[{(_AnswerIndexes.Contains(i) ? "X" : " ")}] {_Options[i]}");

        Console.ResetColor();
      }
    }

    public void ShowAnswer() {
      Console.WriteLine(_Question);
      for (int i = 0; i < _Options.Count; i++) {
        if (!_AnswerIndexes.Contains(i)) continue;
        Console.WriteLine($"> {_Options[i]}");
      }
    }
  }
}