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
        foreach (Component component in components)
            Add(component);

        Hover(0);
    }

    public bool Update(out bool go_back, out string? input)
    {
        Draw();
        HoverCursor(false);

        if (TryReadInput(out input) && input == "Escape")
        {
            go_back = true;
            return true;
        }

        HoverCursor(true);

        go_back = false;
        return true;
    }

    bool TryReadInput(out string? input)
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.Escape:
                input = "Escape";
                return true;

            case ConsoleKey.Enter:
                input = null;
                switch (components[cursor])
                {
                    case InputOption input_option:
                        HoverCursor(true);
                        DrawInputDialogue(input_option);
                        HoverCursor(false);

                        input = input_option.ReadInput();
                        Draw();

                        return input != null;

                    case Option option:
                        input = option.Text;

                        return input != null;
                }

                return false;

            case ConsoleKey.W
            or ConsoleKey.UpArrow:
                if (has_hover)
                    MoveCursor(-1);

                input = null;
                return true;

            case ConsoleKey.S
            or ConsoleKey.DownArrow:
                if (has_hover)
                    MoveCursor(1);

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
        HoverCursor(false);

        if (at >= count)
            for (int i = count - 1; i >= 0; i--)
            {
                if (components[i] is not HoverComponent)
                    continue;

                cursor = i;
                HoverCursor(true);
                return;
            }

        for (int i = at - 1; i >= 0; i--)
        {
            if (components[i] is not HoverComponent)
                continue;

            cursor = i;
            HoverCursor(true);
            return;
        }

        for (int i = at; i < count; i++)
        {
            if (components[i] is not HoverComponent)
                continue;

            cursor = i;
            HoverCursor(true);
            return;
        }
    }

    void HoverCursor(bool value)
    {
        if (components[cursor] is not HoverComponent hover)
            return;

        hover.Hover = value;
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
            cursor = count - 1;
        else if (cursor >= components.Length)
            cursor = 0;

        if (components[cursor] is not HoverComponent)
            MoveCursor(direction);
    }

    void Draw()
    {
        Console.Clear();

        for (int i = 0; i < count; i++)
            Console.WriteLine(components[i]);
    }

    void DrawInputDialogue(InputOption input)
    {
        StringBuilder dialogue = new("Please ");

        if (input is InputTextOption)
            dialogue.Append("enter ");
        else if (input is InputKeyOption)
            dialogue.Append("press ");

        if (input.InputHint is { Length: > 0 })
            dialogue.Append($"{input.InputHint} ");

        if (input is InputTextOption)
            dialogue.Append("(text):");
        else if (input is InputKeyOption)
            dialogue.Append("(key):");

        Add(dialogue.ToString());
        Draw();
        RemoveLast();
    }

    public void Add(Component component)
    {
#if ERR
        ArgumentOutOfRangeException.ThrowIfEqual(components.Length, 0);
#endif
        if (component is HoverComponent)
            has_hover = true;

        if (count == components.Length)
            Array.Resize(ref components, components.Length << 1);

        components[count++] = component;
    }

    void RemoveLast()
    {
#if ERR
        ArgumentOutOfRangeException.ThrowIfLessThan(count, 0);
#endif
        count--;
    }

    public IEnumerator<Component> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
            yield return components[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
