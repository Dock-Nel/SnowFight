using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialPrompt : MonoBehaviour
{
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0f;

        if (Input.GetKey("return"))
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }
    }
}
