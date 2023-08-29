using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentSectionInputArea : MonoBehaviour, OnSuccessHandler
{
    public Button postButton;
    public TMP_InputField inputField;
    public GameObject postCommentServerCallPrefab;
    public CommentSectionSceneController commentSectionSceneController;
    public bool inPostMode = true;
    public EditCommentButton editButton;
    public long idOfCommentInEdit;

    void Start()
    {
        postButton.onClick.AddListener(delegate { OnClick(); });

        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE)
        {
            postButton.interactable = false;
            postButton.gameObject.SetActive(false);
            inputField.text = "Du musst eingeloggt sein zum kommentieren!";
            inputField.interactable = false;
        }
    }

    public void OnClick()
    {
        if (inPostMode)
        {
            OnPostButton();
        } 
        else
        {
            OnUpdateButton();
        }
    }

    public void OnPostButton()
    {
        PostCommentServerCall call = Instantiate(postCommentServerCallPrefab).GetComponent<PostCommentServerCall>();
        call.sceneController = commentSectionSceneController;
        call.onSuccessHandler = this;
        call.visualNovelId = PlayManager.Instance().GetVisualNovelToPlay().id;
        call.comment = inputField.text.Trim();
        call.SendRequest();
        inputField.text = string.Empty;
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        commentSectionSceneController.SetComments(comments);
    }

    public void ActivateEditMode(long idOfCommentInEdit)
    {
        this.inPostMode = false;
        this.idOfCommentInEdit = idOfCommentInEdit;
        postButton.GetComponentInChildren<TextMeshProUGUI>().text = "SPEICHERN";
    }

    public void DeactivateEditMode()
    {
        this.inPostMode = true;
        this.idOfCommentInEdit = -1;
        postButton.GetComponentInChildren<TextMeshProUGUI>().text = "POSTEN";
    }

    public void OnUpdateButton()
    {
        editButton.OnChangeButton();
    }
}
