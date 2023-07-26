using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class QuitListener : MonoBehaviour
{
    [SerializeField] private GameObject targetPanel;
    private bool isPaused = false;

    void Awake()
    {
        targetPanel.SetActive(false);
    }

    void Update()
    {
        if(CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            if (!isPaused && !targetPanel.activeSelf)
            {
                PauseGame();
            } else {
                Cancel();
            }
        }
    }

    public void Quit ()
    {
        Application.Quit();
    }

    public void Cancel()
    {
        isPaused = false;
        targetPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        targetPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
