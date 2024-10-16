namespace FileBackupTool.Menus.Components;

public abstract class InputOption(string? body = null, string? input_hint = null) : Option(body)
{
	public string? InputHint { get; } = input_hint;

	public virtual string? RequestInput() => null;
}
