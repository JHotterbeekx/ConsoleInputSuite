using System;

namespace ConsoleInputSuite.Renderer {
  public class ConsoleRenderer {
    
    private readonly int _StartCursorTop;
    private readonly int _StartCursorLeft;

    public ConsoleRenderer() {
      _StartCursorTop = Console.CursorTop;
      _StartCursorLeft = Console.CursorLeft;
    }

    public void Draw(string[] lines, int? selectLine = null) {
      Console.CursorTop = _StartCursorTop;
      Console.CursorLeft = _StartCursorLeft;

      for (int renderedIndex = 0; renderedIndex < lines.Length; renderedIndex++) {
        if (selectLine.HasValue && renderedIndex == selectLine) {
          Console.BackgroundColor = ConsoleColor.Gray;
          Console.ForegroundColor = ConsoleColor.Black;
        }

        Console.WriteLine(lines[renderedIndex]);
        Console.ResetColor();
      }
    }
  }
}