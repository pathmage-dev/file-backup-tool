namespace FileBackupTool.Menus.Components;

public class InputTextOption(string? text = null, string? input_hint = null) : InputOption(text, input_hint)
{
	public override string? GetInput()
	{
		string? input = Console.ReadLine();

		if (input == "")
			return null;

		return input;
	}

	public static implicit operator InputTextOption(string? body) =>
		new(body);
}
