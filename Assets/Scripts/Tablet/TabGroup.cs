using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [HideInInspector]
    public List<TheBackstage.TabButton> tabButtons;

    public Color tabHoover;
    public Color tabActive;
    public Color tabInactive;
    private TheBackstage.TabButton selectedTab;

    public List<GameObject> pages = new List<GameObject>();

    private void Awake()
    {
        tabButtons = new List<TheBackstage.TabButton>();
    }

    public void Subscribe(TheBackstage.TabButton tabButton)
    {
        tabButtons.Add(tabButton);   
    }

    public void OnTabEnter(TheBackstage.TabButton tabButton)
    {
        ResetTabs();
        if (selectedTab == null || tabButton != selectedTab)
        {
            tabButton.background.color = tabHoover;
        }        
    }

    public void OnTabExit(TheBackstage.TabButton tabButton)
    {
        ResetTabs();
    }

    public void OnTabSelected(TheBackstage.TabButton tabButton)
    {
        selectedTab = tabButton;
        tabButton.background.color = tabActive;
        ResetTabs();
        int index = tabButton.transform.GetSiblingIndex();
        for (int i=0; i<pages.Count; i++) 
        {
            if (i == index)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    private void ResetTabs()
    {
        foreach (TheBackstage.TabButton tabButton in tabButtons)
        {
            if (selectedTab != null && tabButton == selectedTab)
            {
                continue;
            }
            tabButton.background.color = tabInactive;
        }
    }
}
