namespace FileBackupTool.Menus.Components;

public class InputTextOption(string? text = null, string? input_hint = null)
    : InputOption(text, input_hint)
{
    public override string? ReadInput()
    {
        string? input = Console.ReadLine();

        return input == "" ? null : input;
    }

    public static implicit operator InputTextOption(string? text) => new(text);
}
