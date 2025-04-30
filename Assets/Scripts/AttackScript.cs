using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Destroy(collision.gameObject);
        }
    }
}
