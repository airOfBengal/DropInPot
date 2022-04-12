using System.Collections;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPosition = transform.position;
        var percentComplete = 0f;
        while (percentComplete < 1f)
        {
            percentComplete += Time.deltaTime / duration;
            var currentHeight = Mathf.Sin(Mathf.PI * percentComplete);
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) + Vector3.up * currentHeight * 1.5f;
            yield return null;
        }
    }
}
