using System.Collections.Generic;

namespace ConsoleInputSuite.Input.MultiSelect {
  public class InputMultiSelectOption {

    public string Text;
    public dynamic Value;
    public List<InputMultiSelectOption> Children;
    public bool Selected;

    public InputMultiSelectOption(dynamic value, string text, List<InputMultiSelectOption> children = null) {
      Text = text;
      Value = value;
      Children = children ?? new List<InputMultiSelectOption>();
      Selected = false;
    }

  }
}