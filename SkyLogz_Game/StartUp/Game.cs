using Godot;
using System;

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
        
    }
    public override void _Input(InputEvent @event)
    {
        SkyLogz.GameSystem.HandleGameEvent(GetTree());
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
  {
  }
}
