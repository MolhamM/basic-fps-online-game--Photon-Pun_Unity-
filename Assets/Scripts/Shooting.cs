using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.Gameaning.StartingwithPhoton
{
    public class Shooting : MonoBehaviour
    {
        #region PRIVATE FIELDS
        [SerializeField]
        Camera fpsCam;
        [SerializeField]
        float fireRate = 0.1f;
        float fireTimer = 0;
        #endregion

        #region MONO CALLBACKS
        private void Update()
        {
            CheckFiring();
        }
        #endregion

        #region PRIVATE METHODS
        void CheckFiring()
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > fireRate)
            {
                if (Input.GetButton("Fire1"))
                {
                    fireTimer = 0;
                    Fire();
                }
            }
        }
        void Fire()
        {
            RaycastHit hitObject;
            Ray centerCamRay = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(centerCamRay, out hitObject, 100))
            {
                if (hitObject.collider.gameObject.CompareTag("Player") && !hitObject.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    Damage(hitObject);
                }
            }
        }
        void Damage(RaycastHit hitObject)
        {
            print(hitObject.collider.gameObject.name + "Was hit");
            hitObject.collider.gameObject.GetComponent<PhotonView>().RPC("Damage", RpcTarget.AllBuffered, 10.0f);
        }
        #endregion
    }

}
