using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager instance;

    private void Awake() {
        if (DataManager.instance==null) {
            DataManager.instance = this;
        } else {
            if (DataManager.instance!=this) {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }// singleton

    public int playerScore;
    public List<int> collectedKeys = new List<int>();

    public void IncreasePlayerScore() {
        playerScore++;
        UIManager.instance.UpdatePlayerScore();
    }

    public void ResetData() {
        playerScore = 0;
    }

    public void AddToCollectedKeys(int aKeyId) {
        collectedKeys.Add(aKeyId);
    }
    public void ClearCollectiblesData() {
        collectedKeys.Clear();
    }

    public void SaveGame() {
        // currentSceneName
        // playerScore
        // collectedKeys

        string currrentSceneName = SceneLoader.instance.GetSceneName();
        PlayerPrefs.SetString("currentLevel", currrentSceneName);
        PlayerPrefs.SetInt("playerScore", playerScore);

        string collectedKeyIds = "";
        for (int i=0; i<collectedKeys.Count; i++) {
            collectedKeyIds+=collectedKeyIds[i]+",";
        }// 0,2,4,7,
        PlayerPrefs.SetString(currrentSceneName+"_CollectedKeys", collectedKeyIds); // Scene1_CollectedKeys // Scene2_CollectedKeys

        // player.transform.position
        // player.transform.rotation
        // flipState

        // JsonUtility
        // Json script
        // key - value

        // sceneName:Scene1
        //
        
    }

    public void LoadGame() {
        // kayitli level adini al
        string currentSceneName = PlayerPrefs.GetString("currentLevel");

        // player score'u al ve set et
        playerScore = PlayerPrefs.GetInt("playerScore");

        // collected key idlerini al
        string savedIdsString = PlayerPrefs.GetString(currentSceneName+"_CollectedKeys");
        // 0,2,4,7,
        // 0[,]2[,]4[,]7[,]
        // 0
        // 2
        // 4
        // 7
        // null
        string[] savedIdsSstringSplitted = savedIdsString.Split(',');
        for (int i=0; i<savedIdsSstringSplitted.Length; i++) {
            int parsedInt = 0;
            if (int.TryParse(savedIdsSstringSplitted[i], out parsedInt)) {
                collectedKeys.Add(parsedInt);
            } else {
                Debug.LogWarning("Collected key id could not be parsed as integer!");
            }
        }


        // leveli yukle
        SceneLoader.instance.LoadScene(currentSceneName, true);


    }

}
