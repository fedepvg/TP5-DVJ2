using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    [SerializeField]
    private Transform ExplosionCamera;
    [SerializeField]
    private Transform PlayerCamera;
    private const float YDistanceFromExplosion = 1000f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static CameraManager Instance
    {
        get { return instance; }
    }

    public void SwitchToExplosionCamera(Vector3 actualPos)
    {
        ExplosionCamera.gameObject.SetActive(true);
        ExplosionCamera.position=actualPos + new Vector3(0, YDistanceFromExplosion, 0);
    }
}
