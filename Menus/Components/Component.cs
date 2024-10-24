namespace FileBackupTool.Menus.Components;

public class Component(string? text = null)
{
	protected string? _text { get; } = text;

	public override string? ToString()
	{
		if (_text == "")
			return null;

		return _text;
	}

	public static implicit operator Component(string? text) => new(text);
}
