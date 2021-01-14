using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour {

    [Header("Up Movement Parameters")]
    public float upDistance = 10;
    public float upMoveSpeed = 0.1f;
    public bool turnUpWhenAtInitialPosition;

    [Header("Right Movement Parameters")]
    public float rightDistance = 10;
    public float rightMoveSpeed = 0.1f;
    public bool turnRightWhenAtInitialPosition;

    private Vector3 initialPosition;
    private Vector3 upMagnitude;
    private float currentMagnitude;




    // Start is called before the first frame update
    void Start() {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {

        // up down motion
        // yon belirten bir vektor * hareket genligi
        transform.localPosition += transform.up * upMoveSpeed + transform.right * rightMoveSpeed;
        currentMagnitude = (transform.localPosition - initialPosition).magnitude;

        if ( currentMagnitude > upDistance  ) {
            upMoveSpeed *= (-1);
        }
        if (turnUpWhenAtInitialPosition) {
            if (transform.localPosition.y < initialPosition.y) {
                upMoveSpeed *= (-1);
            }
        }

        // right left motion
        // transform.localPosition += transform.right * rightMoveSpeed;
        if ( currentMagnitude > rightDistance  ) {
            rightMoveSpeed *= (-1);
        }
        if (turnRightWhenAtInitialPosition) {
            if (transform.localPosition.x < initialPosition.x) {
                rightMoveSpeed *= (-1);
            }
        }
    }
}
