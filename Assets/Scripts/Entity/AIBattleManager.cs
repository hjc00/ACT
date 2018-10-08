using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBattleManager : BattleManager
{

    public float dectecFreq = 0.5f;  //侦测频率
    public float radius = 10f;
    public float angle = 45;
    public float attackPower = 20f; //攻击伤害

    public SphereCollider atkSphere;

    private float timer = 0;
    private Collider[] cols;
    private AIController aIController;
    private AIStateManager stateManager;
    // public Collider DefenseCol { get { return defenseCol; } }


    private void Start()
    {
        aIController = GetComponentInParent<AIController>();
        stateManager = GetComponentInParent<AIStateManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dectecFreq)
        {
            cols = Physics.OverlapSphere(transform.position + transform.forward.normalized * radius, radius, LayerMask.GetMask("Player"));
            if (cols.Length > 0)
            {
                if (aIController.PlayerGo == null)
                {
                    aIController.SetPlayerGo(cols[0].gameObject);
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward.normalized * radius, radius);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerWeapon")
        {
            if (aIController.dead == false)
            {
                Vector3 selfCenter = defenseCol.bounds.center;
                Vector3 closestPointOnBounds = other.ClosestPointOnBounds(selfCenter);
                aIController.PlayBloodFx(closestPointOnBounds);

                //普通攻击
                if (!stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hit"))
                {
                    aIController.SetTrigger("hit");
                    aIController.PlayHitSound();
                    DoDamage(other.transform.root.GetComponent<PlayerController>().attackPower += Random.Range(-2, 3));
                    UpdateAngryAmount(1);
                }

                //重击
                if (other.transform.root.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("sattack"))
                {
                    aIController.SetTrigger("heavyHit");
                    //镜头抖动
                    EZCameraShake.CameraShaker.Instance.ShakeOnce(2f, 15, 0.1f, 0.5f);
                    //击飞
                    //this.transform.root.GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * 180, ForceMode.Impulse);
                    //this.transform.root.GetComponent<Rigidbody>().AddExplosionForce(200f, transform.forward, 2f, 1f, ForceMode.VelocityChange);
                    //this.transform.root.GetComponent<Rigidbody>().AddExplosionForce(200f,transform.forward,2f);
                    StartCoroutine(HitBack());
                    aIController.PlayHitSound();

                    DoDamage(other.transform.root.GetComponent<PlayerController>().heavyAttackPower += Random.Range(0, 5));
                    UpdateAngryAmount(2);
                }
                //stateManager.gameObject.transform.LookAt(other.transform.root.position);
                StartCoroutine(TurnAround(other.transform.root.position));
                StartCoroutine(Freeze(0.5f));
            }
        }
    }

    public void DoDamage(float amount)
    {
        aIController.DoDamage(amount);
    }

    public void UpdateAngryAmount(float amount)
    {
        stateManager.UpdateAngryAmount(amount);
    }

    IEnumerator Freeze(float time)
    {
        if (stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            stateManager.GetComponent<Animator>().speed = 0.2f;
            while (time >= 0)
            {
                time -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            stateManager.GetComponent<Animator>().speed = 1;
        }
    }

    IEnumerator HitBack()
    {
        float amount = 10;
        while (amount >= 0)
        {
            amount -= 1.5f;
            yield return new WaitForFixedUpdate();
            this.transform.root.GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * amount, ForceMode.Impulse);
        }
    }

    IEnumerator TurnAround(Vector3 dir)
    {
        float timer = 0.5f;
        while (timer >= 0f)
        {
            timer -= Time.fixedDeltaTime;
            Quaternion targetRot = Quaternion.LookRotation(dir - stateManager.gameObject.transform.position);
            stateManager.gameObject.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, 0.1f);
            yield return new WaitForFixedUpdate();
        }
    }



    public void SetAIController(AIController p)
    {
        aIController = p;
    }

    public void EnableAtkSphere()
    {
        atkSphere.enabled = true;
    }

    public void DisableAtkSphere()
    {
        atkSphere.enabled = false;
    }

}
