namespace FileBackupTool.Menus.Components;

public abstract class InputOption(string? text = null, string? input_hint = null) : Option(text)
{
	public string? InputHint { get; } = input_hint;

	public abstract string? ReadInput();
}
