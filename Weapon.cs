namespace GenerickýProgramNetestovaný;

public class Weapon
{
    public readonly string Name = null!;
    public readonly int Price;
    public readonly int KillAward;
    public readonly int Damage;
    public readonly int Bullets;
    public readonly double ArmorPenetration;
    public readonly int DamageFalloffAt500U;
    public readonly double HeadshotMultiplier;
    // ReSharper disable once InconsistentNaming
    public readonly double RPM;
    public readonly int PenetrationPower;
    public readonly int MagazineSize;
    public readonly int AmmoInReserve;
    public readonly int Runspeed;
    public readonly int TaggingPower;
    public readonly int BulletRange;
    public readonly bool HoldToShoot;
    public readonly TracerFrequency Tracers;
    public readonly double AccurateRangeStand;
    public readonly double AccurateRangeCrouch;
    public readonly double StandingInaccuracy;
    public readonly double CrouchingInaccuracy;
    public readonly double RunningInaccuracy;
    public readonly double LadderInaccuracy;
    public readonly double InaccuracyAtJumpApex;
    public readonly double InaccuracyAfterLanding;
    public readonly double InaccuracyFromFiring;
    public readonly double RecoveryTimeCrouch;
    public readonly double RecoveryTimeStand;
    public readonly double RecoilAmount;
    public readonly int RecoilAngleVariance;
    public readonly int RecoilAmountVariance;
    public readonly bool IsRecoilPatternRandom;
    public readonly double FatalHeadshotRange;
    public readonly double FatalHeadshotRangeHelmet;
    public readonly WeaponClass WeaponType;
    public readonly OptionalBoolean Scoped;
    public readonly OptionalBoolean SilencerOn;
    public readonly OptionalBoolean BurstFire;
    public readonly OptionalBoolean RapidFire;
    public readonly Team PurchasableBy;
    public readonly string[] Aliases = null!;
    private Weapon(string rawValues)
    {
        Console.WriteLine(rawValues);
        string[] parsedVals = rawValues.Split(",");
        Name = parsedVals[0];
        Price = int.Parse(parsedVals[1].Replace("$", ""));
        KillAward = int.Parse(parsedVals[2].Replace("$", ""));
        Damage = int.Parse(parsedVals[3]);
        Bullets = int.Parse(parsedVals[4]);
        ArmorPenetration = double.Parse(parsedVals[5].Replace("%", ""));
        DamageFalloffAt500U = int.Parse(parsedVals[6].Replace("%", ""));
        HeadshotMultiplier = double.Parse(parsedVals[7].Replace("x", ""));
        RPM = double.Parse(parsedVals[8]);
        PenetrationPower = int.Parse(parsedVals[9].Replace("%", ""));
        MagazineSize = int.Parse(parsedVals[10]);
        AmmoInReserve = int.Parse(parsedVals[11]);
        Runspeed = int.Parse(parsedVals[12]);
        TaggingPower = int.Parse(parsedVals[13].Replace("%",""));
        BulletRange = int.Parse(parsedVals[14]);
        HoldToShoot = parsedVals[15].Equals("true");
        Tracers = parsedVals[16].Replace(" ", "").ToLower() switch
        {
            "everybullet" => TracerFrequency.EveryBullet,
            "everythird" => TracerFrequency.EveryThird,
            _ => TracerFrequency.None
        };
        AccurateRangeStand = double.Parse(parsedVals[17].Replace("m", ""));
        AccurateRangeCrouch = double.Parse(parsedVals[18].Replace("m", ""));
        StandingInaccuracy = double.Parse(parsedVals[19]);
        CrouchingInaccuracy = double.Parse(parsedVals[20]);
        RunningInaccuracy = double.Parse(parsedVals[21]);
        LadderInaccuracy = double.Parse(parsedVals[22]);
        InaccuracyAtJumpApex = double.Parse(parsedVals[23]);
        InaccuracyAfterLanding = double.Parse(parsedVals[24]);
        InaccuracyFromFiring = double.Parse(parsedVals[25]);
        RecoveryTimeCrouch = double.Parse(parsedVals[26]);
        RecoveryTimeStand = double.Parse(parsedVals[27]);
        RecoilAmount = double.Parse(parsedVals[28]);
        RecoilAngleVariance = int.Parse(parsedVals[29]);
        RecoilAmountVariance = int.Parse(parsedVals[30]);
        IsRecoilPatternRandom = parsedVals[31].ToLower().Equals("random");
        FatalHeadshotRange = double.Parse(parsedVals[32]);
        FatalHeadshotRangeHelmet = double.Parse(parsedVals[33]);
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
        };
        RapidFire = parsedVals[38].ToLower()  switch
        {
            "cannot" => OptionalBoolean.Cannot,
            "true" => OptionalBoolean.Yes,
            "false" => OptionalBoolean.No
        };
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