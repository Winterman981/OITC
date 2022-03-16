using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFade : MonoBehaviour
{
    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeOut()
    {
        for(float f = 1f; f >= -0.05f; f-= 0.05f)
        {
            Color sc = lr.startColor;
            sc.a = f;
            lr.startColor = sc;
            yield return new WaitForSeconds(0.05f);
        }

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color ec = lr.endColor;
            ec.a = f;
            lr.endColor = ec;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(gameObject);
    }
}
