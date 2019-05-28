using Godot;
using System;

public class InstantDungeon : Spatial
{
    [Export]
    public int tileSize { get; set; }
    [Export]
    public int numRooms { get; set; }
    [Export]
    public int minSize { get; set; }
    [Export]
    public int maxSize { get; set; }

    private Random random { get; set; }

    private PackedScene roomData = SkyLogz.GameSystem.Preload("Assets/Components/Props/Rooms/BaseRoom");
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        random = new Random();
        MakeRooms();
    }
    
    private void MakeRooms()
    {
        for (int i = 0; i < numRooms; i++)
        {
            var pos = new Vector3();
            var r = roomData.Instance() as Spatial;
            AddChild(r);


            var w = minSize + random.Next() % (maxSize - minSize);
            var h = minSize + random.Next() % (maxSize - minSize);
            var center = (w * tileSize) / 2;
            var xloc = (w * tileSize) - center;
            pos.Set(xloc, 0, 0);
            var trans = r.GetTransform();
            trans.origin = pos;
            r.SetTransform(trans);
            //r.MakeRoom(pos, new Vector3(w, h, w) * tileSize);
        }
    }
}
