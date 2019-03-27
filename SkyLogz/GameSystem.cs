using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyLogz
{
    public class GameSystem
    {
        public static void GameReady()
        {
            Input.SetMouseMode(Input.MouseMode.Captured);
            GD.Print("Mouse mode captured");
        }
        public static void HandleGameEvent(SceneTree tree)
        {
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                Input.SetMouseMode(Input.MouseMode.Visible);
                tree.Quit();
            }
            if (Input.IsActionJustPressed("restart_game"))
            {
                tree.ReloadCurrentScene();
            }
        }
        public static void RunTest()
        {
            GD.Print("From SkyLogz lib");
        }
    }
    public class PlayerModel
    {
        //Camera
        public float camera_angle { get; set; }
        public float mouse_sensitivity { get; set; }
        public Vector2 camera_change { get; set; }
        //Movement
        public Vector3 velocity { get; set; }
        public Vector3 direction { get; set; }
        //flying
        public int fly_speed { get; set; }
        public int fly_accel { get; set; }
        public bool flying { get; set; }
        //walking
        public float gravity { get; set; }
        public int max_speed { get; set; }
        public int max_running_speed { get; set; }
        public int max_accel { get; set; }
        public int max_decel { get; set; }
        //jumping
        public int leg_strength { get; set; }
        public int in_air { get; set; }
        public bool has_contact { get; set; }
    }
    public static class MovementSystem
    {
        private static void Aim(PlayerModel model, Spatial head)
        {
            if (head != null && model.camera_change.Length() > 0)
            {
                //yaw
                head.RotateY(Mathf.Deg2Rad(-model.camera_change.x * model.mouse_sensitivity));
                //pitch 
                var change = -model.camera_change.y;
                if (change + model.camera_angle < 90 && change + model.camera_angle > -90)
                {
                    var camera = head.GetNode("Camera") as Camera;
                    camera.RotateX(Mathf.Deg2Rad(change));

                    model.camera_angle += change;
                }
            }
            model.camera_change = new Vector2();
        }
        public static void HandlePlayerCamera(InputEvent @event, PlayerModel model, Spatial head)
        {
            
            if (@event is InputEventMouseMotion)
            {
                model.camera_change = (@event as InputEventMouseMotion).Relative;                
            }
        }
        public static void MovePlayer(KinematicBody body, PlayerModel model, Spatial head, float delta)
        {
            Aim(model, head);
            if (model.flying)
            {
                Fly(body, model, head, delta);
            }
            else
                Walk(body, model, head, delta);
        }

        private static void Fly(KinematicBody body, PlayerModel model, Spatial head, float delta)
        {

            //reset plauer direction
            model.direction = new Vector3();

            //get rotation of camera
            var aim = (head.GetNode("Camera") as Camera).GetGlobalTransform().basis;

            //handle inputs and set direction
            if (Input.IsActionPressed("move_forward"))
            {
                model.direction -= aim.z;
            }
            if (Input.IsActionPressed("move_backward"))
            {
                model.direction += aim.z;
            }
            if (Input.IsActionPressed("move_left"))
            {
                model.direction -= aim.x;
            }
            if (Input.IsActionPressed("move_right"))
            {
                model.direction += aim.x;
            }

            //normalize direction for constant speeed
            model.direction = model.direction.Normalized();

            //player at max speed
            var target = model.direction * model.fly_speed;
            //calculate distance to go
            model.velocity = model.velocity.LinearInterpolate(target, model.fly_accel * delta);

            //move
            body.MoveAndSlide(model.velocity);
        }
        private static void Walk(KinematicBody body, PlayerModel model, Spatial head, float delta)
        {

            //reset plauer direction
            model.direction = new Vector3();

            //get rotation of camera
            var aim = (head.GetNode("Camera") as Camera).GetGlobalTransform().basis;

            //handle inputs and set direction
            if (Input.IsActionPressed("move_forward"))
            {
                model.direction -= aim.z;
            }
            if (Input.IsActionPressed("move_backward"))
            {
                model.direction += aim.z;
            }
            if (Input.IsActionPressed("move_left"))
            {
                model.direction -= aim.x;
            }
            if (Input.IsActionPressed("move_right"))
            {
                model.direction += aim.x;
            }

            //normalize direction for constant speeed
            model.direction = model.direction.Normalized();

            //test if on floor
            if (body.IsOnFloor())
            {
                model.has_contact = true;
            }
            else
            {
                var foot = body.GetNode("Foot") as RayCast;
                if (foot != null && foot.IsColliding())
                {
                    model.has_contact = false;
                }
            }

            if (model.has_contact && !body.IsOnFloor())
            {
                body.MoveAndCollide(new Vector3(0, -1, 0));
            }

            var velocity = model.velocity;
            var y = velocity.y;
            velocity.y = 0;
            var speed = 0;
            if (Input.IsActionPressed("move_sprint"))
            {
                speed = model.max_running_speed;
            }
            else
            {
                speed = model.max_speed;
            }
            //player at max speed
            var target = model.direction * speed;

            var accel = 0;
            if (model.direction.Dot(velocity) > 0)
            {
                accel = model.max_accel;
            }
            else
            {
                accel = model.max_decel;
            }

            //calculate distance to go
            velocity = velocity.LinearInterpolate(target, accel * delta);

            //deal with gravity
            velocity.Set(velocity.x, y += model.gravity * delta, velocity.z);


            if (Input.IsActionJustPressed("jump") && model.has_contact)
            {
                velocity.y = model.leg_strength;
                model.has_contact = false;
            }

            if (!body.IsOnFloor())
            {
                model.in_air += 1;
            }

            //move
            velocity = body.MoveAndSlide(velocity, new Vector3(0,1,0), true);
            model.velocity = velocity;
        }
    }
}
