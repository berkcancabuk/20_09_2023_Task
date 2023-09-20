using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controller.Factory;
using Controller;
using Controller.Factory;
using Model.Interface;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using View;

public class UIManager : MonoBehaviour
{
    public static UIManager instance=null;
    public Information Information;
    public TextMeshProUGUI energyValue;
    public List<Buildings> buildings = new List<Buildings>();
    private IBuildingFactory soldierBarrackFactory;
    private IBuildingFactory powerPlantFactory;
    private IBuildingFactory barricadeFactory;
    private readonly List<SoldierBarrackController> _soldierBarrackControllers = new();
    private readonly List<PowerPlantController> _powerPlantControllers = new();
    private readonly List<BarricadeController> _barricadeControllers = new();
    
    private void Awake()
    {
        soldierBarrackFactory = new SoldierBarrackFactory();
        powerPlantFactory = new PowerPlantFactory();
        barricadeFactory = new BarricadeFactory();
        
        if (instance != null)
        {
            Destroy(this);
            
        }
        else
        {
            instance = this;
        }
    }

    public void InstantiateSquareObject(GameObject obj)
    {
        var middleOfScreen = new Vector3(0,0,-0.1f);
        var go = Instantiate(obj, middleOfScreen, Quaternion.identity);
        
       switch (go.tag)
       {
           case("SoldierBarrack"):
           {
               var barrackBuilding = soldierBarrackFactory.GenerateBuilding(middleOfScreen);
               ISoldierBarrackObjectView soldierBarrackObjectView = go.GetComponent<SoldierBarrackObjectView>();
               SoldierBarrackController soldierBarrackController = new SoldierBarrackController(barrackBuilding as ISoldierBarracks,soldierBarrackObjectView, this);
               soldierBarrackObjectView.OnInstantiateObj();
               _soldierBarrackControllers.Add(soldierBarrackController);
                break;
            }
           case("PowerPlant"):
           {
               
               var powerBuilding = powerPlantFactory.GenerateBuilding(middleOfScreen);
               IPowerPlantView powerPlantView = go.GetComponent<IPowerPlantView>();
               PowerPlantController powerPlantController = new PowerPlantController(powerBuilding as IPowerPlant, powerPlantView);
               _powerPlantControllers.Add(powerPlantController);
               break;
           } 
           case("Barricade"):
           {
               
               var barricadeBuilding = barricadeFactory.GenerateBuilding(middleOfScreen);
               IBarricadeView barricadeView = go.GetComponent<BarricadeView>();
               BarricadeController barricadeController = new BarricadeController(barricadeBuilding as IBarricade, barricadeView);
               _barricadeControllers.Add(barricadeController);
               break;
           } 
       }
    }
    
    public void UIObjectPanelClose()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            buildings[i].UI.SetActive(false);
        }
    }
    public void OpenSoldierBarrackPanel(int level1SoldierCount, int level2SoldierCount, int level3SoldierCount)
    {
        
        UIObjectPanelClose();
        Information.image.sprite = buildings[0].image;
        Information.name.text = buildings[0].name;
        buildings[0].UI.SetActive(true);
        buildings[0].level1Count.text = "Level 1: " + level1SoldierCount;
        buildings[0].level2Count.text = "Level 2: " + level2SoldierCount;
        buildings[0].level3Count.text = "Level 3: " + level3SoldierCount;
        
    }
    public void OpenPowerPlantPanel()
    {
        UIObjectPanelClose();
        Information.image.sprite = buildings[1].image;
        Information.name.text = buildings[1].name;
        buildings[1].UI.SetActive(true);
    }
    public void OpenEnemyPanel(GameObject GO)
    {
        UIObjectPanelClose();
        Information.image.sprite = buildings[3].image;
        Information.name.text = buildings[3].name;
        buildings[3].UI.SetActive(true);
        buildings[3].level1Count.text = "Health : "+GO.GetComponent<EnemyObjectClass>()._enemyHealth;
        buildings[3].level2Count.text = "Power : "+GO.GetComponent<EnemyObjectClass>()._enemyAttack;
    }
    
}
[System.Serializable]
public class Information
{
    public TextMeshProUGUI name;
    public Image image;
}
[System.Serializable]
public class Buildings
{
    public string name;
    public GameObject UI;
    public Sprite image;
    public TextMeshProUGUI level1Count;
    public TextMeshProUGUI level2Count;
    public TextMeshProUGUI level3Count;

}
[System.Serializable]
public class Soldiers
{
    public TextMeshProUGUI soldiersLevel;
    public TextMeshProUGUI soldiersCount;
    public TextMeshProUGUI soldiersHealth;
    public TextMeshProUGUI soldiersAttack;
}