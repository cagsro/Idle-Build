using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    TransporterController playerManager;
    public static Brick instance;
    public GameObject prefab;
    public GameObject Parent;
    public float xPos;
    public float zPos;
    public List<Transform> brickPieces = new List<Transform>();
    private void Awake()

    {        
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnBrick()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded) // Oyun baslamadiysa veya bittiyse
        {
            return;
        }
        xPos=  Random.Range(-8f,-1f);
        zPos=  Random.Range(-12f,-9f);
        Instantiate(prefab,new Vector3(xPos,0.2f,zPos),Quaternion.identity,Parent.transform);
        AddList();
    }

    public void AddList(){
    playerManager = FindObjectOfType<TransporterController>();
    brickPieces.Add(transform.GetChild(transform.childCount-1).transform);
    }
}
