using UnityEngine;

namespace TestTask.Core
{
    public class CameraControl : MonoBehaviour
    {
        [Header("Attach the player component.")]
        [SerializeField] Transform player;

        [Header("Height of camera from the players position.")]
        [SerializeField] float offsetZ;
        public float orthographicSize = 5;
        public float aspect = 1.33333f;
        void Start()
        {
           
            aspect = (float)Screen.width / (float)Screen.height;
            Camera.main.projectionMatrix = Matrix4x4.Ortho(
                 -orthographicSize * aspect, orthographicSize * aspect,
                 -orthographicSize, orthographicSize,
                  Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }
        private void LateUpdate()
        {
          
            transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + offsetZ);
        }
    }
}

