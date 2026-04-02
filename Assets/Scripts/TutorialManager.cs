using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialPanel;

    [Header("Input Actions")]
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    [Header("UI Settings")]
    public float typingSpeed = 0.05f;

    private int currentStep = 0;
    private bool tutorialComplete = false;
    private Coroutine typingCoroutine;

    void Awake()
    {
        if (inputActions != null)
        {
            var playerMap = inputActions.FindActionMap("Player");
            moveAction = playerMap.FindAction("Move");
            jumpAction = playerMap.FindAction("Jump");
            shootAction = playerMap.FindAction("Shoot");
        }
    }

    void OnEnable()
    {
        moveAction?.Enable();
        jumpAction?.Enable();
        shootAction?.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();
        jumpAction?.Disable();
        shootAction?.Disable();
    }

    void Start()
    {
        UpdateTutorialUI();
    }

    void Update()
    {
        if (tutorialComplete) return;

        switch (currentStep)
        {
            case 0: // Movement
                if (moveAction != null && moveAction.ReadValue<Vector2>().magnitude > 0.1f)
                {
                    CompleteStep();
                }
                break;

            case 1: // Jump
                if (jumpAction != null && jumpAction.triggered)
                {
                    CompleteStep();
                }
                break;

            case 2: // Shoot
                if (shootAction != null && shootAction.triggered)
                {
                    CompleteStep();
                }
                break;
        }
    }

    void CompleteStep()
    {
        currentStep++;
        if (currentStep >= 2)
        {
            tutorialComplete = true;
            StartCoroutine(FinishTutorialRoutine());
        }
        else
        {
            UpdateTutorialUI();
        }
    }

    void UpdateTutorialUI()
    {
        if (tutorialText == null) return;

        string message = "";
        switch (currentStep)
        {
            case 0:
                message = "Use WASD or the Joystick to move";
                break;
            case 1:
                message = "Press Space or the south button to jump";
                break;
            case 2:
                message = "Press Left Click or the right button to shoot";
                break;
        }

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(message));
    }

    IEnumerator TypeText(string text)
    {
        tutorialText.text = "";
        foreach (char c in text.ToCharArray())
        {
            tutorialText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null;
    }

    IEnumerator FinishTutorialRoutine()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        yield return StartCoroutine(TypeText("Tutorial completed! Now you can play by your own count!"));
            
        yield return new WaitForSeconds(3f);
        
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }
}
