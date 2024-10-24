using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileBackupTool.Menus.Components;

namespace FileBackupTool.Menus;

public struct Menu() : IEnumerable<Component>
{
	Component[] components = new Component[10];
	int count;
	int cursor;

	bool has_hover;

	public Menu(params Span<Component> components)
		: this()
	{
		foreach (var component in components)
			Add(component);

		Hover(0);
	}

	public bool Update(out bool go_back, out string? input)
	{
		draw();
		hoverCursor(false);

		if (
			tryReadInput(out input)
			&& input == ConsoleKey.Escape.ToString()
		)
		{
			go_back = true;
			return true;
		}

		hoverCursor(true);

		go_back = false;
		return true;
	}

	bool tryReadInput(out string? input)
	{
		switch (Console.ReadKey().Key)
		{
			case ConsoleKey.Escape:
				input = "Escape";
				return true;

			case ConsoleKey.Enter:
				switch (components[cursor])
				{
					case InputOption input_option:
						hoverCursor(true);
						drawInputDialogue(input_option);
						hoverCursor(false);

						input = input_option.ReadInput();
						draw();

						return input != null;

					case Option option:
						input = option.Text;

						return input != null;
				}

				input = null;
				return false;

			case ConsoleKey.W
			or ConsoleKey.UpArrow:
				if (has_hover)
					moveCursor(-1);

				input = null;
				return true;

			case ConsoleKey.S
			or ConsoleKey.DownArrow:
				if (has_hover)
					moveCursor(1);

				input = null;
				return true;
		}

		input = null;
		return false;
	}

	public void Hover(int at)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfLessThan(at, 0);
#endif
		hoverCursor(false);

		if (at >= count)
			for (var i = count - 1; i >= 0; i--)
			{
				if (components[i] is not HoverComponent)
					continue;

				cursor = i;
				hoverCursor(true);
				return;
			}

		for (var i = at - 1; i >= 0; i--)
		{
			if (components[i] is not HoverComponent)
				continue;

			cursor = i;
			hoverCursor(true);
			return;
		}

		for (var i = at; i < count; i++)
		{
			if (components[i] is not HoverComponent)
				continue;

			cursor = i;
			hoverCursor(true);
			return;
		}
	}

	void hoverCursor(bool value)
	{
		if (components[cursor] is not HoverComponent hover)
			return;

		hover.Hover = value;
	}

	void moveCursor(int direction)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfLessThan(direction, -1);
		ArgumentOutOfRangeException.ThrowIfEqual(direction, 0);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(direction, 1);
#endif
		cursor += direction;

		if (cursor < 0)
			cursor = count - 1;
		else if (cursor >= components.Length)
			cursor = 0;

		if (components[cursor] is not HoverComponent)
			moveCursor(direction);
	}

	void draw()
	{
		Console.Clear();

		for (var i = 0; i < count; i++)
			Console.WriteLine(components[i]);
	}

	void drawInputDialogue(InputOption input)
	{
		var dialogue = new StringBuilder("Please ");

		switch (input)
		{
			case InputTextOption:
				dialogue.Append("enter ");
				break;
			case InputKeyOption:
				dialogue.Append("press ");
				break;
		}

		if (input.InputHint is { Length: > 0 })
			dialogue.Append($"{input.InputHint} ");

		switch (input)
		{
			case InputTextOption:
				dialogue.Append("(text):");
				break;
			case InputKeyOption:
				dialogue.Append("(key):");
				break;
		}

		Add(dialogue.ToString());
		draw();
		removeLast();
	}

	public void Add(Component component)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfEqual(
			components.Length,
			0
		);
#endif

		if (count == components.Length)
			Array.Resize(ref components, components.Length << 1);

		var idx = count++;

		if (!has_hover && component is HoverComponent)
		{
			has_hover = true;
			Hover(idx);
		}

		components[idx] = component;
	}

	void removeLast()
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfLessThan(count, 0);
#endif
		count--;
	}

	public IEnumerator<Component> GetEnumerator()
	{
		for (var i = 0; i < count; i++)
			yield return components[i];
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
