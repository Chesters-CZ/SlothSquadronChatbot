using System.Reflection;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace GenerickýProgramNetestovaný;

public class SlothSquadron
{
    private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        { WriteIndented = true, IncludeFields = true, MaxDepth = int.MaxValue };

    private List<Weapon> knownWeapons = JsonSerializer.Deserialize<List<Weapon>>(File.ReadAllText("../../../Databaze.json"), _serializerOptions);

    public SlothSquadron(){
    
    }

    public String Ask(String query)
    {
        Console.WriteLine();
        query = query.ToLower();
        String output = "";
        
        if (query.Contains("weapon") || query.Contains("alias"))
        {
            output = "Here is a list of all the weapons I know about along with their aliases/nicknames:\n";
            foreach (Weapon weapon in knownWeapons)
            {
                output += weapon.Name + " - ";
                output += Strings.Join(weapon.Aliases, ", ");
                output += "\n";
            }

            return output;
        }
        else if (query.Contains("stats") || query.Contains("attributes") || query.Contains("properties"))
        {
            output = "Here are the properties i know about and what you can call them:\n" +
                     "Price (cost) - How much in-game money the weapon costs to buy\n" +
                     "Kill award (reward) - Reward for killing an enemy with this weapon\n" +
                     "Damage (base damage) - The base damage for every bullet fired before any multipliers are applied\n" +
                     "Bullets - How many bullets are fired per shot\n" +
                     "Armor penetration - Percentage of damage passing through kevlar and helmets.\n" +
                     "Damage falloff (falloff) - Percentage of damage lost at a range of 500 units. The multiplier is applied as falloff^(distance/500)\n" +
                     "Headshot multiplier (multiplier) - Multiplier applied when the bullet collides with the head hitbox.\n" +
                     "Firerate (rpm) - Maximum achievable firerate in rounds per minute\n" +
                     "Penetration power - Value determining how thick of a wall the bullet can pass through.\n" +
                     "Magazine/clip size (mag size)- How many rounds can be fired before needing to reload\n" +
                     "Ammo in reserve (reserve ammo)- How many rounds in total can be fired by a weapon. This number does not include the magazine/clip the weapon comes preloaded with.\n" +
                     "Mobility (runspeed, speed) - Maximum running speed in units per second when wielding the weapon.\n" +
                     "Tagging power (tag) - Value used to determine the slowdown inflicted by a hit with this weapon.\n" +
                     "Bullet range - Maximum distance in units a bullet fired by this weapon travels\n" +
                     "Hold to shoot (hts) - Whether the weapons continues to shoot when the fire button is held down\n" +
                     "Tracers - Frequency of shooting a bullet with a visible path\n" +
                     "Accurate range stand (standing accuracy) - Maximum distance in meters from which the first bullet fired when standing is sure to hit a 30cm dinnerplate.\n" +
                     "Accurate range crouch (crouching accuracy) - Maximum distance in meters from which the first bullet fired when crouching is sure to hit a 30cm dinnerplate.\n" +
                     "Standing inaccuracy - Base inaccuracy of the first round when fired from a standstill.\n" +
                     "Crouching inaccuracy - Base inaccuracy of the first round when fired while crouching.\n" +
                     "Running inaccuracy - Base inaccuracy of the first round when fired while running at the maximum speed for that weapon\n" +
                     "Ladder inaccuracy - Base inaccuracy of the first round when fired while on a ladder.\n" +
                     "Inaccuracy at jump apex - Base inaccuracy of the first round when fired at the peak of a jump.\n" +
                     "Inaccuracy after landing (landing inaccuracy) - Base inaccuracy of the first round when fired 0.33s after landing a jump.\n" +
                     "Inaccuracy from firing (firing inaccuracy) - How inaccurate each successive shot becomes.\n" +
                     "Recovery time crouch (crouching recovery) - How long it takes for Inaccuracy from firing to expire when in a crouched position.\n" +
                     "Recovery time stand (standing recovery) - How long it takes for Inaccuracy from firing to expire when in a standing position.\n" +
                     "Recoil amount - Amount of kickback from each shot.\n" +
                     "Recoil angle variance - Amount of horizontal recoil (left and right).\n" +
                     "Recoil amount variance - Maximum amount the recoil values can deviate by when the recoil pattern is random.\n" +
                     "Is recoil pattern random (recoil pattern) - If true, the recoil is random. When false, spraying with the weapon will land each of the bullets in the same place when not counting inaccuracy.\n" +
                     "Fatal headshot range (headshot range) - Maximum distance in units from which a single bullet fired deals 100 damage or more. A value of -1 means a single headshot will never kill.\n" +
                     "Fatal headshot range helmet (helmet range) - Maximum distance in units from which a single bullet fired deals 100 damage or more to a helmeted enemy. A value of -1 means a single headshot from this weapon will never kill a helmeted enemy.\n" +
                     "Weapon type - Whether the weapon is a pistol, shotgun, SMG, LMG, assault rifle or a sniper rifle.\n" +
                     "Scoped (scope) - Can the weapon have a scope? If yes, are the current values for when scoped or unscoped?\n" +
                     "Silencer on (silencer) - Does the weapon come with a detachable silencer? If yes, are the current values for when it is attached or detached?\n" +
                     "Burst fire (burst) - Can the weapon fire a quick burst of three bullets? If yes, are the current values for when firing in bursts or not?\n" +
                     "Rapd fire (secondary fire) - Does the weapon offer a secondary fire mode with no prime time? If yes, are the current values for the primary or secondary fire?\n" +
                     "Purchasable by (team) - Which teams can purchase this weapon. Note that some weapons share a slot and only one of them can be chosen to be purchasable during a match.\n";
            return output;
        }

        // Todo: logic

        List<String> weapons = DetectWeapon(query);
        List<PropertyInfo> attributes = DetectAttribute(query);
        return "I'm sorry, my responses are limited. You must ask the right questions.";
    }

    private List<String> DetectWeapon(String query)
    {
        List<String> weapons = new List<string>();

        foreach (Weapon weapon in knownWeapons)
        {
            foreach (string alias in weapon.Aliases)
            {
                if (query.Contains(alias))
                {
                    weapons.Add(weapon.Name);
                    break;
                }
            }
        }

        return weapons;
    }

    private List<PropertyInfo> DetectAttribute(string query)
    {
        List<PropertyInfo> attributes = new List<PropertyInfo>();

        foreach (PropertyInfo property in typeof(Weapon).GetProperties())
        {
            if (query.Contains(property.Name))
            {
                attributes.Add(property);
                break;
            }
        }

        return attributes;
    }
};