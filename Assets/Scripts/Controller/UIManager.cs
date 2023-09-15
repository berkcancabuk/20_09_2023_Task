using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controller.Factory;
using Controller;
using Model.Interface;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using View;

public class UIManager : MonoBehaviour
{
    public static UIManager instance=null;
    public Information Information;
    private List<float> objectPos = new List<float>();
    public TextMeshProUGUI energyValue;
    public List<Buildings> buildings = new List<Buildings>();
    public List<Soldiers> soldiers = new List<Soldiers>();
    public List<GameObject> soldierBarrack = new List<GameObject>();
    public List<GameObject> powerPlant = new List<GameObject>();
    private IBuildingFactory soldierBarrackFactory;
    private IBuildingFactory powerPlantFactory;
    private readonly List<SoldierBarrackController> _soldierBarrackControllers = new();
    private readonly List<PowerPlantController> _powerPlantControllers = new();
    private void Awake()
    {
        soldierBarrackFactory = new SoldierBarrackFactory();
        powerPlantFactory = new PowerPlantFactory();
        
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
        var middleOfScreen = Vector3.zero;
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
           // case("Barricade"):
           // {
           //     
           //     var barrackBuilding = soldierBarrackFactory.GenerateBuilding(middleOfScreen);
           //     ISoldierBarrackView soldierBarrackView = go.GetComponent<SoldierBarrackView>();
           //     SoldierBarrackController soldierBarrackController = new SoldierBarrackController(barrackBuilding as ISoldierBarracks,soldierBarrackView);
           //     _soldierBarrackControllers.Add(soldierBarrackController);
           //     break;
           // } 
       }

    }
    public List<float> ObjScale(GameObject GO)
    {
        objectPos.Clear();
        if (GO.transform.localScale.x % 2 == 0 && GO.transform.localScale.y % 2 == 0)
        {
            objectPos.Add(3.5f);
            objectPos.Add(4.5f);
            return objectPos;
        }
        else if (GO.transform.localScale.x %2 != 0 && GO.transform.localScale.y % 2 == 0 || GO.transform.localScale.x % 2 == 0 && GO.transform.localScale.y % 2 != 0)
        {
            objectPos.Add(3.5f);
            objectPos.Add(4f);

            return objectPos;
        }
        objectPos.Add(2f);
        objectPos.Add(2f);

        return objectPos;
        
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
    public void OpenSoldiersPanel(GameObject GO)
    {
        UIObjectPanelClose();
        Information.image.sprite = buildings[2].image;
        Information.name.text = buildings[2].name;
        buildings[2].UI.SetActive(true);

        print(GO.name);
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersLevel.text = "Soldiers Level: "+GO.GetComponent<SoldierObjectClass>().soldierLevel;
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersCount.text = "Soldiers Count: " + GO.GetComponent<SoldierObjectClass>().soldierCount;
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersHealth.text = "Soldiers Health: " + GO.GetComponent<SoldierObjectClass>().soldierHealth;
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersAttack.text = "Soldiers Attack: " + GO.GetComponent<SoldierObjectClass>().soldierPower;

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

    public void BoughtSoldierBarrack()
    {
        // for (int i = 0; i < soldierBarrack.Count; i++)
        // {
        //     soldierBarrack[i].transform.GetChild(1).gameObject.SetActive(true);
        //     soldierBarrack[i].transform.GetComponent<Button>().enabled = false;
        // }
    }
    public void BoughtPowerPlant()
    {
        // for (int i = 0; i < powerPlant.Count; i++)
        // {
        //     powerPlant[i].transform.GetChild(1).gameObject.SetActive(true);
        //     powerPlant[i].transform.GetComponent<Button>().enabled = false;
        // }
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