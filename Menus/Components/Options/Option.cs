namespace FileBackupTool.Menus.Components;

public class Option(string? text = null) : HoverComponent(text)
{
	public string? Text => _text;

	public static implicit operator Option(string? text) => new(text);
}
