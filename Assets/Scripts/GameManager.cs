using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Com.Gameaning.StartingwithPhoton
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region PUBLIC VARIABLES
        public static GameManager instance;
        #endregion

        #region PRIVATE VARIABLES
        [SerializeField]
          GameObject player;
        #endregion

        #region MONO CALLBACKS
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }
        void Start()
        {
            if(PhotonNetwork.IsConnected){
                InstantiatePlayer();
            }
            else
            {
                print("No connection ");
            }
        }

        #endregion

        #region PUBLIC CLASSES
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion

        #region PRIVATE CLASSES
        void InstantiatePlayer(){
               if(player != null )
               {
                    int randInt = Random.Range(-100,100);
                    PhotonNetwork.Instantiate(player.name, new Vector3(randInt,player.transform.position.y,randInt),Quaternion.identity);
               }
               else
               {
                   Debug.LogError("player reference is null ");
               }
           }
        
        #endregion
    
        #region PUN CALLBACKS

        public override void OnJoinedRoom(){
            print("I joined to  "+ PhotonNetwork.CurrentRoom.Name+ "Room , my name is  "+ PhotonNetwork.NickName);
        }   
        public override void OnPlayerEnteredRoom (Player otherPlayer ){
            print("Other player Entered to "+ PhotonNetwork.CurrentRoom.Name + " room and his name is "+ otherPlayer.NickName);
            print("now we're "+PhotonNetwork.CurrentRoom.PlayerCount + " in the room ");
        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("GameLuncherScene");
        }

        #endregion
    }

}
