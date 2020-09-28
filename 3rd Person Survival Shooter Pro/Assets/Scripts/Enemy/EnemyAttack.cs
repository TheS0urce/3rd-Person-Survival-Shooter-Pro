using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAI _enemyAI;

    private void Start()
    {
        _enemyAI = GetComponentInParent<EnemyAI>();

        if(_enemyAI == null)
        {
            Debug.LogError("EnemyAI is NULL.");
        }
    }

    //ontrigger enter
    //start attack + stop movement

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemyAI.StartAttack();
        }
    }

    //ontrigger exit
    //continue follow player

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemyAI.StopAttack();
        }
    }
}
