namespace FileBackupTool.Menus.Components;

public class InputKeyOption(string? text = null, string? input_hint = null)
	: InputOption(text, input_hint)
{
	public override string? ReadInput()
	{
		ConsoleKeyInfo input = Console.ReadKey();

		if (input.Key == ConsoleKey.Escape)
			return null;

		return input.Key.ToString();
	}

	public static implicit operator InputKeyOption(string? text) => new(text);
}
