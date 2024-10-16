namespace FileBackupTool.Menus.Components;

public class KeyOption(string? body = null, string? input_hint = null) : InputOption(body, input_hint)
{
	public override string? RequestInput() =>
		Console.ReadKey().Key.ToString();

	public static implicit operator KeyOption(string? body) =>
		new(body);
}
