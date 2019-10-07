using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Com.Gameaning.StartingwithPhoton
{
    [RequireComponent(typeof(InputField))]
    public class PlayerInputManager : MonoBehaviour
    {
        #region PRIVATE VARIABLES
            [SerializeField]
            InputField input ;
            [SerializeField]
            LunchManager luncher;
        #endregion

        #region MONO CALLBACKS

            void Start(){

                input = GetComponent<InputField>();

                if(input == null){
                    Debug.LogError("Make sure you add this script to object that contains InputField");
                }
            }
            void OnValueChanged(){
                Debug.Log("test");
            }

        #endregion

        #region PUBLIC METHODS
            public void SetPlayerName(){
                string playerName = input.text;
                
                if(string.IsNullOrEmpty(playerName)){
                    Debug.Log("Player name is empty");
                    return;
                }

                PhotonNetwork.NickName = playerName;
                luncher.Connect();
            }

        #endregion
    }
    
}
