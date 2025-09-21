using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FadeInUI : MonoBehaviour
{
    public float fadeDuration = 1f;

    private Image image;
    private Text uiText;
    private TextMeshProUGUI tmpText;

    void Awake()
    {
        image = GetComponent<Image>();
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();

        if (image == null && uiText == null && tmpText == null)
        {
            Debug.LogError("FadeInUI: No Image, Text, or TextMeshProUGUI component found!");
        }
    }

    public void PlayFade()
    {
        // Reset alpha to 0
        if (image != null)
        {
            Color c = image.color;
            c.a = 0f;
            image.color = c;
        }
        else if (uiText != null)
        {
            Color c = uiText.color;
            c.a = 0f;
            uiText.color = c;
        }
        else if (tmpText != null)
        {
            Color c = tmpText.color;
            c.a = 0f;
            tmpText.color = c;
        }

        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        float timer = 0f;

        while (true)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);

            if (image != null)
            {
                Color c = image.color;
                c.a = alpha;
                image.color = c;
            }
            else if (uiText != null)
            {
                Color c = uiText.color;
                c.a = alpha;
                uiText.color = c;
            }
            else if (tmpText != null)
            {
                Color c = tmpText.color;
                c.a = alpha;
                tmpText.color = c;
            }

            if (alpha >= 1f) break;
            yield return null;
		}
	}
}