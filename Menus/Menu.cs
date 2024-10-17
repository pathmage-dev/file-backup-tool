using System.Text;
using FileBackupTool.Menus.Components;

namespace FileBackupTool.Menus;

public sealed class Menu
{
	Component[] components;
	int component_count;

	int current;

	readonly bool interactive;

	public Menu(int hovered_option, params Component[] components)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfEqual(components.Length, 0);
		ArgumentOutOfRangeException.ThrowIfLessThan(hovered_option, 0);
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(hovered_option, components.Length);
#endif
		this.components = components;
		component_count = components.Length;

		for (int i = 0; i < components.Length; i++)
		{
			if (components[i] is HoverComponent)
			{
				current = i;
				interactive = true;
				break;
			}
		}

		if (components[hovered_option] is HoverComponent)
			current = hovered_option;

		SetCurrentHover(true);
	}

	public Menu(params Component[] components) : this(0, components) { }

	public bool Process(out bool go_back, out string? input)
	{
		go_back = false;

		Draw();
		SetCurrentHover(false);

		if (TryGetInput(out input) &&
		    input == "Escape")
			go_back = true;

		SetCurrentHover(true);

		return true;
	}

	bool TryGetInput(out string? input)
	{
		input = null;

		switch (Console.ReadKey().Key)
		{
			case ConsoleKey.Escape:
				input = "Escape";
				break;

			case ConsoleKey.Enter:
				switch (components[current])
				{
					case InputOption input_option:
						SetCurrentHover(true);
						DrawInputDialogue(input_option);
						SetCurrentHover(false);

						input = input_option.GetInput();
						break;

					case Option option:
						input = option.Text;
						break;
				}
				break;

			case ConsoleKey.W or ConsoleKey.UpArrow:
				if (interactive)
					MoveCursor(-1);
				break;

			case ConsoleKey.S or ConsoleKey.DownArrow:
				if (interactive)
					MoveCursor(1);
				break;
		}

		return input != null;
	}

	void MoveCursor(int direction)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfLessThan(direction, -1);
		ArgumentOutOfRangeException.ThrowIfEqual(direction, 0);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(direction, 1);
#endif
		current += direction;

		if (current < 0)
			current = component_count - 1;
		else if (current >= components.Length)
			current = 0;

		if (components[current] is not HoverComponent)
			MoveCursor(direction);
	}

	void SetCurrentHover(bool value)
	{
		if (components[current] is HoverComponent hover)
			hover.Hover = value;
	}

	void Draw()
	{
		Console.Clear();

		for (int i = 0; i < component_count; i++)
			Console.WriteLine(components[i]);
	}

	void DrawInputDialogue(InputOption option)
	{
		StringBuilder dialogue = new("Please ");

		if (option is InputTextOption)
			dialogue.Append("enter ");
		else if (option is InputKeyOption)
			dialogue.Append("press ");

		if (option.InputHint is { Length: > 0 })
			dialogue.Append($"{option.InputHint} ");

		if (option is InputTextOption)
			dialogue.Append("(text):");
		else if (option is InputKeyOption)
			dialogue.Append("(key):");

		AppendComponent(dialogue.ToString());
		Draw();
		PopComponent();
	}

	void AppendComponent(Component component)
	{
		if (component_count++ == components.Length)
			Array.Resize(ref components, component_count << 2);

		components[component_count - 1] = component;
	}

	void PopComponent() =>
		component_count--;
}
