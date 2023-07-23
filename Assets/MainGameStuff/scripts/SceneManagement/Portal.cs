using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    

    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player")
    {
       StartCoroutine(Transition());
    }
   }
   private IEnumerator Transition(){
    DontDestroyOnLoad(gameObject);
    yield return  SceneManager.LoadSceneAsync(sceneToLoad);
    print("Scene Loaded");
    Destroy(gameObject);
   }
}
