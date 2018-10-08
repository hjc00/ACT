using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject g = GameManager.Instance.LoadResources<GameObject>(GameDefine.goblinPath);
        Instantiate(g, this.transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
