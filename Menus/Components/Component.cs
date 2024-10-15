namespace FileBackupTool.Menus.Components;

public class Component(string? text = null)
{
	protected readonly string? _text = text;

	public override string? ToString() => _text;

	public static implicit operator Component(string? text) =>
		new(text);
}
