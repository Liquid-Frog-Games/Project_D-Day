using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlotHandler : MonoBehaviour
{
    public SpriteRenderer sprite;

    public GameObject[] icons;
    private GameObject tower;


    public Canvas shopCanvas;
    public Canvas previewCanvas;

    public bool isSelected;

    private void Start()
    {
        isSelected = false;
        ShopCanvasGroupOff();
        PreviewCanvasGroupOff();

    }
    //ui for the shop 
   
    private void ShopCanvasGroupOff()
    { 
        //turns the shop UI off
        shopCanvas.GetComponent<CanvasGroup>().alpha = 0;
        shopCanvas.GetComponent<CanvasGroup>().interactable = false;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    
    private void ShopCanvasGroupOn()
    { //turns the UI on
        shopCanvas.GetComponent<CanvasGroup>().alpha = 1;
        shopCanvas.GetComponent<CanvasGroup>().interactable = true;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    //ui for the confirm or decline button

    private void PreviewCanvasGroupOff()
    {
        previewCanvas.GetComponent<CanvasGroup>().alpha = 0;
        previewCanvas.GetComponent<CanvasGroup>().interactable = false;
        previewCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    
    private void PreviewCanvasGroupOn()
    {
        //turns the UI on
        previewCanvas.GetComponent<CanvasGroup>().alpha = 1;
        previewCanvas.GetComponent<CanvasGroup>().interactable = true;
        previewCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //when the player clicks on the plot
    public void OnMouseDown()
    {
        if(isSelected == false)
        {
            Debug.Log("Work please");
            isSelected = true;
            ShopCanvasGroupOn();
            return;

        }
        if(isSelected == true)
        {
            Debug.Log("Turned off");
            isSelected= false;
            ShopCanvasGroupOff();
            return;
        }
    }
    private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

    public void PreviewTower()
    {
        
        {
            
            
            PreviewCanvasGroupOn();
        }
        return;
    }
    public void PlaceTower()
    {
        if (IsPointerOverUIObject())
        {
            Tower towerToBuild = BuildManager.main.GetSelectedTower();
            if (towerToBuild.cost > LevelManager.main.coins)       //TODO: Replace with UI message
            {
                Debug.Log("You dont have enough coins for this towwer");
                return;
            }
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            ShopCanvasGroupOff();
            LevelManager.main.SpendCurrency(towerToBuild.cost);
            BuildManager.main.SetSelectedTower(-1);
            ShopCanvasGroupOff();
            return;
        }
        return;
    }
    /*
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    
    private Color startColor;

    void Start()
    {
        startColor = sr.color;
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
  
        if(IsPointerOverUIObject() == false)
        {
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
        return;
    }
    */
}
