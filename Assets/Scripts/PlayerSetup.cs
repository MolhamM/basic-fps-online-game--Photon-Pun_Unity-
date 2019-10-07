using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Com.Gameaning.StartingwithPhoton
{
    public class PlayerSetup : MonoBehaviourPunCallbacks
    {
	    #region PRIVATE VARIABLES

    	[SerializeField]
    	GameObject playerCamera;
        [SerializeField]
        TextMeshProUGUI playerNameText;
	    
        #endregion

	    #region MONO CALLBACKS
		void Start()
    	{
        	SetUpPlayerComponents();
            SetPlayerUI();

        }
	    #endregion
	
 	    #region PUBLIC METHODS
    
        #endregion
   
	    #region PRIVATE METHODS
        void SetThisPlayerPropirties(bool val){
		    GetComponent<MovementController>().enabled = val;	
		    playerCamera.GetComponent<Camera>().enabled = val;	
        }
        void SetUpPlayerComponents()
        {
            if (photonView.IsMine)
            {
                SetThisPlayerPropirties(true);
            }
            else
            {
                SetThisPlayerPropirties(false);
            }
        }
        void SetPlayerUI()
        {
            if(playerNameText != null)
            {
                playerNameText.text = photonView.Owner.NickName;
            }
            else
            {
                Debug.LogError("No refrence for Text ");
            }
        }

        #endregion   
    }
}