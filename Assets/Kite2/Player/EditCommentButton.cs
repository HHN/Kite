using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditCommentButton : MonoBehaviour, OnSuccessHandler
{
    public long commentId;
    public bool isOwnComment;
    public string commentText;
    public Button button;
    public GameObject editCommentServerCallPrefab;
    public CommentSectionSceneController commentSectionSceneController;
    public CommentSectionInputArea commentSectionInputArea;
    TMP_InputField inputField;
    public GameObject parent;
    public GameObject marking;
    public bool inEdit;

    private void Start()
    {
        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE || !isOwnComment)
        {
            parent.SetActive(false);
            return;
        }
        button.onClick.AddListener(delegate { OnClick(); });
        commentSectionSceneController = GameObject.Find("Controller").GetComponent<CommentSectionSceneController>();
        commentSectionInputArea = GameObject.Find("InputArea").GetComponent<CommentSectionInputArea>();
        inputField = commentSectionInputArea.inputField;

        if (!commentSectionInputArea.inPostMode)
        {
            if (commentId == commentSectionInputArea.idOfCommentInEdit)
            {
                ActivateEditMode(false);
            }
        }
    }

    public void OnClick()
    {
        if (inEdit)
        {
            DeactivateEditMode(true);
        }
        else
        {
            ActivateEditMode(true);
        }
    }

    public void OnChangeButton()
    {
        ChangeCommentServerCall call = Instantiate(editCommentServerCallPrefab).GetComponent<ChangeCommentServerCall>();
        call.sceneController = commentSectionSceneController;
        call.onSuccessHandler = this;
        call.id = commentId;
        call.comment = inputField.text.Trim();
        call.SendRequest();
        DeactivateEditMode(true);
    }

    public void ActivateEditMode(bool setCommentText)
    {
        marking.SetActive(true);
        inEdit = true;
        if (commentSectionInputArea.editButton != null)
        {
            commentSectionInputArea.editButton.DeactivateEditMode(false);
        }
        commentSectionInputArea.editButton = this;
        commentSectionInputArea.ActivateEditMode(commentId);

        if (setCommentText)
        {
            inputField.text = commentText;
        }
    }

    public void DeactivateEditMode(bool setTextToNull)
    {
        marking.SetActive(false);
        inEdit = false;
        commentSectionInputArea.editButton = null;
        commentSectionInputArea.DeactivateEditMode();

        if (setTextToNull)
        {
            inputField.text = string.Empty;
        }
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        commentSectionSceneController.SetComments(comments);
    }
}
