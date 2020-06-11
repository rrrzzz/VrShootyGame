using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMeta : MonoBehaviour
{
    private CurrentPlayerController _co;
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        _co = transform.parent.GetComponent<CurrentPlayerController>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _co.speed = 0;
            _co.winText.enabled = true;
        }
        if (other.transform.CompareTag("Bullet")) SceneManager.LoadScene(0);
    }
}
