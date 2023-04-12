using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{    
    [SerializeField]
    private AsteroidGenerator asteroidGenerator;

    [SerializeField]
    private TurretController turretController;

    [SerializeField]
    public int playerLives = 5;

    [SerializeField]
    public TextMeshProUGUI hp;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        asteroidGenerator.StartGenerating();
        turretController.Initialize();
    }
    
    /// <summary>
    /// Called whenever asteroid hit the ground trigger.
    /// </summary>
    /// <param name="asteroid"></param>
    
    public void AsteroidHitTheGround(AsteroidInfo asteroid)
    {
        playerLives--;

        Debug.Log("Player lost a life. Current count: " + playerLives);
        hp.text = "  : " + playerLives.ToString();
        if (playerLives <= 0)
        {
            PlayerLostAllLives();
        }
    }

    /// <summary>
    /// Called when player lost his lives.
    /// </summary>
    public void PlayerLostAllLives()
    {
        Debug.Log("Game over! You've lost all lives!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void AddHp(){
        Debug.Log("HP+?" );
        int test=Random.Range(0, 4);
        if(test==0){
            Debug.Log("HP+" );
            playerLives++;
            hp.text = "  : " + playerLives.ToString();
        }
    }
}
