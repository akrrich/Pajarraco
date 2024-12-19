using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;
    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void GiveConsent()
    {     
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log("Consent given! We can get the data!!");
    }
    public void PowerUpPickUp(string powerUpAction)
    {
        PowerUpEvent evt = new PowerUpEvent
        {
            actionName = powerUpAction,
        };

        AnalyticsService.Instance.RecordEvent(evt);
        AnalyticsService.Instance.Flush();
    }
}