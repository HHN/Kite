using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BueroNovelImageController : NovelImageController
{
    [SerializeField] private GameObject decoPlantPrefab;
    [SerializeField] private GameObject decoPlantContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private AudioClip decoPlantAudio;
    [SerializeField] private Sprite[] animationFrames;
    private GameObject decoPlant = null;
    private GameObject character = null;

    void Start()
    {
        SetInitialSpirtesForImages();
    }

    private void SetInitialSpirtesForImages()
    {

        Image image = decoPlantPrefab.GetComponent<Image>();
        image.sprite = animationFrames[0];
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

        Vector3[] cornersDecoPlant = new Vector3[4];
        decoPlantRectTransform.GetWorldCorners(cornersDecoPlant);
        Vector3 bottomLeftDecoPlant = cornersDecoPlant[0];
        Vector3 topRightDecoPlant = cornersDecoPlant[2];
        if (x >= bottomLeftDecoPlant.x && x <= topRightDecoPlant.x &&
            y >= bottomLeftDecoPlant.y && y <= topRightDecoPlant.y)
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
                image.sprite = animationFrames[1];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[2];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[0];
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