using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{

    public GameObject bloodFx;
    private EntityController entityController;
    // Use this for initialization
    void Start()
    {
        bloodFx = Instantiate(bloodFx, transform.position, transform.rotation);
        bloodFx.SetActive(false);
    }

    void Update()
    {

    }

    public void SetEntityController(EntityController p)
    {
        entityController = p;
    }

    public void PlayBloodFx(Vector3 pos)
    {
        bloodFx.SetActive(true);
        bloodFx.transform.position = pos;
        bloodFx.GetComponent<ParticleSystem>().Play();
    }
}
