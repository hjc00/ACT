using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleManager : BattleManager
{

    private PlayerController playerController;
    //public Collider DefenseCol { get; set; }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "weapon")
        {
            EZCameraShake.CameraShaker.Instance.ShakeOnce(1.5f, 15f, 0.1f, 0.5f);
            this.transform.root.GetComponent<Rigidbody>().AddForce(-transform.forward * 500, ForceMode.Acceleration);

            Vector3 selfCenter = defenseCol.bounds.center;
            Vector3 closestPointOnBounds = other.ClosestPointOnBounds(selfCenter);
            playerController.PlayBloodFx(closestPointOnBounds);

            if (other.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            {
                ChangeAmountToFallDown(-10);
                playerController.DoDamage(10);

            }
            else if (other.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("sattack"))
            {
                ChangeAmountToFallDown(-20);

                playerController.DoDamage(20);
            }

            playerController.SetTrigger("hit");
            playerController.PlayHitSound();

            if (amountToFallDown <= 0)
            {
                playerController.SetTrigger("fallDown");
                amountToFallDown = 50;
            }
        }
    }




    //对外接口
    public void SetPlayerController(PlayerController p)
    {
        playerController = p;
    }

    public void ChangeAmountToFallDown(int amount)
    {
        amountToFallDown += amount;
    }

    public void EnableCollider()
    {
        defenseCol.enabled = true;
    }

    public void DisableCollider()
    {
        defenseCol.enabled = false;
    }
}
