using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtention : CinemachineExtension
{
    [SerializeField] private float clampAngle;
    public float aimSensitivity;
    private Vector3 startingRotation;
    public Vector3 StartingRotation { get { return startingRotation; } }

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
                float mouseX = InputManager.Instance.GetLookPosition().normalized.x * aimSensitivity * Time.deltaTime;
                float mouseY = -InputManager.Instance.GetLookPosition().normalized.y * aimSensitivity * Time.deltaTime;

                startingRotation.y += mouseX;
                startingRotation.x += mouseY;
                startingRotation.x = Mathf.Clamp(startingRotation.x, -clampAngle, clampAngle);
                state.OrientationCorrection = Quaternion.Euler(startingRotation.x, startingRotation.y, 0);
            }
        }
    }

}
