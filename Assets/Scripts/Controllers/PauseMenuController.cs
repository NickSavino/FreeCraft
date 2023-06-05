using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenuController : MonoBehaviour
{

    GameObject pauseMenu;
    GameObject hud;

    Button saveGame;

    


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        hud = GameObject.FindGameObjectWithTag("HUD");


        saveGame = pauseMenu.transform.Find("ResumeGame").GetComponent<Button>();
        saveGame.onClick.AddListener(ResumeGame);
        pauseMenu.SetActive(false);
    }


    void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }


    public void PauseGame()
    {

        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                hud.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                hud.SetActive(true);
                Time.timeScale = 1;
            }
 
        }
    }




}
