using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinerController : MonoBehaviour
{
    NavMeshAgent nmesh;
    Animator MinerAnimator;
    public static MinerController instance;
    TransporterController playerManager;
    public GameObject prefab;
    public Transform Ore;

    public float stop = 2.4f;
    public float xPos;
    public float zPos;
        
    // Start is called before the first frame update
    void Start()
    {
        //xPos=Random.Range(-9f,-13f);
        //zPos=Random.Range(-9f,-13f);
        nmesh=GetComponent<NavMeshAgent>();
        MinerAnimator = GetComponent<Animator>();
        instance=this;
        StartCoroutine(Spawn());  
    }

    // Update is called once per frame
    void Update()
    {
        
        //nmesh.destination=(new Vector3(xPos,0f,zPos));
        float distanceToOre = Vector3.Distance(transform.position, Ore.position);
        nmesh.destination=(Ore.position);
        if (distanceToOre < stop)
        {
            MinerAnimator.SetTrigger("Mining");
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(GameManager.instance.waitfor);
        Brick.instance.SpawnBrick();
        StartCoroutine(Spawn());
    }

}
