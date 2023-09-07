using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlotHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    void Start()
    {
        startColor = sr.color;
    }


    private bool IsMouseOverUI() 
    {      
        Debug.Log("Does it reguister?");
        return EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId);
        
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
                   //TODO: Replace return with upgrade options
        if (tower != null) return;
  
        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > LevelManager.main.coins)       //TODO: Replace with UI message
        {
            Debug.Log("You dont have enough coins for this towwer");
            return;
        }
        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        BuildManager.main.SetSelectedTower(-1);
    }
}
