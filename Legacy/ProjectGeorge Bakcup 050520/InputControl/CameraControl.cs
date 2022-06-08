using UnityEngine;

namespace ProjectGeorge.InputControl
{
    [RequireComponent(typeof(Camera))]
    public class CameraControl : MonoBehaviour
    {
        [Header("Movement Speeds")]
        [Space]
        public float panSpeed;

        void Update()
        {
            Vector3 panMovement = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                panMovement += Vector3.forward * panSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                panMovement -= Vector3.forward * panSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                panMovement += Vector3.left * panSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                panMovement += Vector3.right * panSpeed;
                //pos.x += panSpeed * ;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                panMovement += Vector3.up * panSpeed;
            }
            if (Input.GetKey(KeyCode.E))
            {
                panMovement += Vector3.down * panSpeed;
            }
            gameObject.transform.position += panMovement;
        }
    }
}