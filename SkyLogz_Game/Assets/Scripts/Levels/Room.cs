using Godot;
using System;

public class Room : Spatial
{
    [Export]
    public int RoomSize { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<BoxShape>("Area/Shape").Extents = new Vector3(RoomSize, RoomSize, RoomSize);
    }
    public void MakeRoom(Vector3 position, Vector3 size)
    {
        //set position
        var transform = GetTransform();
        transform.origin = position;
        SetTransform(transform);
        //room area
        //GetNode<BoxShape>("Area/Shape").SetExtents(size);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
