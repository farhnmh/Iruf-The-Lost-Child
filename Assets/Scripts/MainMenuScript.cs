using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] UnityEvent backMainMenu;
    [SerializeField] UnityEvent applySetting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("BackToMainMenu");
            backMainMenu.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("ApplySetting");
            applySetting.Invoke();
        }
    }

    public void MoveToGame()
    {
        Debug.Log("PlayTheGame");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QuitTheGame");
        Application.Quit();
    }
}
