using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    private void Awake() {
        UIManager.instance = this;
    }


    // Variables & references
    public Text scoreText;
    public int numOfKeys;

    private void Start() {
        numOfKeys = PlayerPrefs.GetInt("PlayerScore");
        scoreText.text = numOfKeys.ToString();
    }

    public void IncreasePlayerScore() {
        numOfKeys++;
        scoreText.text = numOfKeys.ToString();
        PlayerPrefs.SetInt("PlayerScore",numOfKeys);
    }



}
