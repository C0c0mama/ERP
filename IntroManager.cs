using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject Intro;
    public GameObject Tagline;
    public GameObject LogInOrRegister;

    [Header("Intro Settings")]
    public float IntroFadeInDuration = 1.0f;
    public float IntroHoldDuration = 1.5f;
    public float IntroFadeOutDuration = 1.0f;

    [Header("Tagline Timing")]
    public float TaglineFadeInDuration = 1.0f;
    public float TaglineHoldDuration = 2.0f;
    public float TaglineFadeOutDuration = 1.0f;

    private CanvasGroup IntroCG;
    private CanvasGroup TaglineCG;

    void Start()
    {
        IntroCG = GetOrAddCanvasGroup(Intro);
        TaglineCG = GetOrAddCanvasGroup(Tagline);

        //Hide everything at the start
        IntroCG.alpha = 0;
        TaglineCG.alpha = 0;

        //intro starts active but invisible
        Intro.SetActive(true);
        Tagline.SetActive(false);
        LogInOrRegister.SetActive(false);

        //Start the intro sequence
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence() 
    {
        yield return StartCoroutine(Fade(IntroCG, 0f, 1f, IntroFadeInDuration));
        yield return new WaitForSeconds(IntroHoldDuration);
        yield return StartCoroutine(Fade(IntroCG, 1f, 0f, IntroFadeOutDuration));
        Intro.SetActive(false);

        Tagline.SetActive(true);
        yield return StartCoroutine(Fade(TaglineCG, 0f, 1f, TaglineFadeInDuration));

        yield return new WaitForSeconds(TaglineHoldDuration);

        yield return StartCoroutine(Fade(TaglineCG, 1f, 0f, TaglineFadeOutDuration));
        Tagline.SetActive(false);

        LogInOrRegister.SetActive(true);
    }

    IEnumerator Fade(CanvasGroup cg, float from, float to, float duration) 
    {
        float elapsed = 0f;
        cg.alpha = from;

        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        cg.alpha = to; // Ensure it ends at the exact target alpha
    }

    CanvasGroup GetOrAddCanvasGroup(GameObject obj) 
    {
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg == null) 
        {
            cg = obj.AddComponent<CanvasGroup>();
        }
        return cg;
    }
}
