using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlotHandler : MonoBehaviour
{
    //sprites 
    public SpriteRenderer spriteRenderer;
    public Sprite addSprite;
    public Sprite removeSprite;

    //gameObjects
    public GameObject[] icons;
    private GameObject tower;
    private Tower towerToBuild;

    //canvas
    public Canvas shopCanvas;
    public Canvas previewCanvas;

    //booleans 
    public bool shopOpen;
    public bool inPreview;


    private void Start()
    {
        spriteRenderer.sprite = addSprite;
        inPreview = false;
        shopOpen = false;
        ShopCanvasGroupOff();
        PreviewCanvasGroupOff();

    }
    //ui for the shop 

    private void ShopCanvasGroupOff()
    {
        //turns the shop UI off
        shopOpen = false;
        spriteRenderer.sprite = addSprite;
        shopCanvas.GetComponent<CanvasGroup>().alpha = 0;
        shopCanvas.GetComponent<CanvasGroup>().interactable = false;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    private void ShopCanvasGroupOn()
    { //turns the UI on
        shopOpen = true;
        spriteRenderer.sprite = removeSprite;
        shopCanvas.GetComponent<CanvasGroup>().alpha = 1;
        shopCanvas.GetComponent<CanvasGroup>().interactable = true;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    //ui for the confirm or decline button

    private void PreviewCanvasGroupOff()
    {
        //turns the preview UI off
        inPreview = true;
        previewCanvas.GetComponent<CanvasGroup>().alpha = 0;
        previewCanvas.GetComponent<CanvasGroup>().interactable = false;
        previewCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    private void PreviewCanvasGroupOn()
    {
        //turns the preview UI on
        inPreview = false;
        previewCanvas.GetComponent<CanvasGroup>().alpha = 1;
        previewCanvas.GetComponent<CanvasGroup>().interactable = true;
        previewCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //when the player clicks on the plot

    public void OnMouseDown()
    {

        if (tower != null || towerToBuild != null) return;
        if (shopOpen == false)
        {
            ShopCanvasGroupOn();
            return;

        }
        if (shopOpen == true)
        {
            ShopCanvasGroupOff();
            return;
        }
    }

    //does the element get covered by UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //preview function
    public void PreviewTower()
    {
        ShopCanvasGroupOff();
        PreviewCanvasGroupOn();

        towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > LevelManager.main.coins)       //replace with UI message

        {
            Debug.Log("You dont have enough coins for this towwer");
            CancelTower();
            return;
        }

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

        return;
    }

    //placing the tower
    public void PlaceTower()
    {
        tower.GetComponent<TurretHandler>().ToggleActive();
        LevelManager.main.SpendCurrency(towerToBuild.cost);
        BuildManager.main.SetSelectedTower(-1);
        PreviewCanvasGroupOff();
        spriteRenderer.sprite = null;
        return;
    }

    //canceling the preview
    public void CancelTower()
    {
        towerToBuild = null;
        Destroy(tower);
        BuildManager.main.SetSelectedTower(-1);
        PreviewCanvasGroupOff();
        ShopCanvasGroupOn();
        return;
    }

}
