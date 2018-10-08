using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected Animator anim;

   // protected PlayerBattleManager battleManager;
    protected AudioManager audioManager;
    protected AnimationsEventManager animationsEventManager;
    protected FxManager fxManager;

    public virtual void Init()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public virtual void SetTrigger(string name)
    {

    }

    public virtual void PlayAtkSound()
    {

    }


    public virtual void PlayHitSound()
    {

    }

    public virtual void PlayBloodFx(Vector3 pos)
    {

    }

    public virtual void DoDamage(float amount)
    {
        Debug.Log("base");
    }
}
