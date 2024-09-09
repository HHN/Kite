using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class BekannterNovelImageController : NovelImageController
{
    [SerializeField] private GameObject decoPlantPrefab;
    [SerializeField] private GameObject decoPlantContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private AudioClip decoPlantAudio;
    [SerializeField] private Sprite[] animationFramesPlant;
    private GameObject decoPlant = null;
    private GameObject character = null;

    void Start()
    {
        SetInitialSpirtesForImages();
    }

    private void SetInitialSpirtesForImages()
    {

        Image image = decoPlantPrefab.GetComponent<Image>();
        image.sprite = animationFramesPlant[0];
        if (decoPlantContainer.transform.childCount > 0)
        {
            Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
        }
        Instantiate(decoPlantPrefab, decoPlantContainer.transform);
    }

    public override void SetCharacter()
    {
        characterController = characterPrefab.GetComponent<CharacterController>();
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

        Vector3[] cornersDecoBackground = new Vector3[4];
        decoPlantRectTransform.GetWorldCorners(cornersDecoBackground);
        Vector3 bottomLeftDecoBackground = cornersDecoBackground[0];
        Vector3 topRightDecoBackground = cornersDecoBackground[2];


        if (x >= bottomLeftDecoBackground.x && x <= topRightDecoBackground.x &&
                   y >= bottomLeftDecoBackground.y && y <= topRightDecoBackground.y)
        {
            StartCoroutine(OnDecoPlant(audioSource));
            return true;
        }
        return false;
    }

    private IEnumerator OnDecoPlant(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoPlantAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoPlantPrefab.GetComponent<Image>();
                image.sprite = animationFramesPlant[1];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesPlant[2];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesPlant[0];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }
}