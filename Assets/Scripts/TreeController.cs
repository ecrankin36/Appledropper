using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 2f;        // how far left/right to move
    public float baseMoveSpeed = 1f;       // starting speed
    public float maxMoveSpeed = 2.5f;      // maximum speed

    [Header("Apple Dropping")]
    public GameObject applePrefab;         
    public float timeBetweenSets = 5f;     
    public float minTimeBetweenApples = 0.25f; // min random delay
    public float maxTimeBetweenApples = 0.4f;  // max random delay

    private Vector3 startPos;
    private bool isDroppingSet = false;    
    private ScoreManager scoreManager;
    private GameManager gameManager;

    private float phase = 0f; // phase accumulator for smooth sine motion

    void Start()
    {
        startPos = transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();

        // start first set immediately
        if (gameManager != null && !gameManager.isGameOver)
            StartCoroutine(DropAppleSet(CalculateApplesPerSet()));
    }

    void Update()
    {
        int score = scoreManager != null ? scoreManager.applesCaught : 0;

        // Calculate speed bonus per multiple of 5, capped at maxMoveSpeed
        float speedBonus = Mathf.Min((score / 5) * 0.1f, maxMoveSpeed - baseMoveSpeed);
        float moveSpeed = baseMoveSpeed + speedBonus;

        // Increment phase based on moveSpeed for smooth movement
        phase += Time.deltaTime * moveSpeed;
        float xOffset = Mathf.Sin(phase) * moveDistance;
        transform.position = startPos + new Vector3(xOffset, 0, 0);

        // Start next apple set if not currently dropping and game is not over
        if (!isDroppingSet && scoreManager != null && gameManager != null && !gameManager.isGameOver)
        {
            isDroppingSet = true;
            StartCoroutine(DropAppleSet(CalculateApplesPerSet()));
        }
    }

    private int CalculateApplesPerSet()
    {
        int score = scoreManager != null ? scoreManager.applesCaught : 0;
        return Mathf.Max(score / 2, 1);
    }

    private IEnumerator DropAppleSet(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Stop if game is over
            if (gameManager != null && gameManager.isGameOver)
            {
                isDroppingSet = false;
                yield break;
            }

            DropApple();
            // wait random time between apples
            yield return new WaitForSeconds(Random.Range(minTimeBetweenApples, maxTimeBetweenApples));
        }

        // wait timeBetweenSets after set if game not over
        if (gameManager == null || !gameManager.isGameOver)
        {
            yield return new WaitForSeconds(timeBetweenSets);
            isDroppingSet = false;
        }
    }

    private void DropApple()
    {
        if (applePrefab != null && (gameManager == null || !gameManager.isGameOver))
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0f);
            Instantiate(applePrefab, spawnPos, Quaternion.identity);
        }
    }
}
