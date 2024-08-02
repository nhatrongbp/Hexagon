using System;

public class GameEvents
{
    public static Action<int, int> OnHoverGridSquare;   //<col, row>
    public static Action OnUnhoverGridSquare;
    public static Action<int> OnShapeDropped;           //<diceType>

    //multiplayer
    public static Action<bool> OnTurnChanged;
}
