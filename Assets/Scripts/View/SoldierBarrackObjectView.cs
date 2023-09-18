using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using TMPro;
using UnityEngine;
using View.Interface;
using Random = UnityEngine.Random;

namespace View
{


    public class SoldierbarrackInstantiateEventArg
    {
    }
    

    public interface ISoldierBarrackObjectView : IClickableObjectView
    {
        event EventHandler<SoldierbarrackInstantiateEventArg> OnInstantiate;
        Vector3 Position { get; set; }

        void GenerateSoldier(int level, int count);
        int Health { get; set; }

        void OnInstantiateObj();
    }


    public class SoldierBarrackObjectView : MovableObjectView, ISoldierBarrackObjectView
    {
        
        public List<GameObject> soldiers = new();
        private TileListClass _tileList;
        [SerializeField] private TextMeshProUGUI level1Text; 
        [SerializeField] private TextMeshProUGUI level2Text; 
        [SerializeField] private TextMeshProUGUI level3Text;
        private const string Level1StaticText = "Lvl 1 Soldier\n";
        private const string Level2StaticText = "Lvl 2 Soldier\n";
        private const string Level3StaticText = "Lvl 3 Soldier\n";
        public event EventHandler<SoldierbarrackInstantiateEventArg> OnInstantiate= (sender, e) => { };

        public SoldierBarrackObjectView()
        {
            
            
        }

        public Vector3 position;

        

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
                transform.position = value;
            }
        }

        public void GenerateSoldier(int level,int count)
        {
            switch (level)
            {
                case 1:
                    level1Text.text = Level1StaticText + count;
                    break;
                case 2:
                    level2Text.text = Level2StaticText + count;
                    break;
                case 3:
                    level3Text.text = Level3StaticText + count;
                    break;
            }
        }


        public void ChangePosition(Pointer3D pointer)
        {
            Position = pointer.ConvertVector3D();
        }

        public void OnClick()
        {
            // UIManager.instance.OpenSoldierBarrackPanel();
        }

        public void OnInstantiateObj()
        {
            var eventArgs = new SoldierbarrackInstantiateEventArg();
            OnInstantiate(this, eventArgs);
        }
        
        public void InstantiateSpawnPoint(GameObject spawnGO)
        {
            var soldierObject = spawnGO.GetComponent<SoldierObjectClass>();
            var newSoldierLevel = soldierObject.soldierLevel;
            soldiers = soldiers.Where(item => item != null).ToList();
            var soldierLevelToSoldierHealth = soldiers
                .GroupBy(soldier => soldier.GetComponent<SoldierObjectClass>().soldierLevel)
                .ToDictionary(soldierGroup => soldierGroup.Key,
                    soldierGroup =>
                        soldierGroup.Sum(soldier => soldier.GetComponent<SoldierObjectClass>().soldierHealth));

            for (var i = 0; i < _tileList.tiles.Count; i++)
            {
                var randomValue = Random.Range(i, _tileList.tiles.Count);
                if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
                {
                    if (!CheckSoldiersLevel(1, soldierLevelToSoldierHealth, newSoldierLevel, 10, 2) &&
                        !CheckSoldiersLevel(2, soldierLevelToSoldierHealth, newSoldierLevel, 10, 5) &&
                        !CheckSoldiersLevel(3, soldierLevelToSoldierHealth, newSoldierLevel, 10, 10))
                    {
                        var GO = Instantiate(spawnGO,
                            _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center,
                            Quaternion.identity);
                        GO.transform.position = new Vector3(GO.transform.position.x, GO.transform.position.y, -0.1f);
                        soldiers.Add(GO);
                    }

                    break;
                }
            }
        }

        public bool CheckSoldiersLevel(int solderLevel, Dictionary<int, int> soldierLevelToSoldierHealth,
            int newSoldierLevel, int solderHeal, int soldierAttack)
        {
            if (newSoldierLevel == solderLevel && soldierLevelToSoldierHealth.Count != 0 &&
                soldierLevelToSoldierHealth.ContainsKey(solderLevel) && soldierLevelToSoldierHealth[solderLevel] > 0)
            {
                var level1Soldier = soldiers.Find(soldier =>
                    soldier.GetComponent<SoldierObjectClass>().soldierLevel == solderLevel);

                level1Soldier.GetComponent<SoldierObjectClass>().soldierHealth += solderHeal;
                level1Soldier.GetComponent<SoldierObjectClass>().soldierPower += soldierAttack;
                level1Soldier.GetComponent<SoldierObjectClass>().soldierCount++;
                return true;
            }

            return false;
        }

       
    }
}