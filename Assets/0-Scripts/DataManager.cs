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


    void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) { // save enemy movement

            /*
            // SAVE SINGLE ENEMY
            EnemyMovement enemyMovement = GameObject.FindObjectOfType<EnemyMovement>();
            
            // EnemyData enemySaveData = new EnemyData();
            // enemySaveData.range = enemyMovement.moveRange;
            // enemySaveData.multiplier = enemyMovement.moveMultiplier;
            // enemySaveData.speed = enemyMovement.moveSpeed;
            // enemySaveData.sPos = enemyMovement.startPos;
            // enemySaveData.dir = enemyMovement.moveDir;
            // enemySaveData.ePos = enemyMovement.endPos;

            EnemyData enemySaveData = new EnemyData(enemyMovement.transform.position, enemyMovement.moveRange, enemyMovement.moveMultiplier, enemyMovement.moveSpeed, enemyMovement.startPos, enemyMovement.moveDir, enemyMovement.endPos);
            string enemyDataPath = Application.persistentDataPath + "/enemyData.enmdt";
            SaveData(enemySaveData, enemyDataPath);
            */


            // // SAVE MULTIPLE ENEMIES
            // EnemyMovement[] enemyMovements = GameObject.FindObjectsOfType<EnemyMovement>();
            // for (int i=0; i<enemyMovements.Length; i++) {
            //     EnemyData enemySaveData = new EnemyData(enemyMovements[i].transform.position, enemyMovements[i].moveRange, enemyMovements[i].moveMultiplier, enemyMovements[i].moveSpeed, enemyMovements[i].startPos, enemyMovements[i].moveDir, enemyMovements[i].endPos);
            //     string enemyDataPath = Application.persistentDataPath + "/enemyData"+ i +".enmdt"; // enemyData0.enmdt  enemyData1.enmdt  enemyData2.enmdt
            //     SaveData(enemySaveData, enemyDataPath);
            // }

            // // SAVE MULTIPLE ENEMIES - 2
            // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // for (int i=0; i<enemies.Length; i++) {
            //     EnemyMovement enemyMovement = enemies[i].GetComponent<EnemyMovement>();
            //     EnemyData enemySaveData = new EnemyData(enemyMovement.transform.position, enemyMovement.moveRange, enemyMovement.moveMultiplier, enemyMovement.moveSpeed, enemyMovement.startPos, enemyMovement.moveDir, enemyMovement.endPos);
            //     string enemyDataPath = Application.persistentDataPath + "/enemyData"+ i +".enmdt"; // enemyData0.enmdt  enemyData1.enmdt  enemyData2.enmdt
            //     SaveData(enemySaveData, enemyDataPath);
            // }

            // UPGRADE: SAVE ALL MOVING OBJECTS
            ObjectMovement[] objectsMovements = GameObject.FindObjectsOfType<ObjectMovement>();
            for (int i=0; i<objectsMovements.Length; i++) {
                MovingObjectSaveData saveData = new MovingObjectSaveData(objectsMovements[i].transform.position, objectsMovements[i].moveRange, objectsMovements[i].moveMultiplier, objectsMovements[i].moveSpeed, objectsMovements[i].startPos, objectsMovements[i].moveDir, objectsMovements[i].endPos);
                string path = Application.persistentDataPath + "/movingObjectData"+ i +".mvngobjdt";
                SaveData(saveData, path);
            }

        }
        if (Input.GetKeyDown(KeyCode.F2)) { // load enemy movement
            /*
            // LOAD SINGLE ENEMY
            EnemyData loadedEnemyData = new EnemyData();
            string enemyDataPath = Application.persistentDataPath + "/enemyData.enmdt";
            loadedEnemyData = LoadData(loadedEnemyData, enemyDataPath);

            EnemyMovement enemyMovement = GameObject.FindObjectOfType<EnemyMovement>();
            enemyMovement.SetEnemyData(loadedEnemyData.currentPosition, loadedEnemyData.range, loadedEnemyData.multiplier, loadedEnemyData.speed, loadedEnemyData.sPos, loadedEnemyData.dir, loadedEnemyData.ePos);
            */

            // // LOAD MULTIPLE ENEMIES
            // EnemyMovement[] enemyMovements = GameObject.FindObjectsOfType<EnemyMovement>();

            // for (int i=0; i<enemyMovements.Length; i++) {
            //     EnemyData loadedEnemyData = new EnemyData();
            //     string enemyDataPath = Application.persistentDataPath + "/enemyData"+ i +".enmdt"; // enemyData0.enmdt  enemyData1.enmdt  enemyData2.enmdt
            //     loadedEnemyData = LoadData(loadedEnemyData, enemyDataPath);

            //     enemyMovements[i].SetEnemyData(loadedEnemyData.currentPosition, loadedEnemyData.range, loadedEnemyData.multiplier, loadedEnemyData.speed, loadedEnemyData.sPos, loadedEnemyData.dir, loadedEnemyData.ePos);
            // }

            // // LOAD MULTIPLE ENEMIES - 2
            // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // for (int i=0; i<enemies.Length; i++) {
            //     EnemyData loadedEnemyData = new EnemyData();
            //     string enemyDataPath = Application.persistentDataPath + "/enemyData"+ i +".enmdt"; // enemyData0.enmdt  enemyData1.enmdt  enemyData2.enmdt
            //     loadedEnemyData = LoadData(loadedEnemyData, enemyDataPath);

            //     EnemyMovement targetEnemyMovementScript = enemies[i].GetComponent<EnemyMovement>();
            //     targetEnemyMovementScript.SetEnemyData(loadedEnemyData.currentPosition, loadedEnemyData.range, loadedEnemyData.multiplier, loadedEnemyData.speed, loadedEnemyData.sPos, loadedEnemyData.dir, loadedEnemyData.ePos);
            // }

            // UPGRADE: LOAD ALL MOVING OBJECTS
            ObjectMovement[] objectMovements = GameObject.FindObjectsOfType<ObjectMovement>();

            for (int i=0; i<objectMovements.Length; i++) {
                MovingObjectSaveData loadedData = new MovingObjectSaveData();
                string path = Application.persistentDataPath + "/movingObjectData"+ i +".mvngobjdt";
                loadedData = LoadData(loadedData, path);

                objectMovements[i].SetEnemyData(loadedData.currentPosition, loadedData.range, loadedData.multiplier, loadedData.speed, loadedData.sPos, loadedData.dir, loadedData.ePos);
            }
            
        }
    }


    public void SaveData<T>(T aSaveDataClass, string aPath) {
        FileStream stream = new FileStream(aPath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        string jsonString = JsonUtility.ToJson(aSaveDataClass);
        bf.Serialize(stream, jsonString);
        stream.Close();
    }
    public T LoadData<T>(T aDataClassToBeAssigned, string aPath) {
        if (File.Exists(aPath)) {
            FileStream stream = new FileStream(aPath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            string jsonString = (string)bf.Deserialize(stream);
            aDataClassToBeAssigned = JsonUtility.FromJson<T>(jsonString);
            stream.Close();
        }
        return aDataClassToBeAssigned;
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
