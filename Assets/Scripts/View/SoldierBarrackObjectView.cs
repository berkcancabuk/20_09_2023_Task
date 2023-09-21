using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Pathfinding;
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
        public int Soldier1Count { get; set; }
        public int Soldier2Count { get; set; }
        public int Soldier3Count { get; set; }
    }


    public class SoldierBarrackObjectView : MovableObjectView, ISoldierBarrackObjectView
    {
        
        private TileListClass _tileList;
        [SerializeField] private TextMeshProUGUI level1Text; 
        [SerializeField] private TextMeshProUGUI level2Text; 
        [SerializeField] private TextMeshProUGUI level3Text;

        [SerializeField] private SoldierObjectClass Soldier1;
        [SerializeField] private SoldierObjectClass Soldier2;
        [SerializeField] private SoldierObjectClass Soldier3;
        
        private const string Level1StaticText = "Lvl 1 Soldier\n";
        private const string Level2StaticText = "Lvl 2 Soldier\n";
        private const string Level3StaticText = "Lvl 3 Soldier\n";
        public int Soldier1Count { get; set; }
        public int Soldier2Count { get; set; }
        public int Soldier3Count { get; set; }
        
        public event EventHandler<SoldierbarrackInstantiateEventArg> OnInstantiate= (sender, e) => { };

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
                    if (Soldier1.transform.parent != null)
                    {
                        level1Text.text = Level1StaticText + count;
                        SoldierClassInit(Soldier1, 10);
                    }
                    else
                    {
                        Soldier1Count = 0;
                    }
                    break;
                case 2:
                    if (Soldier2.transform.parent != null)
                    {
                        level2Text.text = Level2StaticText + count;
                        SoldierClassInit(Soldier2, 5);
                    }
                    else
                    {
                        Soldier2Count = 0;
                    }
                    break;
                case 3:
                    if (Soldier3.transform.parent != null)
                    {
                        level3Text.text = Level3StaticText + count;
                        SoldierClassInit(Soldier3, 2);
                    }
                    else
                    {
                        Soldier3Count = 0;
                    }
                    break;
            }
        }

        private void SoldierClassInit(SoldierObjectClass soldier,int damage)
        {
            soldier.soldierHealth += 10;
            soldier.soldierPower += damage;
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
       
    }
}