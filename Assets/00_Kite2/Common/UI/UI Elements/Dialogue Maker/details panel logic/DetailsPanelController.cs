using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject goToSettingsWrapper;
    [SerializeField] private GameObject characterComesSettingsWrapper;
    [SerializeField] private GameObject comesGoesSettingsWrapper;
    [SerializeField] private GameObject comesWithEmotionSettingsWrapper;
    [SerializeField] private GameObject characterSpeakesSettingsWrapper;
    [SerializeField] private GameObject textToSpeakSettingsWrapper;
    [SerializeField] private GameObject speakWithEmotionSettingsWrapper;

    [SerializeField] private DialogueNodeType dialogueNodeType;

    [SerializeField] private TMP_Dropdown typeSettingsDropDown;
    [SerializeField] private TMP_Dropdown goToSettingsDropDown;
    [SerializeField] private TMP_Dropdown characterComesSettingsDropDown;
    [SerializeField] private TMP_Dropdown comesOrGoesSettingsDropDown;
    [SerializeField] private TMP_Dropdown comesWithEmotionSettingsDropDown;
    [SerializeField] private TMP_Dropdown characterSpeaksSettingsDropDown;
    [SerializeField] private TMP_Dropdown speaksWithEmotionSettingsDropDown;

    private void Start()
    {
        SetDialogueNodeType(DialogueNodeType.NONE);
    }

    public void SetDialogueNodeType(DialogueNodeType dialogueNodeType)
    {
        switch (dialogueNodeType)
        {
            case DialogueNodeType.NONE: SetDialogueNodeTypeToNone(); return;
            case DialogueNodeType.CHANGE_LOCATION: SetDialogueNodeTypeToChangeLocation(); return;
            case DialogueNodeType.CHARACTER_COMES_OR_GOES: SetDialogueNodeTypeToCharacterComesOrGoes(); return;
            case DialogueNodeType.CHARACTER_SPEAKS: SetDialogueNodeTypeToCharacterSpeaks(); return;
            default: return;
        }
    }

    private void SetDialogueNodeTypeToNone()
    {
        this.dialogueNodeType = DialogueNodeType.NONE;

        this.goToSettingsWrapper.SetActive(false);
        this.characterComesSettingsWrapper.SetActive(false);
        this.comesGoesSettingsWrapper.SetActive(false);
        this.comesWithEmotionSettingsWrapper.SetActive(false);
        this.characterSpeakesSettingsWrapper.SetActive(false);
        this.textToSpeakSettingsWrapper.SetActive(false);
        this.speakWithEmotionSettingsWrapper.SetActive(false);
    }

    private void SetDialogueNodeTypeToChangeLocation()
    {
        this.dialogueNodeType = DialogueNodeType.CHANGE_LOCATION;

        this.goToSettingsWrapper.SetActive(true);
        this.characterComesSettingsWrapper.SetActive(false);
        this.comesGoesSettingsWrapper.SetActive(false);
        this.comesWithEmotionSettingsWrapper.SetActive(false);
        this.characterSpeakesSettingsWrapper.SetActive(false);
        this.textToSpeakSettingsWrapper.SetActive(false);
        this.speakWithEmotionSettingsWrapper.SetActive(false);
    }

    private void SetDialogueNodeTypeToCharacterComesOrGoes()
    {
        this.dialogueNodeType = DialogueNodeType.CHARACTER_COMES_OR_GOES;

        this.goToSettingsWrapper.SetActive(false);
        this.characterComesSettingsWrapper.SetActive(true);
        this.comesGoesSettingsWrapper.SetActive(true);
        this.comesWithEmotionSettingsWrapper.SetActive(true);
        this.characterSpeakesSettingsWrapper.SetActive(false);
        this.textToSpeakSettingsWrapper.SetActive(false);
        this.speakWithEmotionSettingsWrapper.SetActive(false);
    }

    private void SetDialogueNodeTypeToCharacterSpeaks()
    {
        this.dialogueNodeType = DialogueNodeType.CHARACTER_SPEAKS;

        this.goToSettingsWrapper.SetActive(false);
        this.characterComesSettingsWrapper.SetActive(false);
        this.comesGoesSettingsWrapper.SetActive(false);
        this.comesWithEmotionSettingsWrapper.SetActive(false);
        this.characterSpeakesSettingsWrapper.SetActive(true);
        this.textToSpeakSettingsWrapper.SetActive(true);
        this.speakWithEmotionSettingsWrapper.SetActive(true);
    }
}
