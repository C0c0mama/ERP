using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TripSummaryTabs : MonoBehaviour
{
    [Header("Tab Content Panels")]
    public GameObject PanelHistory;
    public GameObject PanelUpcomingRides;
    public GameObject PanelFavorites;
    public GameObject PanelCancelled;

    void Start()
    {
        // Check all panels are assigned
        if (PanelHistory == null)
        {
            Debug.LogError("PanelHistory is NOT assigned in TripSummaryTabs!");
            return;
        }
        if (PanelUpcomingRides == null)
        {
            Debug.LogError("PanelUpcomingRides is NOT assigned in TripSummaryTabs!");
            return;
        }
        if (PanelFavorites == null)
        {
            Debug.LogError("PanelFavorites is NOT assigned in TripSummaryTabs!");
            return;
        }
        if (PanelCancelled == null)
        {
            Debug.LogError("PanelCancelled is NOT assigned in TripSummaryTabs!");
            return;
        }

        // Start on History tab
        SwitchTab(0);
    }

    public void ShowHistory() { SwitchTab(0); }
    public void ShowUpcoming() { SwitchTab(1); }
    public void ShowFavorites() { SwitchTab(2); }
    public void ShowCancelled() { SwitchTab(3); }

    void SwitchTab(int index)
    {
        PanelHistory.SetActive(false);
        PanelUpcomingRides.SetActive(false);
        PanelFavorites.SetActive(false);
        PanelCancelled.SetActive(false);

        switch (index)
        {
            case 0: PanelHistory.SetActive(true); break;
            case 1: PanelUpcomingRides.SetActive(true); break;
            case 2: PanelFavorites.SetActive(true); break;
            case 3: PanelCancelled.SetActive(true); break;
        }
    }
}

