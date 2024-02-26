using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int _enemies;
    int _nbNextLvl;
    [SerializeField] GameObject CanvasLvlFinish;

    void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _nbNextLvl = 1;
        }else{
            _nbNextLvl = 0;
        }
    }

    public void EnemyEliminated()
    {
        _enemies -= 1;
        Debug.Log(_enemies);

        if (_enemies == 0)
        {
            CanvasLvlFinish.SetActive(true);
        }
    }

    public void NxtLevel(){
        Debug.Log("Loading Lvl: "+ _nbNextLvl+1);
        SceneManager.LoadScene(_nbNextLvl);
    }
}
