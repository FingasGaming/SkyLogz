using Godot;
using SkyLogz;
using System;

public class PlayerCharacter : KinematicBody
{
    private Camera playerCamera { get; set; }
    private Spatial head { get; set; }

    private PlayerModel playerModel;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        head = GetNode("Head") as Spatial;
        //playerCamera = GetNode("/Head/Camera") as Camera;

        playerModel = new PlayerModel {
            camera_angle = 0,
            mouse_sensitivity = 0.3f,
            fly_speed = 20,
            fly_accel = 2,
            flying = false,
            gravity = -9.8f * 3,
            max_speed = 20,
            max_running_speed = 30,
            max_accel = 2,
            max_decel = 6,
            leg_strength = 15,
            in_air = 0,
            has_contact = false,
            //max_slop_angle = 35
        };
        
    }

    public override void _Input(InputEvent @event)
    {
        MovementSystem.HandlePlayerCamera(@event, playerModel, head);
    }

    public override void _PhysicsProcess(float delta)
    {
        MovementSystem.MovePlayer(this as KinematicBody, playerModel, head, delta);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
        //  }
    private void _on_Area_body_entered(object body)
    {
        if (body is PlayerCharacter)
        {
            this.playerModel.flying = true;
        }
    }
    private void _on_Area_body_exited(object body)
    {
        if (body is PlayerCharacter)
        {
            this.playerModel.flying = false;
        }
    }
}




