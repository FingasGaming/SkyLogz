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
            //Input.SetMouseMode(Input.MouseMode.Captured);
        }
        public static void HandleGameEvent(Node root)
        {
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                if (Input.GetMouseMode().Equals(Input.MouseMode.Visible))
                {
                    if (root.GetNode<Control>("MainMenu").IsVisible())
                    {
                        root.GetNode<Control>("MainMenu").Hide();
                    }
                    Input.SetMouseMode(Input.MouseMode.Captured);
                }
                else
                {
                    if (!root.GetNode<Control>("MainMenu").IsVisible())
                    {
                        root.GetNode<Control>("MainMenu").Show();
                    }
                    Input.SetMouseMode(Input.MouseMode.Visible);
                }
            }

            if (Input.GetMouseMode().Equals(Input.MouseMode.Captured))
            {

                if (Input.IsActionJustPressed("restart_game"))
                {
                    root.GetTree().ReloadCurrentScene();
                }
                if (Input.IsActionJustPressed("next_map"))
                {
                    GD.Print("next_map");
                }
                if (Input.IsActionJustPressed("loadmap"))
                {
                    GD.Print("Load map");
                    if (root.HasNode("Loaded"))
                    {
                        GD.Print("Has Loaded Node");
                        var loaded = root.GetNode("Loaded") as Spatial;
                        if (loaded.GetChildren().ToList().Count <= 0)
                        {
                            GD.Print("Has Loaded Node children is 0");
                            var scene = PreloadLevel("Relm");
                            GD.Print("Has Loaded Node preload scene");
                            if (scene == null)
                            {

                                GD.Print("scene is null");
                                return;
                            }
                            if (scene.CanInstance())
                            {
                                GD.Print("Can instance");
                                var instance = scene.Instance();
                                GD.Print("Has Loaded Node make instance of scene");
                                loaded.AddChild(instance);
                                GD.Print("Has Loaded Node add instance to scene");
                            }
                        }
                        else
                        {
                            loaded.GetNode<Spatial>("Relm").SetVisible(false);
                            GD.Print("Has Loaded Node set instance visibility to hide");
                        }
                    }
                    else
                    {
                        GD.Print("failed to find Loaded node");
                    }
                }
            }
        }
        public static void HandleMenuEvents(Control menu)
        {
            //var path = "CenterContainer/VBoxContainer/VBoxContainer/";
            //if (menu != null)
            //{
            //    var _quit = menu.GetNode<Button>(path + "Quit");

            //    if (_quit.IsPressed())
            //    {
            //        menu.GetTree().Quit();
            //    }
            //}
        }
        public static void HandleGameEvent(SceneTree tree)
        {
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                Input.SetMouseMode(Input.MouseMode.Visible);
            }
            if (Input.IsActionJustPressed("restart_game"))
            {
                tree.ReloadCurrentScene();
            }
            if (Input.IsActionJustPressed("next_map"))
            {
            }
        }
        public static void RunTest()
        {
            GD.Print("From SkyLogz lib");
        }

        public static PackedScene Preload(string filepath)
        {
            var scene = GD.Load<PackedScene>("res://"+filepath+".tscn"); // Will load when the script is instanced.

            return scene;
        }
        public static PackedScene PreloadLevel(string filename)
        {
            var scene = GD.Load<PackedScene>("res://Assets/Levels/" + filename + ".tscn"); // Will load when the script is instanced.

            return scene;
        }
        public static PackedScene PreloadProcedural(string filename)
        {
            var scene = GD.Load<PackedScene>("res://Assets/Components/Props/Procedural/" + filename + ".tscn"); // Will load when the script is instanced.

            return scene;
        }
    }

    public static class Extension
    {
        public static PackedScene Preload(this Spatial root, string filename = "")
        {
            var scene = GD.Load<PackedScene>(filename); // Will load when the script is instanced.

            return scene;
        }
    }
}
