using UnityEngine;

public class BasketCatch : MonoBehaviour
{
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);  // remove the apple
            scoreManager.AddApple();        // increase the score
        }
    }
}
