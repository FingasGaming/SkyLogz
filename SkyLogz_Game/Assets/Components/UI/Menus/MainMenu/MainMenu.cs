using Godot;
using System;

public class MainMenu : Control
{
    //variables
    MenuButton _quit;
    MenuButton _option;
    MenuButton _newgame;

    [Export]
    public string _title; 

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _title = GetNode<Label>("Title").GetText();

        GD.Print("Menu title: "+_title);

        var path = "CenterContainer/VBoxContainer/Buttons";

        foreach (Button item in GetNode<VBoxContainer>(path).GetChildren())
        {
            //item.Connect("pressed", this, "_on_Button_Pressed");

            /*if (item.GetText().Equals("Quit"))
            {
                _quit = item;
            }
            */
        }

        _quit = GetNode<MenuButton>(path + "/Quit");
        _quit.Connect("pressed", this, "_on_Button_Quit_Pressed");

        _option = GetNode<MenuButton>(path + "/Option");
        _option.Connect("pressed", this, "_on_Button_Option_Pressed");

        _newgame = GetNode<MenuButton>(path + "/NewGame");
        _newgame.Connect("pressed", this, "_on_Button_NewGame_Pressed");
    }

    private void _on_Button_Quit_Pressed()
    {
        GetTree().Quit();
    }
    private void _on_Button_Option_Pressed()
    {

    }
    private void _on_Button_NewGame_Pressed()
    {
        GD.Print("_on_Button_NewGame_Pressed");
        //var path = "res://Assets/Levels/" + _newgame.Scene_to_Load + ".tscn";
        //get starting level from file
        var lv = SkyLogz.GameSystem.PreloadLevel(_newgame.Scene_to_Load);

        if (lv.CanInstance())
        {
            var inst = lv.Instance();
            GD.Print("instance created");

            GD.Print("add instance to scene");
            GetParent().GetNode<Spatial>("Loaded").AddChild(inst);

            GD.Print("instance added");
        }
        //GetTree().ChangeSceneTo(lv);
        this.Hide();
    }

    public override void _GuiInput(InputEvent @event)
    {
        SkyLogz.MenuSystem.HandleMenuEvents(@event, _quit, _option, _newgame);
    }
}
