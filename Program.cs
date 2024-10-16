using FileBackupTool;
using FileBackupTool.Menus;
using FileBackupTool.Menus.Components;

Menu menu = new(
	"Choose number:",
	new HoverComponent("1"),
	"2",
	new HoverComponent("3"),
	new HoverComponent("4"),
	"5",
	new Option(null),
	new Option("6"),
	new TextOption("ent"),
	new KeyOption("press"),
	"7"
	);

// Menu menu = new(null, new SelectComponent("1"));
//
// menu.Append("2");
// menu.Append("3");

// menu.Pop();

while (menu.Process(out string? output))
{
	if (output is not null)
	{
		Console.WriteLine(output);
		if (output == ConsoleKey.Backspace.ToString())
			Console.WriteLine("Back");
		break;
	}
}
