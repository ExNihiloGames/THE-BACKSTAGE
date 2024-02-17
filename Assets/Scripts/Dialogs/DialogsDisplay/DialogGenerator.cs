public class DialogGenerator
{
    PlayerDialogs playerDialogs;
    CalmNPCDialogs calmNPCDialogs;
    PoliteNPCDialogs politeNPCDialogs;
    AngryNPCDialogs angryNPCDialogs;
    DrunkNPCDialogs drunkNPCDialogs;
    HighNPCDialogs highNPCDialogs;

    public DialogGenerator(PlayerDialogs playerDialogs, CalmNPCDialogs calmNPCDialogs, PoliteNPCDialogs politeNPCDialogs, AngryNPCDialogs angryNPCDialogs, DrunkNPCDialogs drunkNPCDialogs, HighNPCDialogs highNPCDialogs)
    {
        this.playerDialogs = playerDialogs;
        this.calmNPCDialogs = calmNPCDialogs;
        this.politeNPCDialogs = politeNPCDialogs;
        this.angryNPCDialogs = angryNPCDialogs;
        this.drunkNPCDialogs = drunkNPCDialogs;
        this.highNPCDialogs = highNPCDialogs;
    }

    public string GetNPCRandomDialog(CharacterEffect npcCharacterEffect, DialogStyle dialogStyle)
    {
        switch (npcCharacterEffect)
        {
            case CharacterEffect.None:
                return calmNPCDialogs.GetRandomDialog(dialogStyle);

            case CharacterEffect.Polite:
                return politeNPCDialogs.GetRandomDialog(dialogStyle);

            case CharacterEffect.Angry:
                return angryNPCDialogs.GetRandomDialog(dialogStyle);

            case CharacterEffect.Drunk:
                return drunkNPCDialogs.GetRandomDialog(dialogStyle);

            case CharacterEffect.High:
                return highNPCDialogs.GetRandomDialog(dialogStyle);

            default:
                return calmNPCDialogs.GetRandomDialog(dialogStyle);
        }
    }

    public string GetNPCSpecificDialog(CharacterEffect npcCharacterEffect, DialogStyle dialogStyle, int dialogIndex)
    {
        switch (npcCharacterEffect)
        {
            case CharacterEffect.None:
                return calmNPCDialogs.GetSpecificDialog(dialogStyle, dialogIndex);

            case CharacterEffect.Polite:
                return politeNPCDialogs.GetSpecificDialog(dialogStyle, dialogIndex);

            case CharacterEffect.Angry:
                return angryNPCDialogs.GetSpecificDialog(dialogStyle, dialogIndex);

            case CharacterEffect.Drunk:
                return drunkNPCDialogs.GetSpecificDialog(dialogStyle, dialogIndex);

            case CharacterEffect.High:
                return highNPCDialogs.GetSpecificDialog(dialogStyle, dialogIndex);

            default:
                return calmNPCDialogs.GetSpecificDialog(dialogStyle, dialogIndex);
        }
    }

    public string GetPlayerRandomDialog(DialogStyle dialogStyle)
    {
        return playerDialogs.GetRandomDialog(dialogStyle);
    }

    public string GetPlayerSpecificDialog(DialogStyle dialogStyle, int dialogIndex)
    {
        return playerDialogs.GetSpecificDialog(dialogStyle, dialogIndex);
    }
}
