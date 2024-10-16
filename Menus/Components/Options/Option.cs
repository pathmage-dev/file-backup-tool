namespace FileBackupTool.Menus.Components;

public class Option(string? body = null) : HoverComponent(body)
{
	public string? Body => _body;

	public static implicit operator Option(string? body) =>
		new(body);
}
