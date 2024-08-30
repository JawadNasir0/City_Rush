using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject gameoverScreen;
    public bool isGameActive;
    public GameObject coinsCount;
    public static bool isGameRestarted =false;
    public float rotationSpeed =120f;
    private GameObject[] coins;
    

    // Start is called before the first frame update
    void Start()
    {
        // If the game was restarted, start the game automatically
        if (isGameRestarted)
        {
            StartGame();
        }
    coins= GameObject.FindGameObjectsWithTag("Coin");
    }
    public void StartGame()
    {
        titleScreen.SetActive(false);
        coinsCount.SetActive(true);
        isGameActive = true;
        isGameRestarted = false;
        
    }

    public void GameOver()
    {
        isGameActive = false;
        StartCoroutine(WaitSeconds());
        
    }
    public void RestartGame()
    {
        isGameRestarted=true;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         
    }
    public void TitleScreen()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator WaitSeconds()
    {

        yield return new WaitForSeconds(3.5f);
        gameoverScreen.gameObject.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        if(isGameActive){
        RotateAllCoins();
        }
    }
    void RotateAllCoins()
    {
        foreach (GameObject coin in coins)
        {
            if(coin!=null){
            coin.transform.Rotate(Vector3.up * rotationSpeed*Time.deltaTime);
            }
        }
    }
}
