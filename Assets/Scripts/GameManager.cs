using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject finishObj;

    void Start()
    {
        EnemyEliminated();
    }

    // Appelez cette fonction chaque fois qu'un ennemi est éliminé
    public void EnemyEliminated()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 && finishObj != null)
        {
            finishObj.SetActive(true);
        }
    }
}
