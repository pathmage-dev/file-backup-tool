namespace FileBackupTool.Menus.Components;

public class SelectComponent(string? text = null) : HoverComponent(text)
{
	public string? Select() => _text;

	public static implicit operator SelectComponent(string? text) =>
		new(text);
}
