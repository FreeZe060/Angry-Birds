using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject[] _enemies;
    [SerializeField] GameObject CanvasLvlFinish;

    void Start()
    {
        EnemyEliminated();
    }

    // Appelez cette fonction chaque fois qu'un ennemi est éliminé
    public void EnemyEliminated()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (_enemies.Length == 0)
        {
            CanvasLvlFinish.SetActive(true);
        }
    }

    public void NxtLevel(){

    }
}
