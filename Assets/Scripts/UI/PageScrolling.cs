using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PageScrolling : MonoBehaviour
{
    public ScrollRect scrollRect = null;
    public Button backBtn;
    public Button nextBtn;

    private float childSize = 0;
    private bool isMove = false;

    public void Init()
    {
        var child = scrollRect.content.GetChild(0);
        RectTransform rectChild = child.GetComponent<RectTransform>();
        childSize = rectChild.sizeDelta.x;
        settingButton();
        buttonActive();
        scorePage(GameManager.instance.CurrentPage, true);
    }

    private void settingButton()
    {
        int maxPage = scrollRect.content.childCount;
        nextBtn.onClick.AddListener(() =>
        {
            if (!isMove)
            {
                GameManager.instance.CurrentPage++;
                if (GameManager.instance.CurrentPage >= (maxPage - 1)) { GameManager.instance.CurrentPage = (maxPage - 1); }
                scorePage(GameManager.instance.CurrentPage);
                buttonActive();
            }
        });
        backBtn.onClick.AddListener(() =>
        {
            if (!isMove)
            {
                GameManager.instance.CurrentPage--;
                if (GameManager.instance.CurrentPage <= 0) { GameManager.instance.CurrentPage = 0; }
                scorePage(GameManager.instance.CurrentPage);
                buttonActive();
            }
        });
    }

    private void buttonActive()
    {
        int curPage = GameManager.instance.CurrentPage;
        int maxPage = scrollRect.content.childCount;
        int minPage = 0;
        nextBtn.interactable = !(curPage >= (maxPage - 1));
        backBtn.interactable = !(curPage <= minPage);
    }

    private void scorePage(int index, bool skip = false)
    {
        if (isMove) return;
        isMove = true;
        var targetPage = childSize * index;
        var content = scrollRect.content;
        float waitingTime = skip == true ? 0 : 0.2f;
        Vector3 target = new Vector3(-targetPage, 0, 0);
        content.DOLocalMove(target, waitingTime).OnComplete(() =>
        {
            isMove = false;
        });
    }
}
