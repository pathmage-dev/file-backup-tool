using System.Collections.Generic;
using System.IO;
using FileBackupTool;
using FileBackupTool.Menus;
using FileBackupTool.Menus.Components;

Menu main_menu = new("a", "b", new HoverComponent("c"), new HoverComponent("d"));
	// "---",
	// new Option("Test"),
	// "---",
	// new HoverComponent("1"),
	// new InputTextOption("text"),
	// new InputKeyOption("key"),
	// "end",
// ];

main_menu.Add("A");

// Settings.MainMenuPresets
// main_menu.Add("file-backup-tool");

// Settings.MainMenuLastSessionHover
// main_menu.Hover(5);

while (main_menu.Update(out bool move_back, out string? input))
{
	if (move_back) return;

	if (input != null)
	{
		Console.WriteLine(input);
		break;
	}
}
