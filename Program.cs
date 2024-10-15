using FileBackupTool;
using FileBackupTool.Menus.Components;

Menu menu = new(
	"Choose number:",
	new HoverComponent("1"),
	"2",
	new HoverComponent("3"),
	new HoverComponent("4"),
	"5",
	new SelectComponent(null),
	new SelectComponent("6"),
	"7"
	);

string? text;
while (menu.Process(out text)) ;
Console.WriteLine(text);

class Menu
{
	readonly string? body;

	readonly Component[] components;
	int cursor;

	public Menu(string? body, int hovered_option, params Component[] components)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfEqual(components.Length, 0);

		ArgumentOutOfRangeException.ThrowIfLessThan(hovered_option, 0);
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(hovered_option, components.Length);
#endif
		this.body = body;
		this.components = components;
		cursor = hovered_option;

		SetHoverAtCursor(true);
	}

	public Menu(string? body, params Component[] components) : this(body, 0, components) { }

	public Menu(params Component[] components) : this(null, components) { }

	void Draw()
	{
		Console.Clear();

		if (body != null)
			Console.WriteLine(body);

		foreach (Component component in components)
			Console.WriteLine(component);
	}

	void MoveCursor(int direction)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfLessThan(direction, -1);
		ArgumentOutOfRangeException.ThrowIfEqual(direction, 0);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(direction, 1);
#endif
		cursor += direction;

		if (cursor < 0)
			cursor = components.Length - 1;
		else if (cursor >= components.Length)
			cursor = 0;

		if (components[cursor] is not HoverComponent)
			MoveCursor(direction);
	}

	void SetHoverAtCursor(bool value)
	{
		if (components[cursor] is HoverComponent hover)
			hover.Hover = value;
	}

	public bool Process(out string? text)
	{
		text = null;

		Draw();
		SetHoverAtCursor(false);

		switch (Console.ReadKey().Key)
		{
			case ConsoleKey.Backspace:
				return false;

			case ConsoleKey.Enter:
				if (components[cursor] is SelectComponent select)
				{
					text = select.Select();

					if (text != null)
						return false;
				}
				break;

			case ConsoleKey.W or ConsoleKey.UpArrow:
				MoveCursor(-1);
				break;

			case ConsoleKey.S or ConsoleKey.DownArrow:
				MoveCursor(1);
				break;
		}

		SetHoverAtCursor(true);

		return true;
	}
}
