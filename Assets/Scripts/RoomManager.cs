using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;
    public GameObject loadingScreen, menuScreen, createRoomScreen, roomScreen, errorScreen;
    public GameObject roomBrowserScreen, setNicknameScreen;
    public TMP_Text loadingText, roomNameText, errorText, nicknameText;
    public TMP_InputField roomNameInput, nicknameInput;
    public RoomItem roomItemPrefab;

    public TMP_Text[] playerNameText;
    public GameObject buttonStartGame, buttonReady;

    // public GameObject buttonTestRoom;

    Dictionary<string, RoomItem> My_dict1 = new Dictionary<string, RoomItem>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    #region Connection
    void Start()
    {
        CloseAllScreens();
        if(!PhotonNetwork.IsConnected){
            loadingScreen.SetActive(true);
            loadingText.text = "Connecting to network...";
            PhotonNetwork.ConnectUsingSettings();
        }
        else OnJoinedLobby();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CloseAllScreens()
    {
        if(loadingScreen != null){
            loadingScreen.SetActive(false);
            menuScreen.SetActive(false);
            createRoomScreen.SetActive(false);
            roomScreen.SetActive(false);
            errorScreen.SetActive(false);
            roomBrowserScreen.SetActive(false);
            setNicknameScreen.SetActive(false);
        }
    }

    public void OpenSetNicknameScreen(){
        //CloseAllScreens();
        setNicknameScreen.SetActive(true);
        if(PlayerPrefs.HasKey("nickname")) nicknameInput.text = PlayerPrefs.GetString("nickname");
    }

    public void SaveNickname(){
        if(!string.IsNullOrEmpty(nicknameInput.text)){
            PhotonNetwork.NickName = nicknameInput.text;
            PlayerPrefs.SetString("nickname", nicknameInput.text);
            nicknameText.text = nicknameInput.text;
            BackToMenu();
        }
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("Connected to server");
        loadingText.text = "Joining lobby...";
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        Debug.Log("joined lobby");
        CloseAllScreens();
        menuScreen.SetActive(true);

        if(PlayerPrefs.HasKey("nickname")) PhotonNetwork.NickName = PlayerPrefs.GetString("nickname");
        else{
            //temporary random name
            PhotonNetwork.NickName = "Guess";
            for(int i = 0; i < 6; ++i){
                PhotonNetwork.NickName += Random.Range(0, 9).ToString();
            }
            PlayerPrefs.SetString("nickname", PhotonNetwork.NickName);
        }
        nicknameText.text = PhotonNetwork.NickName;

        foreach (KeyValuePair<string, RoomItem> ele in My_dict1)
            Destroy(My_dict1[ele.Key].gameObject);
        My_dict1.Clear();
    }

    public override void OnLeftLobby()
    {
        // base.OnLeftLobby();
        foreach (KeyValuePair<string, RoomItem> ele in My_dict1)
            Destroy(My_dict1[ele.Key].gameObject);
        My_dict1.Clear();
    }
    #endregion

    #region Create/Join room
    public void OpenCreateRoomScreen()
    {
        CloseAllScreens();
        createRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 2
            };
            PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, options, null);
            CloseAllScreens();
            loadingText.text = "Creating room...";
            loadingScreen.SetActive(true);
        }
    }

    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        CloseAllScreens();
        loadingText.text = "Joining room...";
        loadingScreen.SetActive(true);
    }

    public void JoinRoom(string roomInfoName)
    {
        PhotonNetwork.JoinRoom(roomInfoName);
        CloseAllScreens();
        loadingText.text = "Joining room...";
        loadingScreen.SetActive(true);
    }

    public void JoinRoomTest(){
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 2
        };
        PhotonNetwork.JoinOrCreateRoom("GM test", options, null);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        CloseAllScreens();
        roomScreen.SetActive(true);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        //update dictionary
        if (!My_dict1.ContainsKey(roomNameText.text))
        {
            RoomItem newRoomItem = Instantiate(roomItemPrefab, roomItemPrefab.transform.parent);
            newRoomItem.SeRoomDetail(roomNameText.text);
            newRoomItem.gameObject.SetActive(true);

            My_dict1.Add(roomNameText.text, newRoomItem);
        }

        UpdatePlayerList();

        buttonStartGame.SetActive(PhotonNetwork.IsMasterClient);
        buttonReady.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // base.OnMasterClientSwitched(newMasterClient);
        buttonStartGame.SetActive(PhotonNetwork.IsMasterClient);
        buttonReady.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList(); //can be optimized by only updating the new player info
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList();
    }

    void UpdatePlayerList(){
        //playerNameText[0].transform.parent.gameObject.SetActive(false);
        playerNameText[1].transform.parent.gameObject.SetActive(false);

        Player[] players = PhotonNetwork.PlayerList;
        for(int i = 0; i < Mathf.Min(players.Length, 2); ++i){
            Debug.Log("found a player named " + players[i].NickName);
            playerNameText[i].transform.parent.gameObject.SetActive(true);
            playerNameText[i].text = players[i].NickName;
            if(players[i].IsLocal) playerNameText[i].text += " (me)";
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseAllScreens();
        loadingText.text = "Leaving room...";
        loadingScreen.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        //base.OnLeftRoom();
        CloseAllScreens();
        if(menuScreen) menuScreen.SetActive(true);
    }
    #endregion

    #region RoomFailed
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        CloseAllScreens();
        errorText.text = "OnCreateRoomFailed:\n" + message;
        errorScreen.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        CloseAllScreens();
        errorText.text = "OnJoinRoomFailed:\n" + message;
        errorScreen.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // base.OnDisconnected(cause);
        CloseAllScreens();
        errorText.text = "OnDisconnected:\n" + cause;
        errorScreen.SetActive(true);

        foreach (KeyValuePair<string, RoomItem> ele in My_dict1)
            Destroy(My_dict1[ele.Key].gameObject);
        My_dict1.Clear();
    }

    public void BackToMenu()
    {
        CloseAllScreens();
        menuScreen.SetActive(true);
    }
    #endregion

    #region RoomBrowser
    public void OpenRoomBrowserScreen()
    {
        CloseAllScreens();
        roomBrowserScreen.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //base.OnRoomListUpdate(roomList);
        Debug.Log("OnRoomListUpdate" + roomList.Count);
        roomItemPrefab.gameObject.SetActive(false);
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList || roomList[i].PlayerCount == roomList[i].MaxPlayers)
            {
                if (My_dict1.ContainsKey(roomList[i].Name))
                {
                    Debug.Log("destroying " + roomList[i].Name);
                    Destroy(My_dict1[roomList[i].Name].gameObject);
                    My_dict1.Remove(roomList[i].Name);
                }
            }
            if (roomList[i].PlayerCount != roomList[i].MaxPlayers)
            {
                //Debug.Log("roomList[i].RemovedFromList)" + roomList[i].RemovedFromList);
                if (!roomList[i].RemovedFromList && !My_dict1.ContainsKey(roomList[i].Name))
                {
                    RoomItem newRoomItem = Instantiate(roomItemPrefab, roomItemPrefab.transform.parent);
                    newRoomItem.SeRoomDetail(roomList[i]);
                    newRoomItem.gameObject.SetActive(true);

                    My_dict1.Add(roomList[i].Name, newRoomItem);
                }
            }
        }
    }


    #endregion

    public void StartGame(){
        PhotonNetwork.LoadLevel("Multiplayer");
    }

    public void BackToMenu(string str){
        //PhotonNetwork.Disconnect();
        SceneManager.LoadScene(str);
    }
}
