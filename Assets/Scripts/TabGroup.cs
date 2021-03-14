using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TabGroup : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] List<TabButton> tabs;
    [SerializeField] Transform shadow;

    TabButton currentTab;
    bool isOpen;

    public void Open()
    {
        gameObject.SetActive(true);
        isOpen = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        isOpen = false;
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
