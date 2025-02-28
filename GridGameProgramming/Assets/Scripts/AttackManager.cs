using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [Header("Round Controls")]
	[SerializeField] private float attackDelay = 2f;
	[SerializeField] private int attacksInRound = 5;
	private bool roundInSession = false;
    private bool canAttack = true;
    private int attackType;

    // Starts the next round upon player discrection if one isn't already going on.  
	public void StartRound()
    {
        if (roundInSession) return;
        roundInSession = true;

        // Determines which attack number we are on for the sake of determining the round end. 
        int attackNumber = 0;

        // Selects and activates the current attack.
        while (attackNumber < attacksInRound)
        {
            if (canAttack)
            {
                canAttack = false; 
                attackNumber++;

                attackType = Random.Range(0, 2);

                switch (attackType)
                {
                    case 0:
                        LineAttack();
                        break;
                    case 1:
                        BombAttack();
                        break;
                    case 2:
                        PointAttack();
                        break;
                }
            }
            StartCoroutine(AttackCooldown());
        }

        // Turns off the current round, and makes the next more difficult. 
        roundInSession = false;
        attacksInRound++;
        attackDelay -= .1f;
    }

    // Allows the grid to attack again once the cooldown has elapsed. 
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true; 
    }

    void LineAttack()
    {

    }
    void BombAttack()
    {

    }
    void PointAttack()
    {

    }
}
