using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {

    public GameObject projectile = null;
    public float projectileSpeed = 10f;
    public bool autimatic = false;
    public float recoil = 0.1f;
    public int ammo = 25;
    public float shootCooldown = 4f;
    public bool canShoot = true;
    public int currentAmmo;
    public Text ammoText;

    private float currentCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = this.ammo;

    }
    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
            PointTowardsMouse();

            bool fire = autimatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);

            if (fire && currentCooldown <= 0 && canShoot) {
                currentCooldown = recoil;
                Shoot();
            }

            currentCooldown = currentCooldown > 0 ? currentCooldown -= Time.deltaTime : 0;
    }

    void PointTowardsMouse() {
        // mouse pos in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // dir from gun to mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Apply the recoil effect
        transform.localPosition = new Vector3(-direction.x, -direction.y, direction.z) * currentCooldown;

        // angle (degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // rotate gun towards mouse
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Shoot() {
        // Instantiate the projectile at the gun's position and rotation
        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);

        // Access the Projectile script on the instantiated projectile (assuming it has one)
        Projectile projectileScript = proj.GetComponent<Projectile>();
        this.ammo--;
        if(this.ammo == 0)
        {
            canShoot = false;
            Invoke("reloadGun", shootCooldown);
        }
        // Set additional properties on the projectile script if needed
        if (projectileScript != null)
        {
            projectileScript.direction = transform.right;
            projectileScript.speed = projectileSpeed; // Adjust the speed as needed
        }
    }

    void reloadGun()
    {
        canShoot = true;
        this.ammo = 25;
    }
}
