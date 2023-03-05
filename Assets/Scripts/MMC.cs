using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMC : MonoBehaviour
{
    public GameObject MenuCamera;
    public GameObject PlayerCamera;
    public GameObject[] PauseMenu;
    public bool TogglePauseMenu = false;
    public AudioSource BGM;


    void Awake()
    {
        MenuCamera.SetActive(true);
        PlayerCamera.SetActive(false);
        MouseEnable();
        BGM.Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && TogglePauseMenu == false)
        {
            MainMenu();
            PauseGame();
            TogglePauseMenu = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && TogglePauseMenu == true)
        {
            StartGame();
            ResumeGame();
            TogglePauseMenu = false;
        }

        Debug.Log(TogglePauseMenu);
    }

    public void StartGame()
    {
        MenuCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        MouseDisable();
    }

    public void MainMenu()
    {
        MenuCamera.SetActive(true);
        PlayerCamera.SetActive(false);
        MouseEnable();
    }

    public void MouseEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MouseDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        for(int i = 0; i < PauseMenu.Length; i++)
        {
            PauseMenu[i].SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        for(int i = 0; i < PauseMenu.Length; i++)
        {
            PauseMenu[i].SetActive(false);
        }
        Time.timeScale = 1;
    }
}
