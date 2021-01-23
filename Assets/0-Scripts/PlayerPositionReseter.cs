using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionReseter : MonoBehaviour {
    // 1. oyuncunun bu objenin icinden gectigini tespit et
    // 2. oyuncuyu baslangic noktasina geri gotur
    // 3. animasyon degerlerini sifrla

    // Transform playerInitialParent;
    // Vector3 playerInitialLocalPosition;
   

    // private void Start() {
    //     // player kimse onu tespit et
    //     GameObject player = GameObject.FindGameObjectWithTag("Player");
    //     // playerin konumunu bir degiskende sakla (ve bu degeri hic degistirme) (bu degiskeni Start disinda mi kullanacagim? => evet)

    //     playerInitialParent = player.transform.parent;
    //     playerInitialLocalPosition = player.transform.localPosition;
        
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Player") {
            // other.transform.SetParent(playerInitialParent);
            // other.transform.localPosition = playerInitialLocalPosition;


            // 2. yontem (sahneyi yeniden yukleyerek resetleme)
            // SceneLoader scriptini bul
            // LoadScene komutunu cagir
            // GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene()
            // GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene()
            SceneLoader.instance.Reload();

        }
    }


}
