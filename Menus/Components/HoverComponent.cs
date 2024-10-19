namespace FileBackupTool.Menus.Components;

public class HoverComponent(string? text = null) : Component(text)
{
    public bool Hover { private get; set; }

    public override string? ToString() => _text != null && Hover ? $" {_text}" : base.ToString();

    public static implicit operator HoverComponent(string? text) => new(text);
}
