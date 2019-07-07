namespace GameJam2019
{
    public enum ResTypeEnum
    {
        Err = 0,
        GUI = 1,
        Model = 2,
        Sound = 3,
        Video = 4,
    }

    public enum AttackDirEnum
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }

    public enum ActionFlagEnum
    {
        Move = 0,
        Rotate = 1,
        ReceiveDamage = 2,
    }

    public enum PropertyEnum
    {
        MoveSpeed = 0,
        CurHp = 1,
        MaxHp = 2,
        AttackDamage = 3,
        SpecialPoint = 4,
    }

    public enum MusicImpactEnum
    {
        EnemyEntity = 0,
        Enemy01Entity = 1,
        Enemy02Entity = 2,
        Enemy03Eneity = 3,
    }
}