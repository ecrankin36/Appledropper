using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<GameObject> baskets;
    public GameObject gameOverUIPrefab; // assign your UI prefab here

    [HideInInspector]
    public bool isGameOver = false;

    private GameObject gameOverInstance;

    public void LoseBasket()
    {
        if (baskets.Count > 0)
        {
            GameObject topBasket = baskets[baskets.Count - 1];
            baskets.RemoveAt(baskets.Count - 1);
            Destroy(topBasket);
        }

        if (baskets.Count == 0 && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            ShowGameOverUI();
        }
    }

    private void ShowGameOverUI()
	{
		if (gameOverUIPrefab == null) return;

		Canvas canvas = FindObjectOfType<Canvas>();
		if (canvas == null) return;

		if (gameOverInstance == null)
		{
			// Instantiate inactive
			gameOverInstance = Instantiate(gameOverUIPrefab, canvas.transform, false);
			gameOverInstance.SetActive(false);
		}

		// Activate now
		gameOverInstance.SetActive(true);

		// --- Add these lines here ---
		FadeInUI fade = gameOverInstance.GetComponent<FadeInUI>();
		if (fade != null) fade.PlayFade();
	}



}
