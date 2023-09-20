using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using View;

public class SoldierObjectClass : MonoBehaviour
{
    public int soldierHealth;
    public int soldierPower;
    public int soldierLevel;
    public int soldierCount;
    [SerializeField] private GameObject _enemy;
    [SerializeField] SoldierSpawnPoint soldierSpawn;
    private GameManager gameManager;
    public SoldierBarrackObjectView soldierBarrackObjectView;
    public Vector3 starPos;
    private void Awake()
    {
       
        soldierSpawn = SoldierSpawnPoint.instance;
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        starPos = transform.localPosition;
    }

    private void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().color == Color.green)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
            GetComponent<AIDestinationSetter>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<AIDestinationSetter>().enabled = true;
        }
    }
}
