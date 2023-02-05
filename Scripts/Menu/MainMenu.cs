using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject button;
    public GameObject canvas;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        button.SetActive(false);
    }
    public void BackLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        button.SetActive(false);
    }
    // Start is called before the first frame update
    public void ExitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        canvas.SetActive(false);
    }


}
