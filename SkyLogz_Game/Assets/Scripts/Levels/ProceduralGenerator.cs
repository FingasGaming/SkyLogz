using Godot;

public class ProceduralGenerator : Spatial
{
    [Export]
    public int LevelGridSize;
    [Export]
    public int TileSize;
    [Export]
    public int NumRooms;
    [Export]
    public int MinSize;
    [Export]
    public int MaxSize;
    [Export]
    public string[] ProceduralItemNames;

    public void MakeRoom(Vector3 position, int size)
    {
        //room area
        AABB room = new AABB(position, new Vector3((float)size, (float)size, (float)size));

        
    }
}
