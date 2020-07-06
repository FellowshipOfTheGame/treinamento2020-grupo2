using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 50;

    [Header("Player")]
    [SerializeField] GameObject player;

    private PlayerLife playerLife;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            playerLife.TakeDamage(damage);
        }
    }
}
