using Godot;
using System;
using System.Collections.Generic;

public class Procedural_Level_Test : Spatial
{
    [Export]
    public int LevelGridSize;
    [Export]
    public int TileSize;
    [Export]
    public string[] ProceduralItemNames;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Random random = new Random();



        for (int x = 0; x < LevelGridSize; x++)
        {
            for (int y = 0; y < LevelGridSize; y++)
            {
                //get random index
                var index = random.Next(ProceduralItemNames.Length);
                //get level name
                var name = ProceduralItemNames[index];

                var level = SkyLogz.GameSystem.PreloadProcedural(name);
                //create instance of level
                var instance = level.Instance() as Spatial;

                AddChild(instance);
                //calculate center
                float center = (LevelGridSize * TileSize) / 2;
                //calculate x and z locations
                float xloc = (x * TileSize) - center;
                float zloc = (y * TileSize) - center;

                //set transform
                instance.GetTransform().Translated(new Vector3(xloc, 0, zloc));
                instance.GetTransform().Rotated(Vector3.Up, random.Next(4) * 90);
            }
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
