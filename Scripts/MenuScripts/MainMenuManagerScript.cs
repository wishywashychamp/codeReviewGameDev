using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManagerScript : MonoBehaviour
{
    public GameObject levelsMenu;     // Variable to store the Level Menu
    public GameObject infoMenu;       // Variable to store the Information Menu
    public GameObject settingsMenu;   // Variable to store the Settings Menu
    public GameObject messagesMenu;   // Variable to store the Settings Menu
    public GameObject towersMenu;     // Variable to store the Towers Menu


    //**********************    FullScreen Toggle settings *********************// 
    // Note following function takes effect only if the game is built, in the editor mode it is not going to take effect
    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }


    //**********************    Quality settings *********************// 
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    //**********************    Audio Slider settings *********************// 
    public AudioMixer audioMixer;

    public void SetVolume(float volume) {
        audioMixer.SetFloat("MyExposedParam", volume); // MyExposedParam has been set on main mixer of the game
    }
    
    
    
    //**********************    Levels menu   *********************// 
    public void ActivateLevelsMenu() {
        levelsMenu.SetActive(true);
    }

    public void DisableLevelsMenu()
    {
        levelsMenu.SetActive(false);
    }


    //**********************    Info menu   *********************// 
    public void ActivateInfoMenu()
    {
        infoMenu.SetActive(true);
    }

    public void DisableInfoMenu()
    {
        infoMenu.SetActive(false);
    }


    //**********************    Settings menu   *********************// 
    public void ActivateSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void DisableSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    //**********************    Messages menu   *********************// 
    public void ActivateMessagesMenu()
    {
        messagesMenu.SetActive(true);
    }

    public void DisableMessagesMenu()
    {
        messagesMenu.SetActive(false);
    }

    //**********************    Towers menu   *********************// 
    public void ActivateTowersMenu()
    {
        towersMenu.SetActive(true);
    }

    public void DisableTowersMenu()
    {
        towersMenu.SetActive(false);
    }



    //**********************    Load Levels    *********************// 
    // Following function loads the Level 1 because its index in project settings is 3
    public void LoadLevel1Scene()
    {
        SceneManager.LoadScene(1);
    }

    // Following function loads the Level 1 because its index in project settings is 3
    public void LoadLevel2Scene()
    {
        SceneManager.LoadScene(2);
    }

    // Following function loads the Level 1 because its index in project settings is 3
    public void LoadLevel3Scene()
    {
        SceneManager.LoadScene(3);
    }


    //**********************    Exit Application    *********************// 
    // Following function quits the game (Note it does not work inside the Editor)
    public void Quit()
    {
        Application.Quit();
    }

}
