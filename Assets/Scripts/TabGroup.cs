using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TabGroup : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] List<TabButton> tabs;
    [SerializeField] Transform panel;
    [SerializeField] Image shadow;

    TabButton currentTab;
    bool isOpen;

    public void Open()
    {
        gameObject.SetActive(true);
        isOpen = true;
        shadow.DOFade(0.75f, 0.5f);
        panel.transform.DOMoveX(360, 0.3f).SetEase(Ease.InOutCubic);
    }

    public void Close()
    {
        isOpen = false;
        shadow.DOFade(0, 0.25f);
        panel.transform.DOMoveX(-360, 0.3f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });        
    }

    public void Toggle()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    public void HideAll()
    {
        foreach (TabButton button in tabs)
        {
            button.tabContent.gameObject.SetActive(false);
        }
    }

    public void ClickTab(TabButton button)
    {
        if (currentTab != button)
        {
            header.text = button.tabName;
            button.tabContent.gameObject.SetActive(true);

            if (currentTab != null)
            {
                currentTab.tabContent.gameObject.SetActive(false);
            }

            currentTab = button;

            Close();
        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == shadow.gameObject)
        {
            Close();
        }
    }
}
