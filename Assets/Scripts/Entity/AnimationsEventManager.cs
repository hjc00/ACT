using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEventManager : MonoBehaviour
{
    public Transform camHandle;
    private Animator anim;
    private Rigidbody rigid;
    private PlayerController playerController;

    [SerializeField]
    private GameObject swordModel;
    //[SerializeField]
    //private Collider[] cols;
    private Collider col;
    public CapsuleCollider defenseCol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponentInParent<Rigidbody>();
        swordModel = transform.DeepFind("weapon").gameObject;
        //cols = swordModel.GetComponents<Collider>();
        col = swordModel.GetComponent<BoxCollider>();
    }

    public void SetPlayerController(PlayerController p)
    {
        playerController = p;
    }

    private void ResetTrigger(string name)
    {
        anim.ResetTrigger(name);
    }

    private void ChangeTimeScale()
    {
        Time.timeScale = 0.5f;
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }

    //打开武器的触发器
    private void WeaponEnable()
    {
        //foreach (var col in cols)
        //{
        //    col.enabled = true;
        //}
        col.enabled = true;
        playerController.PlayAtkSound();
    }

    private void WeaponDisable()
    {
        //foreach (var col in cols)
        //{
        //    col.enabled = false;
        //}
        col.enabled = false;
    }

    private void EnableDefenseCol()
    {
        defenseCol.enabled = true;
    }

    private void DisableDefenseCol()
    {
        defenseCol.enabled = false;
    }


    private void OnAnimatorMove()
    {
        rigid.position += anim.deltaPosition * 0.5f;
    }




}
