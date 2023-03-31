using System.Collections;
using UnityEngine;

public class ScaleAndMoveSprite : MonoBehaviour
{
    public float scaleDuration = 1.0f; // The duration of the scaling animation
    public float maxScale = 2.0f; // The maximum scale to which the sprite will be scaled

    private Vector3 originalScale; // The sprite's original local scale

    private void Awake()
    {
        originalScale = transform.localScale; // Store the sprite's original local scale
        StartCoroutine(ScaleCoroutine(maxScale)); // Start a new scaling coroutine that scales the sprite up to the specified maximum scale
    }

    private IEnumerator ScaleCoroutine(float targetScale)
    {
        float timeElapsed = 0.0f;
        Vector3 startScale = transform.localScale;

        while (true)
        {
            while (timeElapsed < scaleDuration)
            {
                float t = timeElapsed / scaleDuration;
                transform.localScale = Vector3.Lerp(startScale, originalScale * targetScale, t); // Scale the sprite based on the current time and the specified target scale
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.localScale = originalScale * targetScale; // Set the final scale of the sprite to the specified target scale
            yield return new WaitForSeconds(scaleDuration);

            timeElapsed = 0.0f;
            startScale = transform.localScale;

            while (timeElapsed < scaleDuration)
            {
                float t = timeElapsed / scaleDuration;
                transform.localScale = Vector3.Lerp(startScale, originalScale, t); // Scale the sprite back down to its original scale
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.localScale = originalScale; // Set the final scale of the sprite to its original scale
            yield return new WaitForSeconds(scaleDuration);

            timeElapsed = 0.0f;
            startScale = transform.localScale;
        }
    }
}
