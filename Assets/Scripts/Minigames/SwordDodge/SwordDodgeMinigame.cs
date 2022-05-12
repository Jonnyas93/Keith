using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordDodgeMinigame : Minigame
{
    public GameObject animatorGO;
    Animator animator;

    public Warning lWarningGO;
    public Warning mWarningGO;
    public Warning rWarningGO;

    public SwordDodgePlayer Player;

    public List<Warning> warnings;

    public Text text;

    public ParticleSystem hurt;

    SpriteRenderer lWarning;
    SpriteRenderer mWarning;
    SpriteRenderer rWarning;

    int laneNum;
    public int dodgeCount = 0;
    public bool isAttacking;
    bool cooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = animatorGO.GetComponent<Animator>();
        animator.StopPlayback();
        warnings.Add(lWarningGO);
        warnings.Add(mWarningGO);
        warnings.Add(rWarningGO);
        lWarning = lWarningGO.GetComponent<SpriteRenderer>();
        mWarning = mWarningGO.GetComponent<SpriteRenderer>();
        rWarning = rWarningGO.GetComponent<SpriteRenderer>();
        lWarning.enabled = false;
        mWarning.enabled = false;
        rWarning.enabled = false;
        laneNum = Random.Range(0, 3);
        StartCoroutine(ShowWarning());
    }

    // Update is called once per frame
    void Update()
    {
        text.text = dodgeCount.ToString();
        if (!cooldown)
        {
            if (isAttacking == true)
            {
                switch (laneNum)
                {
                    case 0:
                        if (Player.isTouching == PositionEnum.LEFT)
                        {
                            Debug.Log("Left Hit");
                            dodgeCount = 0;
                            StartCoroutine(Hurt());
                            StartCoroutine(CooldownTimer());
                        }
                        break;
                    case 1:
                        if (Player.isTouching == PositionEnum.MIDDLE)
                        {
                            Debug.Log("Middle Hit");
                            dodgeCount = 0;
                            StartCoroutine(Hurt());
                            StartCoroutine(CooldownTimer());
                        }
                        break;
                    case 2:
                        if (Player.isTouching == PositionEnum.RIGHT)
                        {
                            Debug.Log("Right Hit");
                            dodgeCount = 0;
                            StartCoroutine(Hurt());
                            StartCoroutine(CooldownTimer());
                        }
                        break;
                }
            }
        }
    }

    public void Slash()
    {
        switch (laneNum)
        {
            case 0:
                StartCoroutine(LeftAttack());
            break;
            case 1:
                StartCoroutine(MiddleAttack());
            break;
            case 2:
                StartCoroutine(RightAttack());
            break;
            default:
                StartCoroutine(MiddleAttack());
            break;
        };
        
    }

    public void CallShowWarning()
    {
        StartCoroutine(ShowWarning());
    }

    public void CallEndMinigame()
    {
        StartCoroutine(EndMinigame());
    }

    public void CheckForTnight()//I love Fortnite
    {
        if (dodgeCount == 3)
        {
            StartCoroutine(EndMinigame());
        }
        else
        {
            StartCoroutine(ShowWarning());
        }
    }

    IEnumerator ShowWarning()
    {
        yield return new WaitForSeconds(1f);
        warnings[laneNum].StartFlash();
        yield return new WaitForSeconds(warnings[laneNum].GetTotalFlashTime() + 1f);
        Slash();
    }

    IEnumerator LeftAttack()
    {
        animator.Play("Base Layer.Left Lane");
        yield return new WaitForSeconds(0.30f); //Currently just manually inputting the animation length, but should make a dynamic system.
        isAttacking = true;
        yield return new WaitForSeconds(0.74f);
        isAttacking = false;
        yield return new WaitForSeconds(0.05f);
        animator.Play("Base Layer.Neutral");
        laneNum = Random.Range(0, 3);
        dodgeCount++;
        CheckForTnight();
    }
    IEnumerator MiddleAttack()
    {
        animator.Play("Base Layer.Mid Lane");
        yield return new WaitForSeconds(0.30f);
        isAttacking = true;
        yield return new WaitForSeconds(0.74f);
        isAttacking = false;
        yield return new WaitForSeconds(0.05f);
        animator.Play("Base Layer.Neutral");
        laneNum = Random.Range(0, 3);
        dodgeCount++;
        CheckForTnight();
    }
    IEnumerator RightAttack()
    {
        animator.Play("Base Layer.Right Lane");
        yield return new WaitForSeconds(0.30f);
        isAttacking = true;
        yield return new WaitForSeconds(0.74f);
        isAttacking = false;
        yield return new WaitForSeconds(0.05f);
        animator.Play("Base Layer.Neutral");
        laneNum = Random.Range(0, 3);
        dodgeCount++;
        CheckForTnight();
    }

    IEnumerator Hurt()
    {
        ParticleSystem ps = Instantiate(hurt, Player.transform);
        ps.Play();
        yield return new WaitForSeconds(2f);
        Destroy(ps);
    }

    IEnumerator CooldownTimer()
    {
        cooldown = true;
        yield return new WaitForSeconds(1.5f);
        cooldown = false;
    }
}
