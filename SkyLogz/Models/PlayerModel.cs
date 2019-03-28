using Godot;
using System.Collections.Generic;

namespace SkyLogz
{
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
        public int max_slop_angle { get; set; }
    }

    public class Skill : Resource
    {
        public enum SkillType
        {
            Passive,
            Active
        };
        public string skill_name { get; set; }
        public string skill_description { get; set; }
        public int skill_level { get; set; }
        public int max_skill_level { get; set; }
        public int mana_cost { get; set; }
        public int base_damage { get; set; }
        public float success_chance { get; set; }
        public SkillType skill_type { get; set; }
    }
    public class Stats : Resource
    {
        public float health { get; set; }
        public float mana { get; set; }
        public float max_health { get; set; }
        public float max_mana { get; set; }
        public float strength { get; set; }
        public float vitality { get; set; }
        public float dexterity { get; set; }
        public float inteligence { get; set; }
        public float wisdom { get; set; }
        public float luck { get; set; }
        public Dictionary<string, float> modifiers { get; set; }

        public Stats(StartingStats startingStats)
        {
            max_health = startingStats.max_health;
            max_mana = startingStats.max_mana;
            strength = startingStats.strength;
            vitality = startingStats.vitality;
            dexterity = startingStats.dexterity;
            inteligence = startingStats.inteligence;
            wisdom = startingStats.wisdom;
            luck = startingStats.luck;

            modifiers = new Dictionary<string, float>();
        }
        public void heal(float amount)
        {
            health += amount;
            health = Mathf.Max(health, max_health);
        }
        public void take_damage(Hit hit)
        {
            health -= hit.damage;
            health = Mathf.Max(0, health);
        }
        public void addModifier(string id, float value)
        {
            modifiers.Add(id, value);
        }
        public void removeModifier(string id)
        {
            modifiers.Remove(id);
        }
    }
    public class Hit
    {
        public float damage { get; set; }
    }
    public class StartingStats : Resource
    {
        public string job_name { get; set; }
        public string job_description { get; set; }
        public int max_mana { get; set; }
        public int max_health { get; set; }
        public int strength { get; set; }
        public int vitality { get; set; }
        public int dexterity { get; set; }
        public int inteligence { get; set; }
        public int wisdom { get; set; }
        public int luck { get; set; }
    }

    public class Job : Node
    {
        public Stats stats { get; set; }
        public List<Skill> skills { get; set; }
        public StartingStats startingStats { get; set; }

        public Job(StartingStats startingStats, List<Skill> skills)
        {
            this.skills = skills;

            stats = new Stats(startingStats);
        }
    }
}
