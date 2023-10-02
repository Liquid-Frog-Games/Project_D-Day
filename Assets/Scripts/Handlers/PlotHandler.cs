using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlotHandler : MonoBehaviour
{
    //sprites 
    public SpriteRenderer spriteRenderer;
    public Sprite plotSprite;

    //gameObjects
    public GameObject[] icons;
    private GameObject tower;
    private Tower towerToBuild;

    //canvas
    public Canvas shopCanvas;
    public Canvas previewCanvas;
    public Canvas sellCanvas;


    //textmesh
    public TextMeshProUGUI sellPrice;

    //booleans 
    public bool shopOpen;
    public bool sellPreview;


    private void Start()
    {
        shopOpen = false;
        sellPreview = false;
        ShopCanvasGroupOff();
        PreviewCanvasGroupOff();

    }
    //ui for the shop 

    private void ShopCanvasGroupOff()
    {
        //turns the shop UI off
        shopOpen = false;
        shopCanvas.GetComponent<CanvasGroup>().alpha = 0;
        shopCanvas.GetComponent<CanvasGroup>().interactable = false;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    private void ShopCanvasGroupOn()
    { //turns the UI on
        shopOpen = true;
        shopCanvas.GetComponent<CanvasGroup>().alpha = 1;
        shopCanvas.GetComponent<CanvasGroup>().interactable = true;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    //ui for the confirm or decline button

    private void PreviewCanvasGroupOff()
    {
        //turns the preview UI off
        previewCanvas.GetComponent<CanvasGroup>().alpha = 0;
        previewCanvas.GetComponent<CanvasGroup>().interactable = false;
        previewCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    private void PreviewCanvasGroupOn()
    {
        //turns the preview UI on
        previewCanvas.GetComponent<CanvasGroup>().alpha = 1;
        previewCanvas.GetComponent<CanvasGroup>().interactable = true;
        previewCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void SellCanvasGroupOff()
    {
        //turns the preview UI off
        sellPreview = false;
        sellCanvas.GetComponent<CanvasGroup>().alpha = 0;
        sellCanvas.GetComponent<CanvasGroup>().interactable = false;
        sellCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    private void SellCanvasGroupOn()
    {
        //turns the preview UI on
        sellPrice.text = (towerToBuild.cost / 2).ToString();
        sellPreview = true;
        sellCanvas.GetComponent<CanvasGroup>().alpha = 1;
        sellCanvas.GetComponent<CanvasGroup>().interactable = true;
        sellCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //when the player clicks on the plot

    public void OnMouseDown()
    {
        
            if (tower != null)
            {
                SellCanvasGroupOn();
                return;
            }
            else
            {
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

    }

    //does the element get covered by UI
    /*
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    */
    //preview function
    public void PreviewTower()
    {
        ShopCanvasGroupOff();
        PreviewCanvasGroupOn();

        towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > LevelManager.main.coins)       //replace with UI message

        {
            LevelManager.main.StartNotification();
            CancelTower();
            return;
        }

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

        return;
    }

    //placing the tower
    public void PlaceTower()
    {
        if (tower.TryGetComponent<TurretHandler>(out TurretHandler th))
        {
            th.ToggleActive();
        }
        if (tower.TryGetComponent<HydraHandler>(out HydraHandler hh))
        {
            hh.ToggleActive();
        }
        if (tower.TryGetComponent<CockatriceHandler>(out CockatriceHandler ch))
        {
            ch.ToggleActive();
        }
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

    public void SellTower()
    {
        LevelManager.main.IncreaseCurrency(towerToBuild.cost / 2);
        towerToBuild = null;
        Destroy(tower);
        BuildManager.main.SetSelectedTower(-1);
        spriteRenderer.sprite = plotSprite;
        SellCanvasGroupOff();
        return;

    }
    public void CancelSellTower()
    {
        SellCanvasGroupOff();
        return;
    }
}
