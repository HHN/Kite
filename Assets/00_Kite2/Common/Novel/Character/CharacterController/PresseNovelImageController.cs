using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Common.Novel.Character.CharacterController;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = _00_Kite2.Common.Novel.Character.CharacterController.CharacterController;

public class PresseNovelImageController : NovelImageController
{
    [SerializeField] private GameObject decoVasePrefab;
    [SerializeField] private GameObject decoVaseContainer;
    [SerializeField] private GameObject decoGlasPrefab;
    [SerializeField] private GameObject decoGlasContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private AudioClip decoGlasAudio;
    [SerializeField] private AudioClip decoVaseAudio;
    [SerializeField] private Sprite[] animationFramesVase;
    [SerializeField] private Sprite[] animationFramesGlas;
    private GameObject decoGlas = null;
    private GameObject decoVase = null;
    private GameObject character = null;

    void Start()
    {
        SetInitialSpirtesForImages();
    }

    private void SetInitialSpirtesForImages()
    {

        Image image = decoVasePrefab.GetComponent<Image>();
        image.sprite = animationFramesVase[0];
        if (decoVaseContainer.transform.childCount > 0)
        {
            Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
        }
        Instantiate(decoVasePrefab, decoVaseContainer.transform);

        image = decoGlasPrefab.GetComponent<Image>();
        image.sprite = animationFramesGlas[0];
        if (decoGlasContainer.transform.childCount > 0)
        {
            Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
        }
        Instantiate(decoGlasPrefab, decoGlasContainer.transform);
    }


    public override void SetCharacter()
    {
        CharacterController = characterPrefab.GetComponent<CharacterController>();
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        // Check if animations are allowed to proceed, return false if disabled
        if (AnimationFlagSingleton.Instance().GetFlag() == false)
        {
            return false;
        }

        // Get the RectTransforms of the objects to detect touch within their bounds
        RectTransform decoVaseRectTransform = decoVaseContainer.GetComponent<RectTransform>();
        RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();

        // Get the world corners of the vase decoration container
        Vector3[] cornersDecoVase = new Vector3[4];
        decoVaseRectTransform.GetWorldCorners(cornersDecoVase);
        Vector3 bottomLeftDecoVase = cornersDecoVase[0];
        Vector3 topRightDecoVase = cornersDecoVase[2];

        // Get the world corners of the glass decoration container
        Vector3[] cornersDecoGlas = new Vector3[4];
        decoGlasRectTransform.GetWorldCorners(cornersDecoGlas);
        Vector3 bottomLeftDecoGlas = cornersDecoGlas[0];
        Vector3 topRightDecoGlas = cornersDecoGlas[2];

        // Check if the touch coordinates are within the vase decoration bounds
        if (x >= bottomLeftDecoVase.x && x <= topRightDecoVase.x &&
            y >= bottomLeftDecoVase.y && y <= topRightDecoVase.y)
        {
            StartCoroutine(OnDecoVase(audioSource));
            return true;
        }
        // Check if the touch coordinates are within the glass decoration bounds
        else if (x >= bottomLeftDecoGlas.x && x <= topRightDecoGlas.x &&
                    y >= bottomLeftDecoGlas.y && y <= topRightDecoGlas.y)
        {
            StartCoroutine(OnDecoGlas(audioSource));
            return true;
        }

        // Return false if the touch is outside both bounds
        return false;
    }

    private IEnumerator OnDecoVase(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoVaseAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoVasePrefab.GetComponent<Image>();
                image.sprite = animationFramesVase[1];
                Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                Instantiate(decoVasePrefab, decoVaseContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesVase[2];
                Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                Instantiate(decoVasePrefab, decoVaseContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesVase[0];
                Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                Instantiate(decoVasePrefab, decoVaseContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }

    private IEnumerator OnDecoGlas(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoGlasAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoGlasPrefab.GetComponent<Image>();
                image.sprite = animationFramesGlas[1];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[2];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[3];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[4];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[5];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[6];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[0];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }
}

