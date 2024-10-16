using FileBackupTool.Menus.Components;

namespace FileBackupTool.Menus;

public sealed class Menu
{
	readonly string? header;

	Component?[] components;
	int component_count;

	int cursor;

	public Menu(string? header, int hovered_option, params Component[] components)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfEqual(components.Length, 0);

		ArgumentOutOfRangeException.ThrowIfLessThan(hovered_option, 0);
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(hovered_option, components.Length);
#endif
		this.header = header;
		this.components = components;
		component_count = components.Length;
		cursor = hovered_option;

		SetHoverAtCursor(true);
	}

	public Menu(string? header, params Component[] components) : this(header, 0, components) { }

	public Menu(params Component[] components) : this(null, components) { }

	public void Append(Component component)
	{
		if (component_count++ == components.Length)
			Array.Resize(ref components, component_count << 2);

		components[component_count - 1] = component;
	}

	public void Pop()
	{
		components[--component_count] = null;
	}

	void Draw()
	{
		Console.Clear();

		if (header is not null)
			Console.WriteLine(header);

		for (int i = 0; i < component_count; i++)
			Console.WriteLine(components[i]);
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
			cursor = component_count - 1;
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

	public bool Process(out string? output)
	{
		output = null;

		Draw();
		SetHoverAtCursor(false);

		switch (Console.ReadKey().Key)
		{
			case ConsoleKey.Backspace:
				return false;

			case ConsoleKey.Enter:
				switch (components[cursor])
				{
					case TextOption enter:
						if (enter.InputHint is not null)
							Append($"Please enter {enter.InputHint.ToUpper()}:");
						else
							Append($"Please enter text:");

						Draw();
						output = enter.RequestInput();
						Pop();
						break;

					case KeyOption press:
						if (press.InputHint is not null)
							Append($"Please press {press.InputHint.ToUpper()}:");
						else
							Append($"Please press key:");

						Draw();
						output = press.RequestInput();
						Pop();
						break;

					case InputOption input:
						Append("Please enter text:");
						Draw();
						output = input.RequestInput();
						Pop();
						break;

					case Option option:
						output = option.Body;
						break;
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
