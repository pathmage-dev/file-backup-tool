using FileBackupTool;
using FileBackupTool.Menus;
using FileBackupTool.Menus.Components;


Menu example = new(4,
	"Choose number:",
	"w",
	new HoverComponent("1"),
	"2",
	new HoverComponent("3"),
	new HoverComponent("4"),
	// "5",
	// new Option(),
	new Option("6"),
	new InputTextOption("text"),
	new InputKeyOption("key"),
	"7"
	);

while (example.Process(out bool move_back, out string? input))
{
	if (move_back) return;

	if (input != null)
	{
		Console.WriteLine(input);
		break;
	}
}
