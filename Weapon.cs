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
    public int DamageFalloffPer500U;
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
    public double RunningInaccuracy;
    public double LadderInaccuracy;
    public double InaccuracyAtJumpApex;
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

    public Weapon(string rawValues)
    {
        string[] parsedVals = rawValues.Split(",");

        Name = parsedVals[0];
        Price = Int32.Parse(parsedVals[1].Replace("$", ""));
        KillAward = Int32.Parse(parsedVals[2].Replace("$", ""));
        Damage = Int32.Parse(parsedVals[3]);
        Bullets = Int32.Parse(parsedVals[4]);
        ArmorPenetration = Double.Parse(parsedVals[5].Replace("%", ""));
        DamageFalloffPer500U = Int32.Parse(parsedVals[6].Replace("%", ""));
        HeadshotMultiplier = Double.Parse(parsedVals[7].Replace("x", ""));
        RPM = Double.Parse(parsedVals[8]);
        PenetrationPower = Int32.Parse(parsedVals[9].Replace("%", ""));
        MagazineSize = Int32.Parse(parsedVals[10]);
        AmmoInReserve = Int32.Parse(parsedVals[11]);
        Runspeed = Int32.Parse(parsedVals[12]);
        TaggingPower = Int32.Parse(parsedVals[13]);
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
        RunningInaccuracy = Double.Parse(parsedVals[20]);
        LadderInaccuracy = Double.Parse(parsedVals[21]);
        InaccuracyAtJumpApex = Double.Parse(parsedVals[22]);
        InaccuracyFromFiring = Double.Parse(parsedVals[23]);
        RecoveryTimeCrouch = Double.Parse(parsedVals[24]);
        RecoveryTimeStand = Double.Parse(parsedVals[25]);
        RecoilAmount = double.Parse(parsedVals[26]);
        RecoilAngleVariance = Int32.Parse(parsedVals[27]);
        RecoilAmountVariance = Int32.Parse(parsedVals[28]);
        IsRecoilPatternRandom = parsedVals[29].ToLower().Equals("random");
        FatalHeadshotRange = Double.Parse(parsedVals[30]);
        FatalHeadshotRangeHelmet = Double.Parse(parsedVals[31]);
        WeaponType = parsedVals[32].ToLower() switch
        {
            "pistol" => WeaponClass.Pistol,
            "shotgun" => WeaponClass.Shotgun,
            "smg" => WeaponClass.SMG,
            "lmg" => WeaponClass.LMG,
            "rifle" => WeaponClass.Rifle,
            "sniper" => WeaponClass.Sniper
        };
        Scoped = parsedVals[33].ToLower() switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };
        SilencerOn = parsedVals[34].ToLower()  switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };
        BurstFire = parsedVals[35].ToLower()  switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };;
        RapidFire = parsedVals[36].ToLower()  switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };;
        PurchasableBy = parsedVals[37].ToLower() switch
        {
            "cts" => Team.CTs,
            "ts"=> Team.Ts,
            "both" => Team.Both
        };
    }
}