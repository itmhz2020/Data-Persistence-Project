using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public TMP_InputField userInput;
    public TMP_Text bestScoreText;
    public string playerName;
    public int playerHieghScore = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        LoadPlayerData();
        userInput.text = playerName;
    }

    public void GameStart()
    {
       
        playerName = userInput.text;
        
        SceneManager.LoadScene(1);

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void UpdatePlayerData()
    {
        bestScoreText.text = "Best Score: "+ playerName + " :"+ playerHieghScore;
    }

    public void SavePlayerData(int hieghScore)
    {
        Player playerData = new Player();
        playerData.playerName = playerName;
        playerData.playerHieghScore = hieghScore;

        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData.json";
        File.WriteAllText(path,json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        if (File.Exists(path))
        { 
            string json = File.ReadAllText(path);
            Player playerData = JsonUtility.FromJson<Player>(json);
            playerName = playerData.playerName;
            playerHieghScore = playerData.playerHieghScore;
        } 
    }

    class Player
    {
        public string playerName;
        public int playerHieghScore;
    }
}
