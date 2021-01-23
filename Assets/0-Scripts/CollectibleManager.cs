using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour {

    public int id;

    public void Start() {
        if (PlayerPrefs.HasKey("TakenKeys"+id)) {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Player") {
            // playerin key sayisini artir
            // other.GetComponent<PlayerController>().numOfKeys++;
            UIManager.instance.IncreasePlayerScore();
            PlayerPrefs.SetInt("TakenKeys"+id,0);
            
            // bu key'i yok et
            // gameObject.SetActive(false);
            Destroy(gameObject);

        }
    }
}
