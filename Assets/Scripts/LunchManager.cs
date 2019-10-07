using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

namespace Com.Gameaning.StartingwithPhoton
{
    public class LunchManager : MonoBehaviourPunCallbacks
    {
        #region PRIVATE VARIABLES
        [SerializeField]
        GameObject enterGamePanel,connectionPanel,lobbyPanel;
        [SerializeField]
        Slider mouseSenistivitySlider;
        [SerializeField]
        Text mouseSenstivityValue;
        #endregion

        #region MONO CALLBACKS
        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;    
        }
        void Start()
        {
            mouseSenistivitySlider.value = .3f;
            OnSliderValueChange();
            if (string.IsNullOrEmpty(PhotonNetwork.NickName)){

                LoadEnterGamePanel();
            }
            else
            {
                LoadLobbyPanel();
            }
        }
        #endregion

        #region PUBLIC METHODS
        public void Connect(){
            if(!PhotonNetwork.IsConnected){
                PhotonNetwork.ConnectUsingSettings();
                LoadConnectingPanel();
            }else{
                print("Your not connected to server");
            }
        }
        public void JoinRandomRoom(){
            PhotonNetwork.JoinRandomRoom();
        }
        public void OnSliderValueChange()
        {
            mouseSenstivityValue.text = ((float)System.Math.Round((float)(mouseSenistivitySlider.value * 10), 2)).ToString();
            //mouseSenstivityValue.text = ((int)(mouseSenistivitySlider.value * 10)).ToString();
            PlayerPrefs.SetFloat("MouseSpeed", mouseSenistivitySlider.value*10);
        }
        #endregion

        #region PRIVATE METHODS 
            void LoadEnterGamePanel(){
                enterGamePanel.SetActive(true);
                connectionPanel.SetActive(false);
                lobbyPanel.SetActive(false);
            }
            void LoadConnectingPanel(){
                connectionPanel.SetActive(true);
                enterGamePanel.SetActive(false);
                lobbyPanel.SetActive(false);
            }
            void LoadLobbyPanel(){
                lobbyPanel.SetActive(true);
                enterGamePanel.SetActive(false);
                connectionPanel.SetActive(false);
            }
            void CreateAndJoinRoom(){
                string randomRoomName = "Room : "+Random.Range(0,100000);
                
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.IsOpen = true;
                roomOptions.IsVisible = true;
                roomOptions.MaxPlayers = 20;
                PhotonNetwork.CreateRoom(randomRoomName , roomOptions);
            }
        #endregion

        #region PUNCALLBACKS
            public override void OnConnected(){
                print("Connected to internet");
            }
            public override void OnConnectedToMaster(){
                LoadLobbyPanel();
                print("Iam Connected to server my name is " + PhotonNetwork.NickName);
            }
            public override void OnJoinRandomFailed (short returnCode, string message){

                print("failed to join room call back : "+ message);
                CreateAndJoinRoom();
            }
            public override void OnJoinedRoom(){
                print("player : "+PhotonNetwork.NickName +" joined to " + PhotonNetwork.CurrentRoom.Name);
                PhotonNetwork.LoadLevel("GameScene");
            }
            public override void OnPlayerEnteredRoom (Player otherPlayer){
                print("other player joined Name : "+PhotonNetwork.NickName +" room : "+PhotonNetwork.CurrentRoom);
                print("current players in room is "+PhotonNetwork.CurrentRoom.PlayerCount);
            } 
        #endregion
    }
    
}
