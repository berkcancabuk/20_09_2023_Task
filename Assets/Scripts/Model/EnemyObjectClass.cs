using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;

public class EnemyObjectClass : MonoBehaviour
{
    [SerializeField] public int _enemyHealth;
    [SerializeField] public int _enemyAttack;
    [SerializeField] private GameObject _soldier;
    private bool _isEnemyFight;
    List<GameObject> collisionGO = new List<GameObject>();
    private void Update()
    {
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
            EnemySpawner.instance.InstantiateSpawnPoint();
        }
    }
    public IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(2f);
        if (_soldier.GetComponent<SoldierObjectClass>().soldierHealth <=0)
        {
            var soldierObjectClass = _soldier.GetComponent<SoldierObjectClass>();
            var AIDestiation = _soldier.GetComponent<AIDestinationSetter>();
            AIDestiation.GetComponent<AIDestinationSetter>().target = null;
            AIDestiation.GetComponent<AIPath>().enabled = false;
            AIDestiation.GetComponent<AIDestinationSetter>().enabled = false;
            
            _soldier.transform.parent = soldierObjectClass.soldierBarrackObjectView.gameObject.transform;
            _soldier.transform.localPosition = soldierObjectClass.starPos;
            _soldier.transform.rotation = Quaternion.Euler(0,0,0);
            _soldier = null;
        }
        if (_soldier != null)
        {
            _soldier.GetComponent<SoldierObjectClass>().soldierHealth -= _enemyAttack;
            if (_enemyHealth <= 0)
            {
                Destroy(gameObject);
                EnemySpawner.instance.InstantiateSpawnPoint();
                yield break;
            }
            
            StartCoroutine(EnemyAttack());
        }
        else
        {
            if (collisionGO.Count != 0)
            {
                collisionGO.RemoveAt(0);
            }
            
            if (collisionGO.Count >= 1)
            {
               
                _soldier = collisionGO[0];
                _isEnemyFight = false;
                StartCoroutine(EnemyAttack());
            }
            else
            {
                _isEnemyFight = false;
                
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Soldier")
        {
            collisionGO.Add(collision.gameObject);
            if (!_isEnemyFight || collisionGO.Count == 1)
            {
                _soldier = collisionGO[0];
                _isEnemyFight = true;
                StartCoroutine(EnemyAttack());
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke("EnemyAttack");
    }
}
