using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{

    //private VisualElement e_single_player;
    //private VisualElement e_multi_player;
    //private VisualElement e_exit_game;

    VisualElement root;

    private Button single_player;
    private Button multi_player;
    private Button exit_game;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.SetCursor(Resources.Load<Texture2D>("cursor_regular"), new Vector2(0, 0), UnityEngine.CursorMode.Auto);

        this.root = this.GetComponent<UIDocument>().rootVisualElement;
        this.single_player = (Button) root.Query("SinglePlayer").First();

        this.single_player.clicked += () => {
            GoToSinglePlayer();
        };

        this.multi_player = (Button)root.Query("MultiPlayer").First();
        this.exit_game = (Button)root.Query("ExitGame").First();

        this.exit_game.clicked += () =>
        {
            ExitGame();
        };
    }

    void ExitGame()
    {
        Application.Quit();
    }


    void GoToSinglePlayer()
    {
        SceneManager.LoadScene("NickTestScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
