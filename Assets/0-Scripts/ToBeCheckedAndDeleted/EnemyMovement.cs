using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Editor Parameters")]
    public float moveRange = 8;
    public float moveSpeed = 0.01f;

    [Header("Set at runtime by system")]
    public Vector3 startPos;
    public Vector3 moveDir;
    public Vector3 endPos;
    public float moveMultiplier = 1;


    private void Start() {
        startPos = transform.position;
        moveDir = -transform.right;
        endPos = startPos + (moveDir * moveRange);
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.x < endPos.x || transform.position.x > startPos.x) {
            moveMultiplier *= -1;
        }
        transform.position += moveDir * (moveSpeed * moveMultiplier);

    }

    public void SetEnemyData(Vector3 aCurrentPosition, float aRange, float aMultiplier, float aMoveSpeed, Vector3 aStartPos, Vector3 aDirection, Vector3 anEndPos) {
        moveRange = aRange;
        moveMultiplier = aMultiplier;
        moveSpeed = aMoveSpeed;
        startPos = aStartPos;
        moveDir = aDirection;
        endPos = anEndPos;
        transform.position = aCurrentPosition;
    }

}

[System.Serializable]
public class EnemyData {
    public Vector3 currentPosition;
    public float range;
    public float multiplier;
    public float speed;
    public Vector3 sPos;
    public Vector3 dir;
    public Vector3 ePos;

    public EnemyData() {}

    public EnemyData(Vector3 aCurrentPosition, float aRange, float aMultiplier, float aSpeed, Vector3 aStartPos, Vector3 aDirection, Vector3 anEndPos) {
        this.currentPosition = aCurrentPosition;
        this.range = aRange;
        this.multiplier = aMultiplier;
        this.speed = aSpeed;
        this.sPos = aStartPos;
        this.dir = aDirection;
        this.ePos = anEndPos;
    }

}
