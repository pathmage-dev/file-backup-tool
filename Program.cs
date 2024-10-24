using FileBackupTool;
using FileBackupTool.Menus;
using FileBackupTool.Menus.Components;

var main_menu = new Menu() { new Option("Test"), "----------------" };

// Settings.MainMenuPresets
// main_menu.Add("file-backup-tool");

// Settings.MainMenuLastSessionHover
// main_menu.Hover(5);

while (main_menu.Update(out var move_back, out var input))
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
