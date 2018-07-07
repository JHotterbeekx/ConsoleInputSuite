using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleInputSuite.Input.MultiSelect {
  public class InputMultiSelect {

    private readonly string _Question;
    private readonly List<InputMultiSelectOptionWrapper> _Options;
    private readonly InputMultiSelectSettings _Settings;

    public InputMultiSelect(string question, List<InputMultiSelectOption> options, InputMultiSelectSettings settings = null) {
      _Settings = settings ?? new InputMultiSelectSettings();
      _Question = question;
      _FlattenOptions(options, ref _Options);
    }

    public IEnumerable<dynamic> Ask() {
      _Draw();
      _ReadAnswer();
      return _GetSelected();
    }

    private IEnumerable<dynamic> _GetSelected() {
      IEnumerable<InputMultiSelectOptionWrapper> optionsToReturn = _Options.Where(i => i.Selected != InputMultiSelectToggleState.Off);

      if (!_Settings.ReturnIndeterminateParentValues) optionsToReturn = optionsToReturn.Where(i => i.Selected == InputMultiSelectToggleState.On);
      if (!_Settings.ReturnOnlySelectedChildValues) optionsToReturn = optionsToReturn.Where(i => !i.IsParent);

      return optionsToReturn.Select(x => x.Option.Value);
    }

    private void _FlattenOptions(List<InputMultiSelectOption> options, ref List<InputMultiSelectOptionWrapper> flatOptions, int? parentIndex = null, int level = 0) {
      if (flatOptions == null) flatOptions = new List<InputMultiSelectOptionWrapper>();

      foreach (var option in options) {
        int nextIndex = flatOptions.Any() ? flatOptions.Max(x => x.Index) + 1: 0;

        flatOptions.Add(new InputMultiSelectOptionWrapper {
          Index = nextIndex,
          Option = option,
          ParentIndex = parentIndex,
          Level = level,
          IsParent = option.Children != null && option.Children.Any(),
          Selected = InputMultiSelectToggleState.Off
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
          activeIndex = Math.Min(_Options.Count - 1, activeIndex + 1);
        }

        if (inputCharacter.Key == ConsoleKey.Spacebar) {
          _ToggleIndex(activeIndex);
        }

        _Draw(activeIndex);
      }
    }

    private void _ToggleIndex(int index, InputMultiSelectToggleState? forceSelected = null) {
      var option = _Options[index];
      var newSelectedValue = forceSelected ?? _GetSelectedToggleValue(option.Selected);

      option.Selected = newSelectedValue;
      foreach (var child in _Options.Where(i => i.ParentIndex == index)) {
        child.Selected = newSelectedValue;
        _ToggleIndex(child.Index, newSelectedValue);
      }

      if (!forceSelected.HasValue) {
        int? parentIndex = option.ParentIndex;

        while (parentIndex.HasValue) {
          var siblings = _Options.Where(i => i.ParentIndex == parentIndex).ToList();
          var parent = _Options.FirstOrDefault(i => i.Index == parentIndex);

          if (siblings.All(x => x.Selected == InputMultiSelectToggleState.On)) {
            parent.Selected = InputMultiSelectToggleState.On;
          }
          else if (siblings.All(x => x.Selected == InputMultiSelectToggleState.Off)) {
            parent.Selected = InputMultiSelectToggleState.Off;
          }
          else {
            parent.Selected = InputMultiSelectToggleState.Indeterminate;
          }

          parentIndex = parent.ParentIndex;
        }
      }
    }

    private void _Draw(int activeIndex = 0) {
      Console.CursorLeft = 0;
      Console.CursorTop = 0;
      Console.WriteLine(_Question);

      _RenderText(_Options, activeIndex);
    }

    private void _RenderText(List<InputMultiSelectOptionWrapper> options, int selected) {
      if (options == null) return;

      for (int renderedIndex = 0; renderedIndex < options.Count; renderedIndex++) {
        var option = options[renderedIndex];
        if (renderedIndex == selected) {
          Console.BackgroundColor = ConsoleColor.Gray;
          Console.ForegroundColor = ConsoleColor.Black;
        }


        Console.WriteLine($"{new String(' ', option.Level * 2)}[{_ShowToggleState(option.Selected)}] {option.Option.Text}");
        Console.ResetColor();
      }
    }

    private string _ShowToggleState(InputMultiSelectToggleState toggleState) {
      switch (toggleState) {
        case InputMultiSelectToggleState.On:
          return "X";
        case InputMultiSelectToggleState.Indeterminate:
          return "-";
        default:
          return " ";
      }
    }

    private InputMultiSelectToggleState _GetSelectedToggleValue(InputMultiSelectToggleState toggleState) {
      switch (toggleState) {
        case InputMultiSelectToggleState.On:
          return InputMultiSelectToggleState.Off;
        default:
          return InputMultiSelectToggleState.On;
      }
    }

    private class InputMultiSelectOptionWrapper {
      public InputMultiSelectOption Option;
      public int Index;
      public int? ParentIndex;
      public bool IsParent;
      public int Level;
      public InputMultiSelectToggleState Selected;
    }

    private enum InputMultiSelectToggleState {
      On,
      Off,
      Indeterminate
    }
  }

  
}