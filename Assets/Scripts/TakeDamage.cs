using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Com.Gameaning.StartingwithPhoton
{
    public class TakeDamage : MonoBehaviourPunCallbacks
    {
        #region PRIVATE FIELDS
        private float health, startHealth;
        [SerializeField]
        Image healthBar;
        #endregion

        #region MONO CALLBACKS
        void Start()
        {
            startHealth = health = 100;
            healthBar.fillAmount = health / startHealth;
        }
        #endregion

        #region PUBLIC METHODS 
        [PunRPC]
        public void Damage(float damage)
        {
            print("current health before damage is " + health);
            if (damage > 0)
            {
                health -= damage;
                healthBar.fillAmount = health / startHealth;
                print("current health after damage is " + health);
            }
            if (health <= 0)
            {
                Die();
            }
        }
        #endregion
        #region PRIVATE METHOD
        private void Die()
        {
            print("Im dead " + GetComponent<PhotonView>().Owner.NickName);
            if (photonView.IsMine)
            {
                GameManager.instance.LeaveRoom();
            }
        }
        #endregion
    }

}
