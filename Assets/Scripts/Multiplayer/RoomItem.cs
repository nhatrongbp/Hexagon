using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomNameText;
    RoomInfo roomInfo;
    
    public void SeRoomDetail(RoomInfo info){
        roomInfo = info;
        roomNameText.text = info.Name;
    }

    public void SeRoomDetail(string infoName){
        roomNameText.text = infoName;
    }

    public void OpenRoom(){
        if(roomInfo != null) RoomManager.instance.JoinRoom(roomInfo);
        else if(roomNameText.text != "") RoomManager.instance.JoinRoom(roomNameText.text);
    }
}
