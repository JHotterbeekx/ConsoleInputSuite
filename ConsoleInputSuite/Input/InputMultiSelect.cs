using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleInputSuite.Input.Interface;

namespace ConsoleInputSuite.Input {
  public class InputMultiSelect : IQuestion {

    private readonly string _Question;
    private readonly List<InputMultiSelectOption> _Options;
    private readonly List<InputMultiSelectOptionWrapper> _InternalOptions;

    public InputMultiSelect(string question, List<InputMultiSelectOption> options) {
      _Question = question;
      _Options = options;
      _FlattenOptions(options, ref _InternalOptions);
    }

    public void Ask() {
      _Draw();
      _ReadAnswer();
    }

    private void _FlattenOptions(List<InputMultiSelectOption> options, ref List<InputMultiSelectOptionWrapper> flatOptions, int? parentIndex = null, int level = 0) {
      if (flatOptions == null) flatOptions = new List<InputMultiSelectOptionWrapper>();

      foreach (var option in options) {
        int nextIndex = flatOptions.Any() ? flatOptions.Max(x => x.Index) : 0;

        flatOptions.Add(new InputMultiSelectOptionWrapper {
          Index = nextIndex,
          Option = option,
          ParentIndex = parentIndex,
          Level = level,
          Selected = false
        });

        if (option.Children != null) _FlattenOptions(option.Children, ref flatOptions, nextIndex, level +1);
      }
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
          _InternalOptions[activeIndex].Selected = !_InternalOptions[activeIndex].Selected;
        }

        _Draw(activeIndex);
      }
    }

    private void _Draw(int activeIndex = 0) {
      Console.CursorLeft = 0;
      Console.CursorTop = 0;
      Console.WriteLine(_Question);

      _RenderText(_InternalOptions, activeIndex);
    }

    private void _RenderText(List<InputMultiSelectOptionWrapper> options, int selected) {
      if (options == null) return;

      for (int renderedIndex = 0; renderedIndex < options.Count; renderedIndex++) {
        var option = options[renderedIndex];
        if (renderedIndex == selected) {
          Console.BackgroundColor = ConsoleColor.Gray;
          Console.ForegroundColor = ConsoleColor.Black;
        }


        Console.WriteLine($"{new String(' ', option.Level * 2)}[{(option.Selected ? "X" : " ")}] {option.Option.Text}");
        Console.ResetColor();
      }
    }

    public void ShowAnswer() {
      Console.WriteLine(_Question);
      _Options.Where(x => x.Selected).ToList().ForEach(x => Console.WriteLine($"> {x.Text}"));
    }

    private class InputMultiSelectOptionWrapper {
      public InputMultiSelectOption Option;
      public int Index;
      public int? ParentIndex;
      public int Level;
      public bool Selected;
    }
  }

  
}