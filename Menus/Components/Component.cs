namespace FileBackupTool.Menus.Components;

public class Component(string? body = null)
{
	protected readonly string? _body = body;

	public override string? ToString() => _body;

	public static implicit operator Component(string? body) =>
		new(body);
}
