using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureManager : MonoBehaviour
{
    [Header("Honk Components")]
    public AudioSource hjonk;

    public enum ProcedureState { ReleasingBridge, StartUp, OpenShutters, Moving, WheelPosition, PositioningBridge }
    public enum StartupState { TestingLights, StartingUp }
    [Header("Procedure State")]
    public ProcedureState procedureState;
    public StartupState startupState;

    //Header For Releasing Components;

    [Header("Startup Components")]
    public bool testedLight = false;
    public bool startIsActive = false;

    public bool handTimerActive = false;

    public float handTimer;

    IEnumerator handActivateTimer;

    [Header("Open Shutters Components")]
    public bool shuttersOpened = false;

    [Header("Moving Components")]
    public bool test;

    [Header("Debug")]
    [Range(1.0f, 100f)]public float gameSpeed;

    void Start()
    {
        handActivateTimer = HandActivateTimer();
    }

    void Update()
    {
        Time.timeScale = gameSpeed;

        switch (procedureState)
        {
            case ProcedureState.ReleasingBridge:
                //Insert Visual Elements Code here;

                procedureState = ProcedureState.StartUp;
                break;

            case ProcedureState.StartUp:
                switch (startupState)
                {
                    case StartupState.TestingLights:
                        if (!testedLight)
                        {
                            Debug.Log("Lights not Tested");
                        }
                        else if (testedLight)
                        {
                            startupState = StartupState.StartingUp;
                        }
                        break;

                    case StartupState.StartingUp:
                        if (!startIsActive)
                        {
                            Debug.Log("Hand button not Pressed");

                            if(!handTimerActive)
                            {
                                handTimerActive = true;

                                StartCoroutine(handActivateTimer);
                            }
                        }
                        else if (startIsActive)
                        {
                            StopCoroutine(handActivateTimer);

                            handTimerActive = false;

                            procedureState = ProcedureState.OpenShutters;
                        }
                        break;
                }
                break;

            case ProcedureState.OpenShutters:
                break;

            case ProcedureState.Moving:
                break;

            case ProcedureState.WheelPosition:
                break;

            case ProcedureState.PositioningBridge:
                break;
        }
    }

    public void PressedLightTestButton()
    {
        testedLight = true;
    }

    public void PressedStartupButton()
    {
        startIsActive = true;
    }

    public void PlayHjonk()
    {
        hjonk.Play();
    }

    public IEnumerator HandActivateTimer()
    {
        yield return new WaitForSeconds(handTimer);

        testedLight = false;
        startIsActive = false;

        handTimerActive = false;

        startupState = StartupState.TestingLights;
    }
}
