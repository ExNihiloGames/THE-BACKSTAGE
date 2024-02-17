[System.Flags]
public enum CharacterEffect
{
    None = 0,

    Drunk = 1<<0,
    High = 1<<1,
    Angry = 1<<2,
    Polite = 1<<3,

    All = ~0

}
