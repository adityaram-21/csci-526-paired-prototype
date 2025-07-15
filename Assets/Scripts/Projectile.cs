using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            Tower tower = other.GetComponent<Tower>();
            if (tower != null)
            {
                tower.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}