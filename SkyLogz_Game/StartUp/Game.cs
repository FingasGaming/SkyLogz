using Godot;
using System;
using SkyLogz;

public class Game : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("SkyLogz Game Launch");
        SkyLogz.GameSystem.RunTest();
        SkyLogz.GameSystem.GameReady();

        //var scene = GameSystem.Preload("res://Assets/Levels/Level_1.tscn");
        //var instance = scene.Instance();

        //GetNode("Loaded").AddChild(instance);
        //Extension.Preload(this, "");
    }
    public override void _Input(InputEvent @event)
    {
        SkyLogz.GameSystem.HandleGameEvent(this);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
    }
}
