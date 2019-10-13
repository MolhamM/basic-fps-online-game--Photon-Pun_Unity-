using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.Gameaning.StartingwithPhoton
{
    public class MovementController : MonoBehaviour
    {
        #region PRIVATE VARIABLES
        [SerializeField]
        float speed = 5f,turningAroundSensitevity = 3 , jumpForce =10;
        float currentCamRotationUpAndDown = 0 ,camRotationUpAndDown ;
        Rigidbody rigidbody ;
        Vector3 velocity,rotateValue, camRotateValue ;
        [SerializeField]
        GameObject playerCameraView;
        [SerializeField]
        LayerMask jumpableLayer;
        bool canJump = false;
        CapsuleCollider capCollider;
        KeyCode jumpKeyCode = KeyCode.Space;
        #endregion


        #region MONO CALLBACKS
        void Start()
        {
            turningAroundSensitevity = PlayerPrefs.GetFloat("MouseSpeed", 3);
            currentCamRotationUpAndDown= 0;
            velocity = rotateValue = camRotateValue = Vector3.zero;
            rigidbody = GetComponent<Rigidbody>();
            capCollider = GetComponent<CapsuleCollider>();
        }
        void Update()
        {
            CheckMovements();
            CheckJump();
            CheckRotation();
        }
        void FixedUpdate()
        {
            Move();
            Jump();
            Rotate();
        }
        #endregion




        #region PRIVATE CLASSES
        #region  MOVEMENTS
        void CheckMovements(){
            Vector3 horizontalVelocity = HorizontalMovement();
            Vector3 verticalVelocity = VerticalMovement();
            velocity = ((horizontalVelocity+verticalVelocity).normalized*speed);
        }
        void CheckJump()
        {

            canJump = Physics.CheckCapsule(capCollider.bounds.center,
                 new Vector3(capCollider.bounds.center.x, capCollider.bounds.min.y, capCollider.bounds.center.z),
                 capCollider.radius *.9f,jumpableLayer);
            print(canJump);
        }
        Vector3 HorizontalMovement(){
            float movement = Input.GetAxis("Horizontal");
            Vector3 movementVelocity = transform.forward*-movement;
                    
            return movementVelocity;
        }
        Vector3 VerticalMovement(){
            float movement = Input.GetAxis("Vertical");
            Vector3 movementVelocity = transform.right*movement;
                    
            return movementVelocity;
        }
        void Move(){
            if(velocity!= Vector3.zero){
                rigidbody.MovePosition(rigidbody.position+velocity*Time.fixedDeltaTime);
            }
        }
        void Jump()
        {
            print("here in jump");
            if (Input.GetKey(jumpKeyCode) && canJump)
            {
                print("inside");
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        #endregion

        #region Rotation
        void CheckRotation(){
            CheckPlayerRotation();
            CheckCamRotation();
        }
        void Rotate(){
            RotatePlayer();
            RotateCamera();
        }
        void CheckPlayerRotation(){
            float yRotation = Input.GetAxis("Mouse X");
            rotateValue = new Vector3(transform.localEulerAngles.x, yRotation ,transform.localEulerAngles.z)*turningAroundSensitevity;
        }
        void RotatePlayer(){
                   
            if(rotateValue != Vector3.zero){
                rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotateValue));
            }                    
        }
        void CheckCamRotation(){
            camRotationUpAndDown = Input.GetAxis("Mouse Y")*turningAroundSensitevity;
        }
        void RotateCamera(){
            if(playerCameraView != null){
                currentCamRotationUpAndDown -= camRotationUpAndDown;
                playerCameraView.transform.localEulerAngles = new Vector3 (Mathf.Clamp(currentCamRotationUpAndDown,-85,85), playerCameraView.transform.localEulerAngles.y , playerCameraView.transform.localEulerAngles.z);

            }else{
                Debug.LogError("Please add the camera component");
            }
        }

        #endregion
        #endregion
    
    }

}
