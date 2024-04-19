using Cinemachine;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Audio;

public class CinemachinePOVExtention : CinemachineExtension
{
    [SerializeField] private float clampAngle;

    private float aimSensitivity = 100;
    private Vector3 startingRotation;
    public Vector3 StartingRotation { get { return startingRotation; } }

    private int invertX = 1;
    private int invertY = -1;

    public static CinemachinePOVExtention Instance;

    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        else
            Instance = this;

        startingRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                float mouseX = InputManager.Instance.GetLookPosition().normalized.x * aimSensitivity * Time.deltaTime * invertX;
                float mouseY = InputManager.Instance.GetLookPosition().normalized.y * aimSensitivity * Time.deltaTime * invertY;

                startingRotation.y += mouseX;
                startingRotation.x += mouseY;
                startingRotation.x = Mathf.Clamp(startingRotation.x, -clampAngle, clampAngle);
                state.OrientationCorrection = Quaternion.Euler(startingRotation.x, 0, 0);
            }
        }
    }


    public void InvertX() => invertX = -invertX;
    public void InvertY() => invertY = -invertY;

    public void SliderValue(float _value) => aimSensitivity = _value * 100;
}
