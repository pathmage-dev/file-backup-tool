namespace FileBackupTool.Menus.Components;

public class InputKeyOption(string? text = null, string? input_hint = null) : InputOption(text, input_hint)
{
	public override string? GetInput()
	{
		ConsoleKeyInfo input = Console.ReadKey();

		if (input.Key == ConsoleKey.Escape)
			return null;

		return input.Key.ToString();
	}

	public static implicit operator InputKeyOption(string? body) =>
		new(body);
}
