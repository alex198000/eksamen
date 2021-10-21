using UnityEngine;

namespace Levels
{
    public class ButtonsControl : MonoBehaviour
    {
        [SerializeField] MyPlayerMovement myPlayerMovement;
        [SerializeField] PlayerAttack playerAttack;
        void Update()
        {
            MooveBut();
        }
        public void MooveBut()
        {
            if (Input.GetButton("Jump"))
            {
                myPlayerMovement.Jump();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                myPlayerMovement.LeftMove();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                myPlayerMovement.StopMove();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                myPlayerMovement.RightMove();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                myPlayerMovement.StopMove();
            }
            if (Input.GetKey(KeyCode.Q))
            {
                playerAttack.Dubin();
            }
            if (Input.GetKey(KeyCode.E))
            {
                playerAttack.Fire();
            }
        }
    }
}