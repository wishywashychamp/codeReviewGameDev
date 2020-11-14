using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public WaypointScript firstWaypoint;  // Storing the first waypoint 
    public GameObject loosingScreen;      // Variable to stoer the losingScreen so it can be shown when player does not have health left
    public GameObject winningScreen;      // Variable to store the screen displayed when the player 
    private HealthScript playerHealth;    // Varaible to referenece the players health

  
    void Start()
    {
        // Getting the reference to the Player's health by the script 
        playerHealth = FindObjectOfType<HealthScript>();

        // Getting the reference to the Spawner`s position with its name
        spawner = GameObject.Find("SpawningPoint").transform;

        // Calling the below function to start the wave coroutine
        StartCoroutine(WavesSpawner());
    }

    void Update()
    {
        // Checking if player presses escape button, if so pausing the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }       
    }

    // Following function activates eathier winning or loosing screen depending the boolean value when it is called
    // The winning and loosing screens are disabled by default (unchecked from the inspector)
    private void GameOver(bool playerHasWon)
    {
        // Checking if the player has won from the passed parameter
        if (playerHasWon)
        {
            // Activating the winning screen
            winningScreen.SetActive(true);
        }
        else
        {
            // Activating the loosing screen
            loosingScreen.SetActive(true);
        }

        // Freezing the game time in order to stop the eecution of functions
        Time.timeScale = 0;
    }

    // Following function decrements the number of EnemyShips to defeat (called in DamagePlayersHealth function below)
    public void DecrementEnemyShipNumber()
    {
        numberOfEnemyShipsToDefeat--;
    }


    // Following function applies damages to the player`s health and manages gameOver state if no health is left
    public void DamagePlayersHealth(int damage)
    {
        // Applying damage to the player and getting a boolean to see if the player has no health left
        bool IsOutOfHealth = playerHealth.ApplyDamage(damage);

        //If the player is out of health, the GameOver function is called 
        if (IsOutOfHealth)
        {
            GameOver(false);
        }

        // Calling above function to decrement thenumber of EnemyShips to defeat
        DecrementEnemyShipNumber();
    }




    //*********************  Placing Tower Part   ******************//

    private bool _isPointerOnAllowedArea = true; // Variable to store if the mouse is hovering on top of the allowed area

    // Following function returns true if the mouse is hovering over on allowed place (Called in OnMouseEnter and OnMouseExit functions)
    public bool isPointerOnAllowedArea()
    {
        return _isPointerOnAllowedArea;
    }

    // Following function sets _isPointerOnAllowedArea to true when the mouse is hovering over on allowed area
    void OnMouseEnter()
    {
        _isPointerOnAllowedArea = true;
    }

    // Following function sets _isPointerOnAllowedArea to false when the mouse is hovering over on not allowed area
    void OnMouseExit()
    {
        // Set that the mouse is not hovering anymore an area where placing
        _isPointerOnAllowedArea = false;
    }



    //**************      EnemyShip Wave Controllong Part     ***********************//

    private int numberOfEnemyShipsToDefeat;     // Storing how many EnemyShips left to defeat
    public GameObject enemyShipPrefab;          // The prefab that has to be spawned as enemy (EnemyShip Prefab
    private Transform spawner;                  // Variable to store where EnemyShip has to be 
    public int numberOfWaves;                   // The number of waves that the player has to face in this level
    public int numberOfEnemyShipsPerWave;       // The number of EnemyShips that the player has to face per wave.


    // Following coroutine spawns different waves of EnemySHips
    private IEnumerator WavesSpawner()
    {
        // Making this coroutine wait 27seconds, so that enemies start to spawn when the background music gains intensity
        yield return new WaitForSeconds(10);

        // For each wave calling below EnemyShipSpawner coroutine to handle each single wave
        for (int i = 0; i < numberOfWaves; i++)
        {
            yield return EnemyShipSpawner();
            
            // Increasing the number of EnemySHips so that player faces more Enemy SHips in the next wave
            numberOfEnemyShipsPerWave += 5;
        }

        // If the Player won all the waves, calling the GameOver function in winning mode
        GameOver(true);
    }

    // Coroutine that spawns the enemyShips for a single wave, and waits until all the EnemyShips are destroyed
    private IEnumerator EnemyShipSpawner()
    {
        // Initializing the number that needs to be defeated for this wave
        numberOfEnemyShipsToDefeat = numberOfEnemyShipsPerWave;

        // Progressively spawning the EnemyShips
        for (int i = 0; i < numberOfEnemyShipsPerWave; i++)
        {
            // Instantiating a EnemyShip at the Spawner position
            Instantiate(enemyShipPrefab, spawner.position, Quaternion.identity);

            // How many time in between there should be when instantiating EnemyShips 
            //float ratio = (i * 1f) / (numberOfEnemyShipsPerWave - 1);
            //float timeToWait = Mathf.Lerp(3f, 5f, ratio) + Random.Range(0f, 2f);

            // How many time in between there should be when instantiating EnemyShips, unlike above commented time now it has fixed time 1 second 
            float timeToWait = 1;
            yield return new WaitForSeconds(timeToWait);
        }

        // Once all the EnemySHips for this wave are spawned, waiting until all of them are defeated
        yield return new WaitUntil(() => numberOfEnemyShipsToDefeat <= 0);
    }


    //*************** PAUSE MENU ***************************//
    public static bool IsGamePaused = false;
    public GameObject pauseMenu;

    // Following funtion resumes the game, it is public because it will be accessed by the Reume button in the UI
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Normal speed for the game
        IsGamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Freezing the game
        IsGamePaused = true;
    }

    // Following function loads the Main Menu because its index in project settings is 0
    public void LoadMenuScene()
    {
        Time.timeScale = 1; // Setting the timescale to 1 because when the pause menu was opened it was set to 0
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1; // Setting the timescale to 1 because when the pause menu was opened it was set to 0
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        Debug.Log(nextSceneIndex);
        // Checking if the there is a next scene
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    // Following function quits the game (Note it does not work inside the Editor)
    public void Quit()
    {
        Application.Quit();
    }

}
