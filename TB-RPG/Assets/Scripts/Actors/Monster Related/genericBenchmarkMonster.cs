using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class genericBenchmarkMonster : Monster {

    public genericBenchmarkMonster()
        : base("Generic", 1, 4, null, 
                new Resource[] {
                    new Resource(10),
                    new Resource(100)
                },
                new int[] { 5,5,5,5,5 })
    {


        GameObject imagePrefab = Resources.Load("demonSkull") as GameObject;
        image = GameObject.Instantiate(imagePrefab, imagePrefab.transform.position, imagePrefab.transform.rotation) as GameObject;
        image.transform.SetParent(GameObject.Find("BattleCanvas").transform, false);
        Debug.Log(image);
        


    }

}
