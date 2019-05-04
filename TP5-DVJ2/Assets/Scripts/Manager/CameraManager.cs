using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    [SerializeField]
    private GameObject ExplosionCamera;
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
        Instantiate<GameObject>(ExplosionCamera, actualPos + new Vector3(0, YDistanceFromExplosion, 0),ExplosionCamera.transform.rotation);
    }
}
