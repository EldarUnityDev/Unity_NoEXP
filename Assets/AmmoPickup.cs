using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public void ReplenishAmmo()
    {
        if (References.thePlayer != null)
        {
            RefillWeapon(References.thePlayer.mainWeapon);
            RefillWeapon(References.thePlayer.secondaryWeapon);
            Destroy(gameObject);
        }
    }
    void RefillWeapon(WeaponBehaviour weapon)
    {
        if(weapon != null)
        {
            weapon.currentMagSize = weapon.magSize;
        }
    }
}
