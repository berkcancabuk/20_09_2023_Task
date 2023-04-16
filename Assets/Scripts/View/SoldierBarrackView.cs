using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using Assets.Scripts.Controller.Factory;
using Random = UnityEngine.Random;

namespace Assets.Scripts.View
{
    

    public class SoldierBarrackClickedEventArgs : EventArgs
    {
    }
    public class SoldierbarrackInstantiateEventArg
    {
    }
    public interface ISoldierBarrackView
    {
        event EventHandler<SoldierBarrackClickedEventArgs> OnClicked;
        event EventHandler<SoldierbarrackInstantiateEventArg> OnInstantiate;
        Vector3 Position { get; set; }
        int Health { get; set; }
        void OnClick();
        void OnInstan();
    }

   

    public class SoldierBarrackView : MonoBehaviour, ISoldierBarrackView
    {
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] public Color _spriteRendererStartColor;

        [SerializeField] public bool isTrigger;
        public List<GameObject> soldiers = new();
        private TileListClass _tileList;
        public int index = 0;
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRendererStartColor = spriteRenderer.color;
            _tileList = TileListClass.instance;
        }
        public event EventHandler<SoldierBarrackClickedEventArgs> OnClicked = (sender, e) => { };
        public event EventHandler<SoldierbarrackInstantiateEventArg> OnInstantiate= (sender, e) => { };

        public Vector3 position;
        public int soldierBarrackHealth;

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
                transform.position = value;
            }
        }

        public int Health
        {
            get => soldierBarrackHealth;
            set
            {
                soldierBarrackHealth = value;
                //SoldierBarrack bir healli clası olmalı
                //transform.GetComponent<EnemyObjectClass>()._enemyHealth = soldierBarrackHealth;
            }
        }


        private void Update()
        {
            if (soldierBarrackHealth <= 0) Destroy(gameObject);
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5)),
                    Vector2.zero);
                if (hit.transform.gameObject == gameObject)
                {
                    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5);
                    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                    transform.position = new Vector3((int)(curPosition.x) + .5f, (int)(curPosition.y) + .5f, -.1f);
                    var eventArgs = new SoldierBarrackClickedEventArgs();
                    OnClicked(this, eventArgs);
                }
            }
        }
        

        // Enemy soldierla carpisti mi
        private void OnTriggerStay2D(Collider2D other)
        { 
            isTrigger = true; 
            spriteRenderer.color = new Color(212f / 255f, 49f / 255f, 49f / 255f, 120f / 255f);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isTrigger = false;
            spriteRenderer.color = _spriteRendererStartColor;
        }

        public void OnClick()
        {
            UIManager.instance.OpenSoldierBarrackPanel();
        }

        public void OnInstantiateObj()
        {
            var eventArgs = new SoldierbarrackInstantiateEventArg();
            OnInstantiate(this, eventArgs);
        }
        public void OnInstan()
        {
            
        }
         
        public void InstantiateSpawnPoint(GameObject spawnGO)
     {
        SoldierObjectClass soldierObject = spawnGO.GetComponent<SoldierObjectClass>();
        int newSoldierLevel = soldierObject.soldierLevel;
        soldiers = soldiers.Where(item => item != null).ToList();
        Dictionary<int, int> soldierLevelToSoldierHealth = soldiers.GroupBy(soldier => soldier.GetComponent<SoldierObjectClass>().soldierLevel)
           .ToDictionary(soldierGroup => soldierGroup.Key, soldierGroup => soldierGroup.Sum(soldier => soldier.GetComponent<SoldierObjectClass>().soldierHealth));
        
        for (int i = 0; i < _tileList.tiles.Count; i++)
        {
            int randomValue = Random.Range(i, _tileList.tiles.Count);
            if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
            {
                if(!CheckSoldiersLevel(1, soldierLevelToSoldierHealth, newSoldierLevel,10,2) &&
                    !CheckSoldiersLevel(2, soldierLevelToSoldierHealth, newSoldierLevel,10,5) &&
                    !CheckSoldiersLevel(3, soldierLevelToSoldierHealth, newSoldierLevel,10,10))
                {
                    GameObject GO = Instantiate(spawnGO, _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity);
                    GO.transform.position = new Vector3(GO.transform.position.x, GO.transform.position.y, -0.1f);
                    soldiers.Add(GO);
                }
                break;
            }
            }
        }
        public bool CheckSoldiersLevel(int solderLevel, Dictionary<int, int> soldierLevelToSoldierHealth, int newSoldierLevel, int solderHeal, int soldierAttack)
    {

        if (newSoldierLevel == solderLevel && soldierLevelToSoldierHealth.Count != 0 && soldierLevelToSoldierHealth.ContainsKey(solderLevel) && soldierLevelToSoldierHealth[solderLevel] > 0)
        {
            GameObject level1Soldier = soldiers.Find(soldier => soldier.GetComponent<SoldierObjectClass>().soldierLevel == solderLevel);

            level1Soldier.GetComponent<SoldierObjectClass>().soldierHealth += solderHeal;
            level1Soldier.GetComponent<SoldierObjectClass>().soldierPower += soldierAttack;
            level1Soldier.GetComponent<SoldierObjectClass>().soldierCount++;
            return true;
        }
        return false;
    }
    }
}