namespace FileBackupTool.Menus.Components;

public class HoverComponent(string? body = null) : Component(body)
{
	public bool Hover { private get; set; }

	public override string? ToString()
	{
		if (Hover)
			return $" {_body}";

		return base.ToString();
	}

	public static implicit operator HoverComponent(string? body) =>
		new(body);
}
