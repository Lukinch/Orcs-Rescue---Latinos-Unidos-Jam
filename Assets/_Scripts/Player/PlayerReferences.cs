using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] GameObject _visuals;
    [SerializeField] GameObject _camerasParent;
    [SerializeField] Rigidbody _playerRigidbody;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInputController _playerInputController;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Cinemachine.CinemachineFreeLook _freeLookCam;

    public GameObject Visuals { get => _visuals; }
    public GameObject CamerasParent { get => _camerasParent; }
    public Rigidbody PlayerRigidbody { get => _playerRigidbody; }
    public PlayerInput PlayerInput { get => _playerInput; }
    public PlayerController PlayerController { get => _playerController; }
    public PlayerInputController PlayerInputController { get => _playerInputController; }
    public Animator PlayerAnimator { get => _playerAnimator; }
    public Cinemachine.CinemachineFreeLook FreeLookCam { get => _freeLookCam; }

    public bool AreVisualsVisible() => _visuals.activeInHierarchy;
    public void EnableVisuals() => _visuals.SetActive(true);
    public void DisableVisuals() => _visuals.SetActive(false);
    public bool AreCamerasVisible() => _camerasParent.activeInHierarchy;
    public void EnableCameras() => _camerasParent.SetActive(true);
    public void DisableCameras() => _camerasParent.SetActive(false);
    public bool IsControllerScriptEnabled() => _playerController.enabled;
    public void EnableControllerScript() => _playerController.enabled = true;
    public void DisableControllerScript() => _playerController.enabled = false;
    public void MakePlayerKinematic() => _playerRigidbody.isKinematic = true;
    public void MakePlayerDynamic() => _playerRigidbody.isKinematic = false;
}
