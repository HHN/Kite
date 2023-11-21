using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetailsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject goToSettingsWrapper;
    [SerializeField] private GameObject characterComesSettingsWrapper;
    [SerializeField] private GameObject comesGoesSettingsWrapper;
    [SerializeField] private GameObject comesWithEmotionSettingsWrapper;
    [SerializeField] private GameObject characterSpeakesSettingsWrapper;
    [SerializeField] private GameObject textToSpeakSettingsWrapper;
    [SerializeField] private GameObject speakWithEmotionSettingsWrapper;

    [SerializeField] private DialogueNode dialogueNode;
    [SerializeField] private DialogueNodeWrapper dialogueNodeWrapper;

    [SerializeField] private TMP_InputField nodeNameInputField;
    [SerializeField] private TMP_InputField textToSpeakInputField;
    [SerializeField] private TMP_Dropdown typeSettingsDropDown;
    [SerializeField] private TMP_Dropdown goToSettingsDropDown;
    [SerializeField] private TMP_Dropdown characterComesSettingsDropDown;
    [SerializeField] private TMP_Dropdown comesOrGoesSettingsDropDown;
    [SerializeField] private TMP_Dropdown comesWithEmotionSettingsDropDown;
    [SerializeField] private TMP_Dropdown characterSpeaksSettingsDropDown;
    [SerializeField] private TMP_Dropdown speaksWithEmotionSettingsDropDown;

    [SerializeField] private Animator detailsPanelAnimator;

    private void Start()
    {
    }

    public void Initialize(DialogueNode dialogueNode, DialogueNodeWrapper wrapper)
    {
        this.dialogueNode = dialogueNode;
        this.dialogueNodeWrapper = wrapper;

        if (dialogueNode == null) 
        { 
            return; 
        }

        this.ShowFieldsOfNode(dialogueNode);

        this.nodeNameInputField.text = dialogueNode.GetNodeName();
        this.textToSpeakInputField.text = dialogueNode.GetTextToTalk();
        this.typeSettingsDropDown.value = GetIndexOfValueForNodeTypeSettings(dialogueNode.GetNodeType());
        this.goToSettingsDropDown.value = GetIndexOfValueForGoToSettings(dialogueNode.GetGoToEnvironment());
        this.characterComesSettingsDropDown.value = GetIndexOfValueForCharacterComesSettings(dialogueNode.GetCharacterComes());
        this.comesOrGoesSettingsDropDown.value = GetIndexOfValueForCharacterComesOrGoesSettings(dialogueNode.GetCharacterComesDirection());
        this.comesWithEmotionSettingsDropDown.value = GetIndexOfValueForCharacterComesWithEmotionSettings(dialogueNode.GetEmotionWhileComming());
        this.characterSpeaksSettingsDropDown.value = GetIndexOfValueForCharacterSpeaksSettings(dialogueNode.GetCharacterTalkes());
        this.speaksWithEmotionSettingsDropDown.value = GetIndexOfValueForSpeaksWithEmotionSettings(dialogueNode.GetEmotionWhileTalking());

        ValidateDetailsPanel();
    }

    public void OnConfirmButton()
    {
        if (this.nodeNameInputField.text == string.Empty || this.nodeNameInputField.text == "")
        {
            return;
        }

        SafeChanges();
        dialogueNodeWrapper.OnFinishEdit();
        CloseDetailsPanel();
    }

    public void OnCancelButton()
    {
        CloseDetailsPanel();
    }

    public void OpenDetailsPanel()
    {
        detailsPanelAnimator.SetBool("isOpen", true);
    }

    public void CloseDetailsPanel()
    {
        detailsPanelAnimator.SetBool("isOpen", false);
    }

    public bool IsNodeSatisfied()
    {
        if (this.nodeNameInputField.text == string.Empty || this.nodeNameInputField.text == "")
        {
            return false;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.NONE)
        {
            return false;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.CHANGE_LOCATION)
        {
            if (GetValueForGoToSettingsFromIndex(this.goToSettingsDropDown.value) == LocationEnum.NONE)
            {
                return false;
            }
            return true;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.CHARACTER_COMES_OR_GOES)
        {
            if (GetValueForCharacterComesSettingsFromIndex(this.characterComesSettingsDropDown.value) == CharacterEnum.NONE)
            {
                return false;
            }
            if (GetValueForCharacterComesOrGoesSettingsFromIndex(this.comesOrGoesSettingsDropDown.value) == DirectionEnum.NONE)
            {
                return false;
            }
            if (GetValueForCharacterComesWithEmotionSettingsFromIndex(this.comesWithEmotionSettingsDropDown.value) == EmotionEnum.NONE)
            {
                return false;
            }
            return true;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.CHARACTER_SPEAKS)
        {
            if (this.textToSpeakInputField.text == string.Empty || this.textToSpeakInputField.text == "")
            {
                return false;
            }
            if (GetValueForCharacterSpeaksSettingsFromIndex(this.characterSpeaksSettingsDropDown.value) == CharacterEnum.NONE)
            {
                return false;
            }
            if (GetValueForSpeaksWithEmotionSettingsFromIndex(this.speaksWithEmotionSettingsDropDown.value) == EmotionEnum.NONE)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    private void SafeChanges()
    {
        if (this.dialogueNode == null)
        {
            return;
        }

        this.dialogueNode.SetNodeName(this.nodeNameInputField.text);
        this.dialogueNode.SetTextToTalk(this.textToSpeakInputField.text);
        this.dialogueNode.SetNodeType(GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value));
        this.dialogueNode.SetGoToEnvironment(GetValueForGoToSettingsFromIndex(this.goToSettingsDropDown.value));
        this.dialogueNode.SetCharacterComes(GetValueForCharacterComesSettingsFromIndex(this.characterComesSettingsDropDown.value));
        this.dialogueNode.SetCharacterComesDirection(GetValueForCharacterComesOrGoesSettingsFromIndex(this.comesOrGoesSettingsDropDown.value));
        this.dialogueNode.SetEmotionWhileComming(GetValueForCharacterComesWithEmotionSettingsFromIndex(this.comesWithEmotionSettingsDropDown.value));
        this.dialogueNode.SetCharacterTalkes(GetValueForCharacterSpeaksSettingsFromIndex(this.characterSpeaksSettingsDropDown.value));
        this.dialogueNode.SetEmotionWhileTalking(GetValueForSpeaksWithEmotionSettingsFromIndex(this.speaksWithEmotionSettingsDropDown.value));
    }

    private int GetIndexOfValueForNodeTypeSettings(DialogueNodeType dialogueNodeType)
    {
        switch (dialogueNodeType)
        {
            case DialogueNodeType.NONE: return 0;
            case DialogueNodeType.CHANGE_LOCATION: return 1;
            case DialogueNodeType.CHARACTER_COMES_OR_GOES: return 2;
            case DialogueNodeType.CHARACTER_SPEAKS: return 3;
            default:  return -1;
        }
    }

    private int GetIndexOfValueForGoToSettings(LocationEnum locationEnum)
    {
        switch (locationEnum)
        {
            case LocationEnum.NONE: return 0;
            case LocationEnum.OFFICE: return 1;
            default: return -1;
        }
    }

    private int GetIndexOfValueForCharacterComesSettings(CharacterEnum characterEnum)
    {
        switch (characterEnum)
        {
            case CharacterEnum.NONE: return 0;
            case CharacterEnum.MR_MAYER: return 1;
            default: return -1;
        }
    }

    private int GetIndexOfValueForCharacterComesOrGoesSettings(DirectionEnum directionEnum)
    {
        switch (directionEnum)
        {
            case DirectionEnum.NONE: return 0;
            case DirectionEnum.COMES: return 1;
            case DirectionEnum.GOES: return 2;
            default: return -1;
        }
    }

    private int GetIndexOfValueForCharacterComesWithEmotionSettings(EmotionEnum emotionEnum)
    {
        switch (emotionEnum)
        {
            case EmotionEnum.NONE: return 0;
            case EmotionEnum.PLEASED: return 1;
            case EmotionEnum.ANGRY: return 2;
            case EmotionEnum.HORRIFIED: return 3;
            case EmotionEnum.GRUMPY: return 4;
            case EmotionEnum.HOPELESS: return 5;
            case EmotionEnum.EXCITED: return 6;
            case EmotionEnum.LAUGHING: return 7;
            case EmotionEnum.WEARY: return 8;
            case EmotionEnum.HAPPY: return 9;
            case EmotionEnum.SAD: return 10;
            case EmotionEnum.UNCERTAIN: return 11;
            case EmotionEnum.SURPRISED: return 12;
            default: return -1;
        }
    }

    private int GetIndexOfValueForCharacterSpeaksSettings(CharacterEnum characterEnum)
    {
        switch (characterEnum)
        {
            case CharacterEnum.NONE: return 0;
            case CharacterEnum.INFO_OR_INTRO_OR_OUTRO: return 1;
            case CharacterEnum.ME: return 2;
            case CharacterEnum.MR_MAYER: return 3;
            default: return -1;
        }
    }

    private int GetIndexOfValueForSpeaksWithEmotionSettings(EmotionEnum emotionEnum)
    {
        switch (emotionEnum)
        {
            case EmotionEnum.NONE: return 0;
            case EmotionEnum.PLEASED: return 1;
            case EmotionEnum.ANGRY: return 2;
            case EmotionEnum.HORRIFIED: return 3;
            case EmotionEnum.GRUMPY: return 4;
            case EmotionEnum.HOPELESS: return 5;
            case EmotionEnum.EXCITED: return 6;
            case EmotionEnum.LAUGHING: return 7;
            case EmotionEnum.WEARY: return 8;
            case EmotionEnum.HAPPY: return 9;
            case EmotionEnum.SAD: return 10;
            case EmotionEnum.UNCERTAIN: return 11;
            case EmotionEnum.SURPRISED: return 12;
            default: return -1;
        }
    }

    private DialogueNodeType GetValueForNodeTypeSettingsFromIndex(int index)
    {
        switch (index)
        {
            case 0: return DialogueNodeType.NONE;
            case 1: return DialogueNodeType.CHANGE_LOCATION;
            case 2: return DialogueNodeType.CHARACTER_COMES_OR_GOES;
            case 3: return DialogueNodeType.CHARACTER_SPEAKS;
            default: return DialogueNodeType.NONE;
        }
    }

    private LocationEnum GetValueForGoToSettingsFromIndex(int index)
    {
        switch (index)
        {
            case 0: return LocationEnum.NONE;
            case 1: return LocationEnum.OFFICE;
            default: return LocationEnum.NONE;
        }
    }

    private CharacterEnum GetValueForCharacterComesSettingsFromIndex(int index)
    {
        switch (index)
        {
            case 0: return CharacterEnum.NONE;
            case 1: return CharacterEnum.MR_MAYER;
            default: return CharacterEnum.NONE;
        }
    }

    private DirectionEnum GetValueForCharacterComesOrGoesSettingsFromIndex(int index)
    {
        switch (index)
        {
            case 0: return DirectionEnum.NONE;
            case 1: return DirectionEnum.COMES;
            case 2: return DirectionEnum.GOES;
            default: return DirectionEnum.NONE;
        }
    }

    private EmotionEnum GetValueForCharacterComesWithEmotionSettingsFromIndex(int index)
    {
        switch (index)
        {
            case 0: return EmotionEnum.NONE;
            case 1: return EmotionEnum.PLEASED;
            case 2: return EmotionEnum.ANGRY;
            case 3: return EmotionEnum.HORRIFIED;
            case 4: return EmotionEnum.GRUMPY;
            case 5: return EmotionEnum.HOPELESS;
            case 6: return EmotionEnum.EXCITED;
            case 7: return EmotionEnum.LAUGHING;
            case 8: return EmotionEnum.WEARY;
            case 9: return EmotionEnum.HAPPY;
            case 10: return EmotionEnum.SAD;
            case 11: return EmotionEnum.UNCERTAIN;
            case 12: return EmotionEnum.SURPRISED;
            default: return EmotionEnum.NONE;
        }
    }

    private CharacterEnum GetValueForCharacterSpeaksSettingsFromIndex(int index)
    {
        switch (index)
        {
            case 0: return CharacterEnum.NONE;
            case 1: return CharacterEnum.INFO_OR_INTRO_OR_OUTRO;
            case 2: return CharacterEnum.ME;
            case 3: return CharacterEnum.MR_MAYER;
            default: return CharacterEnum.NONE;
        }
    }

    private EmotionEnum GetValueForSpeaksWithEmotionSettingsFromIndex(int index)
    {
        switch (index)
        {
            case 0: return EmotionEnum.NONE;
            case 1: return EmotionEnum.PLEASED;
            case 2: return EmotionEnum.ANGRY;
            case 3: return EmotionEnum.HORRIFIED;
            case 4: return EmotionEnum.GRUMPY;
            case 5: return EmotionEnum.HOPELESS;
            case 6: return EmotionEnum.EXCITED;
            case 7: return EmotionEnum.LAUGHING;
            case 8: return EmotionEnum.WEARY;
            case 9: return EmotionEnum.HAPPY;
            case 10: return EmotionEnum.SAD;
            case 11: return EmotionEnum.UNCERTAIN;
            case 12: return EmotionEnum.SURPRISED;
            default: return EmotionEnum.NONE;
        }
    }

    private void ShowFieldsOfNode(DialogueNode dialogueNode)
    {
        if (dialogueNode == null) 
        { 
            return; 
        }

        switch (dialogueNode.GetNodeType())
        {
            case DialogueNodeType.NONE: ShowFieldForNodeTypeNone(); return;
            case DialogueNodeType.CHANGE_LOCATION: ShowFieldForNodeTypeChangeLocation(); return;
            case DialogueNodeType.CHARACTER_COMES_OR_GOES: ShowFieldForNodeTypeCharacterComesOrGoes(); return;
            case DialogueNodeType.CHARACTER_SPEAKS: ShowFieldForNodeTypeCharacterSpeaks(); return;
            default: return;
        }
    }

    private void ShowFieldForNodeTypeNone()
    {
        this.goToSettingsWrapper.SetActive(false);
        this.characterComesSettingsWrapper.SetActive(false);
        this.comesGoesSettingsWrapper.SetActive(false);
        this.comesWithEmotionSettingsWrapper.SetActive(false);
        this.characterSpeakesSettingsWrapper.SetActive(false);
        this.textToSpeakSettingsWrapper.SetActive(false);
        this.speakWithEmotionSettingsWrapper.SetActive(false);
    }

    private void ShowFieldForNodeTypeChangeLocation()
    {
        this.goToSettingsWrapper.SetActive(true);
        this.characterComesSettingsWrapper.SetActive(false);
        this.comesGoesSettingsWrapper.SetActive(false);
        this.comesWithEmotionSettingsWrapper.SetActive(false);
        this.characterSpeakesSettingsWrapper.SetActive(false);
        this.textToSpeakSettingsWrapper.SetActive(false);
        this.speakWithEmotionSettingsWrapper.SetActive(false);
    }

    private void ShowFieldForNodeTypeCharacterComesOrGoes()
    {
        this.goToSettingsWrapper.SetActive(false);
        this.characterComesSettingsWrapper.SetActive(true);
        this.comesGoesSettingsWrapper.SetActive(true);
        this.comesWithEmotionSettingsWrapper.SetActive(true);
        this.characterSpeakesSettingsWrapper.SetActive(false);
        this.textToSpeakSettingsWrapper.SetActive(false);
        this.speakWithEmotionSettingsWrapper.SetActive(false);
    }

    private void ShowFieldForNodeTypeCharacterSpeaks()
    {
        this.goToSettingsWrapper.SetActive(false);
        this.characterComesSettingsWrapper.SetActive(false);
        this.comesGoesSettingsWrapper.SetActive(false);
        this.comesWithEmotionSettingsWrapper.SetActive(false);
        this.characterSpeakesSettingsWrapper.SetActive(true);
        this.textToSpeakSettingsWrapper.SetActive(true);
        this.speakWithEmotionSettingsWrapper.SetActive(true);
    }

    public void OnValueChangedForTypeDropdown()
    {
        switch (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value))
        {
            case DialogueNodeType.NONE: ShowFieldForNodeTypeNone(); return;
            case DialogueNodeType.CHANGE_LOCATION: ShowFieldForNodeTypeChangeLocation(); return;
            case DialogueNodeType.CHARACTER_COMES_OR_GOES: ShowFieldForNodeTypeCharacterComesOrGoes(); return;
            case DialogueNodeType.CHARACTER_SPEAKS: ShowFieldForNodeTypeCharacterSpeaks(); return;
            default: return;
        }
    }

    public void ValidateDetailsPanel()
    {
        nodeNameInputField.GetComponent<Image>().color = new Color(1,1,1);
        typeSettingsDropDown.GetComponent<Image>().color = new Color(1,1,1);
        goToSettingsDropDown.GetComponent<Image>().color = new Color(1,1,1);
        characterComesSettingsDropDown.GetComponent<Image>().color = new Color(1,1,1);
        comesOrGoesSettingsDropDown.GetComponent<Image>().color = new Color(1,1,1);
        comesWithEmotionSettingsDropDown.GetComponent<Image>().color = new Color(1,1,1);
        textToSpeakInputField.GetComponent<Image>().color = new Color(1,1,1);
        characterSpeaksSettingsDropDown.GetComponent<Image>().color = new Color(1,1,1);
        speaksWithEmotionSettingsDropDown.GetComponent<Image>().color = new Color(1,1,1);

        Color yellow = new Color((254f / 255f), (235f / 255f), (24f / 255f));

        if (this.nodeNameInputField.text == string.Empty || this.nodeNameInputField.text == "")
        {
            nodeNameInputField.GetComponent<Image>().color = yellow;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.NONE)
        {
            typeSettingsDropDown.GetComponent<Image>().color = yellow;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.CHANGE_LOCATION)
        {
            if (GetValueForGoToSettingsFromIndex(this.goToSettingsDropDown.value) == LocationEnum.NONE)
            {
                goToSettingsDropDown.GetComponent<Image>().color = yellow;
            }
            return;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.CHARACTER_COMES_OR_GOES)
        {
            if (GetValueForCharacterComesSettingsFromIndex(this.characterComesSettingsDropDown.value) == CharacterEnum.NONE)
            {
                characterComesSettingsDropDown.GetComponent<Image>().color = yellow;
            }
            if (GetValueForCharacterComesOrGoesSettingsFromIndex(this.comesOrGoesSettingsDropDown.value) == DirectionEnum.NONE)
            {
                comesOrGoesSettingsDropDown.GetComponent<Image>().color = yellow;
            }
            if (GetValueForCharacterComesWithEmotionSettingsFromIndex(this.comesWithEmotionSettingsDropDown.value) == EmotionEnum.NONE)
            {
                comesWithEmotionSettingsDropDown.GetComponent<Image>().color = yellow;
            }
            return;
        }

        if (GetValueForNodeTypeSettingsFromIndex(this.typeSettingsDropDown.value) == DialogueNodeType.CHARACTER_SPEAKS)
        {
            if (this.textToSpeakInputField.text == string.Empty || this.textToSpeakInputField.text == "")
            {
                textToSpeakInputField.GetComponent<Image>().color = yellow;
            }
            if (GetValueForCharacterSpeaksSettingsFromIndex(this.characterSpeaksSettingsDropDown.value) == CharacterEnum.NONE)
            {
                characterSpeaksSettingsDropDown.GetComponent<Image>().color = yellow;
            }
            if (GetValueForSpeaksWithEmotionSettingsFromIndex(this.speaksWithEmotionSettingsDropDown.value) == EmotionEnum.NONE)
            {
                speaksWithEmotionSettingsDropDown.GetComponent<Image>().color = yellow;
            }
            return;
        }
    }
}
