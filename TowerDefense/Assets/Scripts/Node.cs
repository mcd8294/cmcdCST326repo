using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false; 
    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if(!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect,5f);
        Debug.Log("Turret built! Money left: " + PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;
        
        //Get rid of old turret
        Destroy(turret);
        
        //Build new turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect,5f);

        isUpgraded = true;
        
        Debug.Log("Turret upgraded! Money left: " + PlayerStats.Money);
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect,5f);

        Destroy(turret);
        turretBlueprint = null;
    }
    
    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        if(!buildManager.CanBuild)
            return;
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
