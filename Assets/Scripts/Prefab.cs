using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Prefab : MonoBehaviour
{
    public static Prefab instance;

    public List<GameObject> brickList;
    public List<GameObject> sortedBrickList;

    void Awake() 
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        foreach(GameObject cube in brickList)
        {
            cube.SetActive(false);
        }
        sortedBrickList = brickList.OrderBy(platform => platform.transform.position.y).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
