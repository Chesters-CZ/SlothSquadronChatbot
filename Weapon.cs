using System.Diagnostics;

namespace GenerickýProgramNetestovaný;

public class Weapon
{
    public string Name;
    public int Price;
    public int KillAward;
    public int Damage;
    public int Bullets;
    public double ArmorPenetration;
    public int DamageFalloffAt500U;
    public double HeadshotMultiplier;
    public double RPM;
    public int PenetrationPower;
    public int MagazineSize;
    public int AmmoInReserve;
    public int Runspeed;
    public int TaggingPower;
    public int BulletRange;
    public bool HoldToShoot;
    public TracerFrequency Tracers;
    public double AccurateRangeStand;
    public double AccurateRangeCrouch;
    public double StandingInaccuracy;
    public double CrouchingInaccuracy;
    public double RunningInaccuracy;
    public double LadderInaccuracy;
    public double InaccuracyAtJumpApex;
    public double InaccuracyAfterLanding;
    public double InaccuracyFromFiring;
    public double RecoveryTimeCrouch;
    public double RecoveryTimeStand;
    public double RecoilAmount;
    public int RecoilAngleVariance;
    public int RecoilAmountVariance;
    public bool IsRecoilPatternRandom;
    public double FatalHeadshotRange;
    public double FatalHeadshotRangeHelmet;
    public WeaponClass WeaponType;
    public OptionalBoolean Scoped;
    public OptionalBoolean SilencerOn;
    public OptionalBoolean BurstFire;
    public OptionalBoolean RapidFire;
    public Team PurchasableBy;
    public String[] Aliases;
    public Weapon(string rawValues)
    {
        Console.WriteLine(rawValues);
        string[] parsedVals = rawValues.Split(",");
        Name = parsedVals[0];
        Price = Int32.Parse(parsedVals[1].Replace("$", ""));
        KillAward = Int32.Parse(parsedVals[2].Replace("$", ""));
        Damage = Int32.Parse(parsedVals[3]);
        Bullets = Int32.Parse(parsedVals[4]);
        ArmorPenetration = Double.Parse(parsedVals[5].Replace("%", ""));
        DamageFalloffAt500U = Int32.Parse(parsedVals[6].Replace("%", ""));
        HeadshotMultiplier = Double.Parse(parsedVals[7].Replace("x", ""));
        RPM = Double.Parse(parsedVals[8]);
        PenetrationPower = Int32.Parse(parsedVals[9].Replace("%", ""));
        MagazineSize = Int32.Parse(parsedVals[10]);
        AmmoInReserve = Int32.Parse(parsedVals[11]);
        Runspeed = Int32.Parse(parsedVals[12]);
        TaggingPower = Int32.Parse(parsedVals[13].Replace("%",""));
        BulletRange = Int32.Parse(parsedVals[14]);
        HoldToShoot = parsedVals[15].Equals("true");
        Tracers = parsedVals[16].Replace(" ", "").ToLower() switch
        {
            "everybullet" => TracerFrequency.EveryBullet,
            "everythird" => TracerFrequency.EveryThird,
            _ => TracerFrequency.None
        };
        AccurateRangeStand = Double.Parse(parsedVals[17].Replace("m", ""));
        AccurateRangeCrouch = Double.Parse(parsedVals[18].Replace("m", ""));
        StandingInaccuracy = Double.Parse(parsedVals[19]);
        CrouchingInaccuracy = Double.Parse(parsedVals[20]);
        RunningInaccuracy = Double.Parse(parsedVals[21]);
        LadderInaccuracy = Double.Parse(parsedVals[22]);
        InaccuracyAtJumpApex = Double.Parse(parsedVals[23]);
        InaccuracyAfterLanding = Double.Parse(parsedVals[24]);
        InaccuracyFromFiring = Double.Parse(parsedVals[25]);
        RecoveryTimeCrouch = Double.Parse(parsedVals[26]);
        RecoveryTimeStand = Double.Parse(parsedVals[27]);
        RecoilAmount = Double.Parse(parsedVals[28]);
        RecoilAngleVariance = Int32.Parse(parsedVals[29]);
        RecoilAmountVariance = Int32.Parse(parsedVals[30]);
        IsRecoilPatternRandom = parsedVals[31].ToLower().Equals("random");
        FatalHeadshotRange = Double.Parse(parsedVals[32]);
        FatalHeadshotRangeHelmet = Double.Parse(parsedVals[33]);
        WeaponType = parsedVals[34].ToLower() switch
        {
            "pistol" => WeaponClass.Pistol,
            "shotgun" => WeaponClass.Shotgun,
            "smg" => WeaponClass.SMG,
            "lmg" => WeaponClass.LMG,
            "rifle" => WeaponClass.Rifle,
            "sniper" => WeaponClass.Sniper
        };
        Scoped = parsedVals[35].ToLower() switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };
        SilencerOn = parsedVals[36].ToLower()  switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };
        BurstFire = parsedVals[37].ToLower()  switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };;
        RapidFire = parsedVals[38].ToLower()  switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };;
        PurchasableBy = parsedVals[39].ToLower() switch
        {
            "cts" => Team.CTs,
            "ts" => Team.Ts,
            "both" => Team.Both
        };
    }

    public Weapon()
    {
        
    }
}