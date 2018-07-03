using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleInputSuite.Input.Interface;

namespace ConsoleInputSuite.Input {
  public class InputMultiSelect : IQuestion {

    private readonly string _Question;
    private readonly List<InputMultiSelectOption> _Options;

    public InputMultiSelect(string question, List<InputMultiSelectOption> options) {
      _Question = question;
      _Options = options;
    }

    public void Ask() {
      _Draw();
      _ReadAnswer();
    }

    private void _ReadAnswer() {
      var activeIndex = 0;

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
          activeIndex = Math.Min(_Options.Count + _Options.SelectMany(x => x.Children).Count() - 1, activeIndex + 1);
        }

        if (inputCharacter.Key == ConsoleKey.Spacebar) {
          _Options[activeIndex].Selected = !_Options[activeIndex].Selected;
        }

        _Draw(activeIndex);
      }
    }

    private void _Draw(int activeIndex = 0) {
      Console.CursorLeft = 0;
      Console.CursorTop = 0;
      Console.WriteLine(_Question);

      _RenderText(_Options, activeIndex);
    }

    private void _RenderText(List<InputMultiSelectOption> options, int selected = 0, int level = 0, int renderedIndex = 0) {
      for (int i = 0; i < options.Count; i++) {
        if (renderedIndex == selected) {
          Console.BackgroundColor = ConsoleColor.Gray;
          Console.ForegroundColor = ConsoleColor.Black;
        }

        Console.WriteLine($"{new String(' ', level * 2)}[{(options[i].Selected ? "X" : " ")}] {options[i].Text}");
        Console.ResetColor();
        renderedIndex++;

        if (options[i].Children.Any()) {
          _RenderText(options[i].Children, selected, level + 1, renderedIndex);
          renderedIndex += options[i].Children.Count;
        } 
      }
    }

    public void ShowAnswer() {
      Console.WriteLine(_Question);
      _Options.Where(x => x.Selected).ToList().ForEach(x => Console.WriteLine($"> {x.Text}"));
    }
  }
}