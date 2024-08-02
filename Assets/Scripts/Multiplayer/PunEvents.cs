using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;

public class PunEvents
{
    public const byte FinishDroppingShapeEventCode = 1;
    public static Vector2 targetDroppingPos;
    public static void SendFinishDroppingShapeEvent(int actor, bool isValidDrop)
    {
        MyDebug.Log("my id is {0}, im going to send FinishDroppingShapeEventCode to all players", actor);
        object[] content = new object[] {
            actor,
            isValidDrop,
            targetDroppingPos.x, targetDroppingPos.y
        };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(FinishDroppingShapeEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public const byte GenerateRandomShapeEventCode = 2;
    public static void SendGenerateRandomShapeEvent(int actor)
    {
        object[] content = new object[] { actor };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(GenerateRandomShapeEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public const byte OnShapeDroppedEventCode = 3;
    public static void SendOnShapeDroppedEvent(int actor, int shapeSize)
    {
        MyDebug.Log("my id is {0}, im going to send SendOnShapeDroppedEvent to all players", actor);
        object[] content = new object[] { actor, shapeSize };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(OnShapeDroppedEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }
}
