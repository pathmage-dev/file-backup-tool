using FileBackupTool;
using FileBackupTool.Menus;
using FileBackupTool.Menus.Components;

Menu main_menu = ["---", new Option("Test"), "---", new HoverComponent("1"), "fdfdddia"];

// Settings.MainMenuPresets
// main_menu.Add("file-backup-tool");

// Settings.MainMenuLastSessionHover
// main_menu.Hover(5);

while (main_menu.Update(out bool move_back, out string? input))
{
	if (move_back)
	{
		Console.Write("aaaa");
		return;
	}

	if (input != null)
	{
		Console.WriteLine(input);
		break;
	}
}
