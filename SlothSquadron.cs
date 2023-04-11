using System.Reflection;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace GenerickýProgramNetestovaný;

public class SlothSquadron
{
    private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        { WriteIndented = true, IncludeFields = true, MaxDepth = int.MaxValue };

    private List<Weapon> knownWeapons =
        JsonSerializer.Deserialize<List<Weapon>>(File.ReadAllText("../../../Databaze.json"), _serializerOptions);

    public SlothSquadron()
    {
        foreach (FieldInfo property in typeof(Weapon).GetFields())
            Console.WriteLine(property.Name.ToLower());
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
                     "Kill award (reward, award) - Reward for killing an enemy with this weapon\n" +
                     "Damage - The base damage for every bullet fired before any multipliers are applied\n" +
                     "Bullets - How many bullets are fired per shot\n" +
                     "Armor penetration - Percentage of damage passing through kevlar and helmets.\n" +
                     "Damage falloff at 500U (falloff) - Percentage of damage lost at a range of 500 units. The multiplier is applied as falloff^(distance/500)\n" +
                     "Headshot multiplier (multiplier) - Multiplier applied when the bullet collides with the head hitbox.\n" +
                     "RPM (firerate) - Maximum achievable firerate in rounds per minute\n" +
                     "Penetration power - Value determining how thick of a wall the bullet can pass through.\n" +
                     "Magazine size (mag size)- How many rounds can be fired before needing to reload\n" +
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
                     "Fatal headshot range - Maximum distance in units from which a single bullet fired deals 100 damage or more. A value of -1 means a single headshot will never kill.\n" +
                     "Fatal headshot range helmet (helmet range) - Maximum distance in units from which a single bullet fired deals 100 damage or more to a helmeted enemy. A value of -1 means a single headshot from this weapon will never kill a helmeted enemy.\n" +
                     "Weapon type - Whether the weapon is a pistol, shotgun, SMG, LMG, assault rifle or a sniper rifle.\n" +
                     "Scoped (scope) - Can the weapon have a scope? If yes, are the current values for when scoped or unscoped?\n" +
                     "Silencer on (silencer) - Does the weapon come with a detachable silencer? If yes, are the current values for when it is attached or detached?\n" +
                     "Burst fire (burst) - Can the weapon fire a quick burst of three bullets? If yes, are the current values for when firing in bursts or not?\n" +
                     "Rapid fire (secondary fire) - Does the weapon offer a secondary fire mode with no prime time? If yes, are the current values for the primary or secondary fire?\n" +
                     "Purchasable by (team) - Which teams can purchase this weapon. Note that some weapons share a slot and only one of them can be chosen to be purchasable during a match.\n";
            return output;
        }

        List<String> weapons = DetectWeapon(query);
        List<String> attributes = DetectAttribute(query);

        bool canProceed = true;

        if (weapons.Count > 0 || attributes.Count > 0)
        {
            output += "I understood you want to know";
            if (attributes.Count > 1)
            {
                output += ":\n" + String.Join(", ", attributes) + "\nof";
            }
            else if (attributes.Count == 1)
            {
                output += "the " + attributes[0] + " of";
            }
            else if (attributes.Count == 0)
            {
                output += " something about";
                canProceed = false;
            }

            if (weapons.Count > 1)
            {
                output += ":\n" + String.Join(", ", weapons);
            }
            else if (weapons.Count == 1)
            {
                output += " the " + weapons[0];
            }
            else if (weapons.Count == 0)
            {
                output += " one of the weapons.";
                canProceed = false;
            }

            if (!canProceed)
            {
                output += "\n\n Please try to rephrase your question to include the " +
                          (attributes.Count == 0 ? "attribute" : "weapon") + " you want me to tell you about.";
                return output;
            }
            else
            {
                if (weapons.Count == 1 && attributes.Count == 1)
                {
                    output += "\n\n The " + attributes[0] + " of " + weapons[0] + " is " +
                              GetAttributeByName(weapons[0], attributes[0]);
                    return output;
                }
                else
                {
                    output += "\n\n";
                    output += "NAME".PadRight(35);
                    foreach (string attribute in attributes)
                    {
                        output += attribute.ToUpper().PadRight(35);
                    }

                    foreach (string weapon in weapons)
                    {
                        output += "\n";
                        output += weapon.PadRight(35);
                        output += GetMultipleAttributesByName(weapon, attributes);
                    }
                }
                return output;
            }
        }

