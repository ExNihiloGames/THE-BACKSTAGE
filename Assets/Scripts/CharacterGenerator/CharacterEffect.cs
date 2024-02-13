[System.Flags]
public enum CharacterEffect
{
    None = 0,

    Drunk = 1<<0,
    High = 1<<1,

    All = ~0

}
