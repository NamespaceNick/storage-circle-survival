using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public enum Equipped { Shield, Turret, Empty};
    public Equipped currEquipped;

    public bool inUse = false;

    Shield shield;
    Turret turret;


	void Start ()
    {
        shield = GetComponentInChildren<Shield>();
        turret = GetComponentInChildren<Turret>();
        if (shield == null || turret == null)
        {
            Debug.LogError(" equipment slot missing references");
        }

        InitializeEquipment();
	}


    public void CycleEquipment()
    {
        if (currEquipped == Equipped.Shield)
        {
            shield.Unequip();
            currEquipped = Equipped.Turret;
            turret.Equip();
        }
        else if (currEquipped == Equipped.Turret)
        {
            turret.Unequip();
            currEquipped = Equipped.Shield;
            shield.Equip();
        }
        else if (currEquipped == Equipped.Empty)
        {
            currEquipped = Equipped.Shield;
            shield.Equip();
        }
    }


    // Initializes equipment for game startup
    void InitializeEquipment()
    {
        if (currEquipped != Equipped.Shield)
        {
            shield.gameObject.SetActive(false);
        }
        if (currEquipped != Equipped.Turret)
        {
            turret.gameObject.SetActive(false);
        }
    }


    public void TriggerPressed()
    {
        if (currEquipped == Equipped.Turret)
        { turret.Shoot(); }
    }
}