        return "I'm sorry, my responses are limited. You must ask the right questions.";
    }

    private String GetMultipleAttributesByName(string weapon, List<string> attributes)
    {
        string output = "";
        OptionalBoolean optionalParam = OptionalBoolean.Cannot;

        if (weapon.ToLower().Contains("revolver"))
        {
            Console.WriteLine(
                "You have mentioned the R8 Revolver. Would you like to know about stats for the primary or secondary fire? (p/s)");

            while (true)
            {
                String input = Console.ReadLine() ?? string.Empty;
                if (input.ToUpper()[0].Equals('P'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("glock"))
        {
            Console.WriteLine(
                "You have mentioned the Glock-18. Would you like to know about stats for semi-auto or burst fire? (s/b)");

            while (true)
            {
                String input = Console.ReadLine() ?? string.Empty;
                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('B'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("usp"))
        {
            Console.WriteLine(
                "You have mentioned the USP-S. Would you like to know about stats for firing with the silencer attached or detached? (a/d)");

            while (true)
            {
                String input = Console.ReadLine() ?? string.Empty;
                if (input.ToUpper()[0].Equals('D'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('A'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("aug"))
        {
            Console.WriteLine(
                "You have mentioned the AUG. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
            while (true)
            {
                String input = Console.ReadLine() ?? string.Empty;
                if (input.ToUpper()[0].Equals('U'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("famas"))
        {
            Console.WriteLine(
                "You have mentioned the FAMAS. Would you like to know about stats for automatic or burst fire? (a/b)");
            while (true)
            {
                String input = Console.ReadLine() ?? string.Empty;
                if (input.ToUpper()[0].Equals('A'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('B'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("m4a1"))
        {
            Console.WriteLine(
                "You have mentioned the M4A1-S. Would you like to know about stats for firing with the silencer attached or detached? (a/d)");
            while (true)
            {
                String input = Console.ReadLine() ?? string.Empty;
                if (input.ToUpper()[0].Equals('D'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('A'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.Contains("sg553"))
        {
            Console.WriteLine(
                "You have mentioned the SG 553. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
            while (true)
            {
                String input = Console.ReadLine() ??string.Empty;
                if (input.ToUpper()[0].Equals('U'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("awp"))
        {
            Console.WriteLine(
                "You have mentioned the AWP. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
            while (true)
            {
                String input = Console.ReadLine() ??string.Empty;
                if (input.ToUpper()[0].Equals('U'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("g3sg1"))
        {
            Console.WriteLine(
                "You have mentioned the G3SG1. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
            while (true)
            {
                String input = Console.ReadLine() ??string.Empty;
                if (input.ToUpper()[0].Equals('U'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("scar-20"))
        {
            Console.WriteLine(
                "You have mentioned the SCAR-20. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
            while (true)
            {
                String input = Console.ReadLine() ??string.Empty;
                if (input.ToUpper()[0].Equals('U'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }
        else if (weapon.ToLower().Contains("ssg 08"))
        {
            Console.WriteLine(
                "You have mentioned the SSG 08. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
            while (true)
            {
                String input = Console.ReadLine() ??string.Empty;
                if (input.ToUpper()[0].Equals('U'))
                {
                    optionalParam = OptionalBoolean.No;
                    break;
                }

                if (input.ToUpper()[0].Equals('S'))
                {
                    optionalParam = OptionalBoolean.Yes;
                    break;
                }
            }
        }

        foreach (string attribute in attributes)
        {
            output += GetAttributeByName(weapon, attribute, optionalParam).PadRight(35);
        }

        return output;
    }

    private String GetAttributeByName(string weapon, string attribute,
        OptionalBoolean optionalParam = OptionalBoolean.Cannot)
    {
        Weapon w = new Weapon();
        if (optionalParam != OptionalBoolean.Cannot)
        {
            if (weapon.ToLower().Contains("revolver"))
            {
                Console.WriteLine(
                    "You have mentioned the R8 Revolver. Would you like to know about stats for the primary or secondary fire? (p/s)");

                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('P'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("glock"))
            {
                Console.WriteLine(
                    "You have mentioned the Glock-18. Would you like to know about stats for semi-auto or burst fire? (s/b)");

                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('B'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("usp"))
            {
                Console.WriteLine(
                    "You have mentioned the USP-S. Would you like to know about stats for firing with the silencer attached or detached? (a/d)");

                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('D'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('A'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("aug"))
            {
                Console.WriteLine(
                    "You have mentioned the AUG. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('U'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("famas"))
            {
                Console.WriteLine(
                    "You have mentioned the FAMAS. Would you like to know about stats for automatic or burst fire? (a/b)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('A'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('B'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("m4a1"))
            {
                Console.WriteLine(
                    "You have mentioned the M4A1-S. Would you like to know about stats for firing with the silencer attached or detached? (a/d)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('D'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('A'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("sg 553"))
            {
                Console.WriteLine(
                    "You have mentioned the SG 553. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('U'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("awp"))
            {
                Console.WriteLine(
                    "You have mentioned the AWP. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('U'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("g3sg1"))
            {
                Console.WriteLine(
                    "You have mentioned the G3SG1. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('U'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("scar-20"))
            {
                Console.WriteLine(
                    "You have mentioned the SCAR-20. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('U'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
            else if (weapon.ToLower().Contains("ssg 08"))
            {
                Console.WriteLine(
                    "You have mentioned the SSG 08. Would you like to know about stats for firing when scoped or unscoped? (s/u)");
                while (true)
                {
                    String input = Console.ReadLine() ??string.Empty;
                    if (input.ToUpper()[0].Equals('U'))
                    {
                        optionalParam = OptionalBoolean.No;
                        break;
                    }

                    if (input.ToUpper()[0].Equals('S'))
                    {
                        optionalParam = OptionalBoolean.Yes;
                        break;
                    }
                }
            }
        }

        foreach (Weapon knownWeapon in knownWeapons)
        {
            if (knownWeapon.Name.Equals(weapon))
            {
                if (knownWeapon.Scoped == optionalParam || knownWeapon.SilencerOn == optionalParam ||
                    knownWeapon.BurstFire == optionalParam || knownWeapon.RapidFire == optionalParam)
                {
                    w = knownWeapon;
                }
            }
        }

        switch (attribute)
        {
            case "price":
                return w.Price.ToString();
            case "killaward":
                return w.KillAward.ToString();
            case "damage":
                return w.Damage.ToString();
            case "bullets":
                return w.Bullets.ToString();
            case "armorpenetration":
                return w.ArmorPenetration.ToString();
            case "damagefalloffat500u":
                return w.DamageFalloffAt500U.ToString();
            case "headshotmultiplier":
                return w.HeadshotMultiplier.ToString();
            case "rpm":
                return w.RPM.ToString();
            case "penetrationpower":
                return w.PenetrationPower.ToString();
            case "magazinesize":
                return w.MagazineSize.ToString();
            case "ammoinreserve":
                return w.AmmoInReserve.ToString();
            case "runspeed":
                return w.Runspeed.ToString();
            case "taggingpower":
                return w.TaggingPower.ToString();
            case "bulletrange":
                return w.BulletRange.ToString();
            case "holdtoshoot":
                return w.HoldToShoot.ToString();
            case "tracers":
                return w.Tracers.ToString();
            case "accuraterangestand":
                return w.AccurateRangeStand.ToString();
            case "accuraterangecrouch":
                return w.AccurateRangeCrouch.ToString();
            case "standinginaccuracy":
                return w.StandingInaccuracy.ToString();
            case "crouchinginaccuracy":
                return w.CrouchingInaccuracy.ToString();
            case "runninginaccuracy":
                return w.RunningInaccuracy.ToString();
            case "ladderinaccuracy":
                return w.LadderInaccuracy.ToString();
            case "inaccuracyatjumpapex":
                return w.InaccuracyAtJumpApex.ToString();
            case "inaccuracyafterlanding":
                return w.InaccuracyAfterLanding.ToString();
            case "inaccuracyfromfiring":
                return w.InaccuracyFromFiring.ToString();
            case "recoverytimecrouch":
                return w.RecoveryTimeCrouch.ToString();
            case "recoverytimestand":
                return w.RecoveryTimeStand.ToString();
            case "recoilamount":
                return w.RecoilAmount.ToString();
            case "recoilanglevariance":
                return w.RecoilAngleVariance.ToString();
            case "recoilamountvariance":
                return w.RecoilAmountVariance.ToString();
            case "isrecoilpatternrandom":
                return w.IsRecoilPatternRandom.ToString();
            case "fatalheadshotrange":
                return w.FatalHeadshotRange.ToString();
            case "fatalheadshotrangehelmet":
                return w.FatalHeadshotRangeHelmet.ToString();
            case "weapontype":
                return w.WeaponType.ToString();
            case "scoped":
                return w.Scoped.ToString();
            case "silenceron":
                return w.SilencerOn.ToString();
            case "burstfire":
                return w.BurstFire.ToString();
            case "rapidfire":
                return w.RapidFire.ToString();
            case "purchasableby":
                return w.PurchasableBy.ToString();
            default:
                return "an invalid value";
        }
    }


    private List<String> DetectWeapon(String query)
    {
        List<String> weapons = new List<string>();

        foreach (Weapon weapon in knownWeapons)
        {
            if (query.Contains(weapon.Name))
            {
                weapons.Add(weapon.Name);
            }
            else
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
        }

        return weapons;
    }

    private List<String> DetectAttribute(string query)
    {
        List<String> attributes = new List<String>();

        query = query.ToLower().Replace(" ", "");

        foreach (FieldInfo field in typeof(Weapon).GetFields())
        {
            if (query.Contains(field.Name.ToLower()))
            {
                attributes.Add(field.Name.ToLower());
            }

            if (query.Contains("cost"))
            {
                attributes.Add("price");
            }

            if (query.Contains("award") || query.Contains("reward"))
            {
                attributes.Add("killaward");
            }

            if (query.Contains("falloff"))
            {
                attributes.Add("damagefalloffat500u");
            }

            if (query.Contains("multiplier"))
            {
                attributes.Add("headshotmultiplier");
            }

            if (query.Contains("firerate"))
            {
                attributes.Add("rpm");
            }

            if (query.Contains("magsize"))
            {
                attributes.Add("magazinesize");
            }

            if (query.Contains("reserveammo"))
            {
                attributes.Add("ammoinreserve");
            }

            if (query.Contains("mobility") || query.Contains("speed"))
            {
                attributes.Add("runspeed");
            }

            if (query.Contains("tag"))
            {
                attributes.Add("taggingpower");
            }

            if (query.Contains("hts"))
            {
                attributes.Add("holdtoshoot");
            }

            if (query.Contains("standingaccuracy"))
            {
                attributes.Add("accuraterangestand");
            }

            if (query.Contains("crouchingaccuracy"))
            {
                attributes.Add("accuraterangecrouch");
            }

            if (query.Contains("landinginaccuracy"))
            {
                attributes.Add("inaccuracyafterlanding");
            }

            if (query.Contains("firinginaccuracy"))
            {
                attributes.Add("inaccuracyfromfiring");
            }

            if (query.Contains("crouchingrecovery"))
            {
                attributes.Add("recoverytimecrouch");
            }

            if (query.Contains("standingrecovery"))
            {
                attributes.Add("recoverytimestand");
            }

            if (query.Contains("recoilpattern"))
            {
                attributes.Add("isrecoilpatternrandom");
            }

            if (query.Contains("helmetrange"))
            {
                attributes.Add("fatalheadshotrangehelmet");
            }

            if (query.Contains("scope"))
            {
                attributes.Add("scoped");
            }

            if (query.Contains("silencer"))
            {
                attributes.Add("silenceron");
            }

            if (query.Contains("burst"))
            {
                attributes.Add("burstfire");
            }

            if (query.Contains("secondaryfire"))
            {
                attributes.Add("rapidfire");
            }

            if (query.Contains("team"))
            {
                attributes.Add("purchasableby");
            }
        }

        return attributes.Distinct().ToList();
    }
};