using UnityEngine;

namespace TestTask.Attribute
{
    public class DestroyAfterCertainTime : MonoBehaviour
    {
        [SerializeField] float time;
        private void Awake()
        {
            Destroy(this.gameObject, time);
        }
    }
}

