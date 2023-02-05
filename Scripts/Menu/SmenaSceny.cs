using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmenaSceny : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Invoke("Smena", 1);

    }
    public void Smena()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
