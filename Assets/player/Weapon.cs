using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject currentWeapon;

    void Start()
    {
        EquipWeapon(currentWeapon);
    }

    public void EquipWeapon(GameObject newWeapon)
    {
        currentWeapon = newWeapon;

        if (currentWeapon != null)
        {
            currentWeapon.transform.parent = transform;
            currentWeapon.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}
