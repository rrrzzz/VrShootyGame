using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentPlayerController : MonoBehaviour
{
    public Text winText;
    public float speed = 2;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }
}