using System.Collections.Generic;

namespace ConsoleInputSuite.Input {
  public class InputMultiSelectOption {

    public string Text;
    public string Value;
    public List<InputMultiSelectOption> Children;
    public bool Selected;

    public InputMultiSelectOption(string value, string text, List<InputMultiSelectOption> children = null) {
      Text = text;
      Value = value;
      Children = children ?? new List<InputMultiSelectOption>();
      Selected = false;
    }

  }
}