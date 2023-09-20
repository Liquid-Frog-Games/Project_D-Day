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


    public Canvas canvas;

    public bool isSelected;

    private void Start()
    {
        isSelected = false;
        canvasGroupOff();

    }

    private void canvasGroupOff()
    {
        canvas.GetComponent<CanvasGroup>().alpha = 0;
        canvas.GetComponent<CanvasGroup>().interactable = false;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void canvasGroupOn()
    {
        canvas.GetComponent<CanvasGroup>().alpha = 1;
        canvas.GetComponent<CanvasGroup>().interactable = true;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void OnMouseDown()
    {
        if(isSelected == false)
        {
            Debug.Log("Work please");
            isSelected = true;
            canvasGroupOn();
            return;

        }
        if(isSelected == true)
        {
            Debug.Log("Turned off");
            isSelected= false;
            canvasGroupOff();
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

    public void PlaceTower()
    { 
        if(IsPointerOverUIObject())
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
            canvasGroupOff();
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
