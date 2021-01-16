using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedController : MonoBehaviour {
    public float animationSpeed = 1;


    private void Start() {
        GetComponent<Animator>().SetFloat("speed", animationSpeed);
    }
}
