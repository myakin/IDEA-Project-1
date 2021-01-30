using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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


        /*
        string currrentSceneName = SceneLoader.instance.GetSceneName();
        PlayerPrefs.SetString("currentLevel", currrentSceneName);
        PlayerPrefs.SetInt("playerScore", playerScore);

        string collectedKeyIds = "";
        for (int i=0; i<collectedKeys.Count; i++) {
            collectedKeyIds+=collectedKeyIds[i]+",";
        }// 0,2,4,7,
        PlayerPrefs.SetString(currrentSceneName+"_CollectedKeys", collectedKeyIds); // Scene1_CollectedKeys // Scene2_CollectedKeys
        */


        /*
        // json method
        // currentSceneName -> string
        // playerScore -> int
        // collectedKeys -> List<int>
        SaveData myData = new SaveData();
        myData.sceneName = SceneLoader.instance.GetSceneName();
        myData.score = playerScore;
        myData.keys = collectedKeys;

        PlayerPrefs.SetString("MySaveData", JsonUtility.ToJson(myData));
        /*
        sasaS
            {
            "scenName":"deger"
            "score":"deger"
            "keys":"deger"
        }
        */


        // save into binary file
        /*
        string path = Application.persistentDataPath + "/mySaveData.ideaprj1sav";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fileStream, JsonUtility.ToJson(myData));

        fileStream.Close();
        */
        string filePath = Application.persistentDataPath + "/savedSceneData_"+SceneLoader.instance.GetSceneName()+".ideaprj1sav"; //savedSceneData_Scene1.ideaprj1sav    savedSceneData_Scene2.ideaprj1sav    
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fileStream, collectedKeys);
        fileStream.Close();


        string filePath2 = Application.persistentDataPath + "/playerData.ideaprj1sav";
        FileStream fileStream2 = new FileStream(filePath2, FileMode.Create);
        BinaryFormatter bf2 = new BinaryFormatter();
        bf.Serialize(fileStream2, SceneLoader.instance.GetSceneName());
        bf.Serialize(fileStream2, playerScore);
        fileStream2.Close();



        // FileStream -> yol, acma modu => sistemde bir dosya aciyor ve dinlemeye geciyor
        // BinaryFormatter -> benim datam bu bunu serialize et (binarye donustur) (su file streame yaz, data)



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

        /*
        PlayerPrefs methos
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
        */


        /*
        // json method
        SaveData myLoadData = new SaveData();
        myLoadData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("MySaveData"));

        string currentSceneName = myLoadData.sceneName;
        playerScore = myLoadData.score;
        collectedKeys = myLoadData.keys;
        */

           
        // binary method
        /*
        //SaveData myLoadData = new SaveData();
        string filePath = Application.persistentDataPath + "/savedSceneData.ideaprj1sav";
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        // string readData = (string)bf.Deserialize(fileStream);
        // myLoadData = JsonUtility.FromJson<SaveData>(readData);
        //string currentSceneName = myLoadData.sceneName;
        //playerScore = myLoadData.score;
        //collectedKeys = myLoadData.keys;

        

        string currentSceneName = (string)bf.Deserialize(fileStream);
        playerScore = (int)bf.Deserialize(fileStream);
        collectedKeys = (List<int>)bf.Deserialize(fileStream);

        fileStream.Close();
        */



        string filePath = Application.persistentDataPath + "/playerData.ideaprj1sav";
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        string currentSceneName = (string)bf.Deserialize(fileStream);
        playerScore = (int)bf.Deserialize(fileStream);
        fileStream.Close();

        string sceneDataPath = Application.persistentDataPath + "/savedSceneData_" + currentSceneName + ".ideaprj1sav";
        FileStream fileStream2 = new FileStream(sceneDataPath, FileMode.Open);
        BinaryFormatter bf2 = new BinaryFormatter();
        collectedKeys = (List<int>)bf2.Deserialize(fileStream2);
        fileStream2.Close();


        // leveli yukle
        SceneLoader.instance.LoadScene(currentSceneName, true);

    }

    public void LoadSceneData(string aSceneName)
    {
        string sceneDataPath = Application.persistentDataPath + "/savedSceneData_" + aSceneName + ".ideaprj1sav";
        FileStream fileStream2 = new FileStream(sceneDataPath, FileMode.Open);
        BinaryFormatter bf2 = new BinaryFormatter();
        collectedKeys = (List<int>)bf2.Deserialize(fileStream2);
        fileStream2.Close();
    }


    /*

    public void LoadData(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        // datami buraya variable icine atarim
        fileStream.Close();

        return ...
    }
    */



}
