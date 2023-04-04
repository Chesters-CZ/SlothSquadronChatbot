using System.Reflection;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace GenerickýProgramNetestovaný;

public class SlothSquadron
{
    private JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        { WriteIndented = true, IncludeFields = true, MaxDepth = int.MaxValue };

    private List<Weapon> knownWeapons = JsonSerializer.Deserialize<List<Weapon>>(File.ReadAllText("../../../Databaze.json"));

    public SlothSquadron()
    {
    }

    public String Ask(String query)
    {
        query = query.ToLower();
        String output = "";
        if (query.Contains("weapon") || query.Contains("alias"))
        {
            output = "Here is a list of all the weapons I know about along with their aliases/nicknames:";
            foreach (Weapon weapon in knownWeapons)
            {
                output += weapon.Name + " - ";
                output += Strings.Join(weapon.Aliases, ", ");
            }

            return output;
        }
        else if (query.Contains("stats") || query.Contains("attributes") || query.Contains("properties"))
        {
            output = "Here is what I know about the weapons:\n" +
                     "Price - How much in-game money the weapon costs to buy\n" +
                     "Kill award - Reward for killing an enemy with this weapon\n" +
                     "Damage - The base damage for every bullet fired before any multipliers are applied\n" +
                     "Bullets - How many bullets are fired per shot\n" +
                     "Armor penetration - Percentage of damage passing through kevlar and helmets.\n" +
                     "Damage falloff @ 500U - Percentage of damage lost at a range of 500 units. The multiplier is applied as falloff^(distance/500)\n" +
                     "Headshot multiplier - Multiplier applied when the bullet collides with the head hitbox.\n" +
                     "Firerate in RPM - Maximum achievable firerate in rounds per minute\n" +
                     "Penetration power - Value determining how thick of a wall the bullet can pass through.\n" +
                     "Magazine/clip size - How many rounds can be fired before needing to reload\n" +
                     "Ammo in reserve - How many rounds in total can be fired by a weapon. This number does not include the magazine/clip the weapon comes preloaded with.\n"+
                     "Mobility in u/s - Maximum running speed when wielding the weapon.\n" +
                     "Tagging power - Value used to determine the slowdown inflicted by a hit with this weapon.\n" +
                     "Bullet range - Maximum distance in units a bullet fired by this weapon travels\n" +
                     "Hold to shoot - Whether the weapons continues to shoot when the fire button is held down\n" +
                     "Tracers - Frequency of shooting a bullet with a visible path\n" +
                     "Accurate range stand - Maximum distance in meters from which the first bullet fired when standing is sure to hit a 30cm dinnerplate.\n"+
            "Accurate range crouch - Maximum distance in meters from which the first bullet fired when crouching is sure to hit a 30cm dinnerplate.\n" // TODO
        }
        {
            
        }
        
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
    }

};