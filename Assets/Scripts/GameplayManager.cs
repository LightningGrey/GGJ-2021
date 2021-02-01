using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;


    public void EnemyKill(GameObject enemy)
    {
        enemies.Remove(enemy);
        enemy.SetActive(false);
        if (enemies.Count == 0)
        {
            SceneManager.LoadScene(2);
        }
    }

}
