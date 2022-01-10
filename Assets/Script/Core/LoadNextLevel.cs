using UnityEngine;

namespace TestTask.Core
{
    public class LoadNextLevel : MonoBehaviour
    {

        [SerializeField]public GameObject gate;
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")){
                gate.SetActive (true);
              GameHandler.instance.NextLevelGame();  
            }
        }
    }
}
