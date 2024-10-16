namespace FileBackupTool.Menus.Components;

public class TextOption(string? body = null, string? input_hint = null) : InputOption(body, input_hint)
{
	public override string? RequestInput() =>
		Console.ReadLine() ?? base.RequestInput();

	public static implicit operator TextOption(string? body) =>
		new(body);
}
