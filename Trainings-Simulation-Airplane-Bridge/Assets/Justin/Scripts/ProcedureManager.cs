using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcedureManager : MonoBehaviour
{
    public string sceneToLoad;
    public PunishedBobMoveCode bob;

    [Header("Honk Components")]
    public AudioSource hjonk;

    public enum ProcedureState { ReleasingBridge, StartUp, OpenShutters, Moving, WheelPosition, PositioningBridge }
    public enum StartupState { TestingLights, StartingUp }
    [Header("Procedure State")]
    public ProcedureState procedureState;
    public StartupState startupState;

    [Header("Releasing Components")]
    public bool isReleased;

    [Header("Startup Components")]
    public bool testedLight = false;
    public bool startIsActive = false;

    public bool handTimerActive = false;

    public float handTimer;

    IEnumerator handActivateTimer;

    [Header("Open Shutters Components")]
    public bool shuttersOpened = false;

    [Header("Moving Components")]
    public bool doneMoving;

    [Header("Positioning Bridge")]
    public bool finished;

    [Header("Debug")]
    [Range(1.0f, 100f)]public float gameSpeed;

    void Start()
    {
        handActivateTimer = HandActivateTimer();

        procedureState = ProcedureState.ReleasingBridge;
    }

    void Update()
    {
        Time.timeScale = gameSpeed;

        if(bob.playerHasThumbUp)
        {
            isReleased = true;
        }

        switch (procedureState)
        {
            case ProcedureState.ReleasingBridge:
                if(!isReleased)
                {
                    Debug.Log("Bridge is not released");
                }else if(isReleased)
                {
                    procedureState = ProcedureState.StartUp;
                }
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
                if (!shuttersOpened)
                {
                    Debug.Log("Shutters are not opened");
                }
                else if (shuttersOpened)
                {
                    procedureState = ProcedureState.Moving;
                }
                break;

            case ProcedureState.Moving:
                if(!doneMoving)
                {
                    Debug.Log("Movement still in progress");
                }else if(doneMoving)
                {
                    procedureState = ProcedureState.PositioningBridge;
                }
                break;

            case ProcedureState.PositioningBridge:
                if(!finished)
                {
                    finished = true;
                }else if(finished)
                {
                    StartCoroutine(BackToMain());
                }
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

    public IEnumerator BackToMain()
    {
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(sceneToLoad);
    }
}
