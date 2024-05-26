using UnityEngine;

public class Weapon : MonoBehaviour
{
    private enum WeaponsType
    {
        Pistol
    }
    [SerializeField] private Transform firePoint;
    [SerializeField] private WeaponsType weapon;
    [SerializeField] private  Shooting shooting;
    
    private void LateUpdate()
    {
        if (Input.GetButtonDown("Shot"))
        {
            if (weapon == WeaponsType.Pistol)
            {
                Pistol();
            }
        }
     
    }
    public void Pistol()
    {
            shooting.Shot(80, 1, 0.25f, firePoint);
    }
}