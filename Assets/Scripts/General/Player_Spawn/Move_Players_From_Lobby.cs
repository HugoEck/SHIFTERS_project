using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Players_From_Lobby : MonoBehaviour
{
    private static Move_Players_From_Lobby _instance;
    private static CinemachineTargetGroup[] targetGroups;
    private static CinemachineVirtualCamera[] cameras;

    private void Start()
    {
        _instance = this;

        targetGroups = FindObjectsOfType<CinemachineTargetGroup>();
        cameras = FindObjectsOfType<CinemachineVirtualCamera>();

        DontDestroyOnLoad(gameObject);

        // Check if there are more than one active target groups
        if (targetGroups.Length > 1)
        {
            // Iterate through the target groups
            for (int i = 0; i < targetGroups.Length; i++)
            {
                // Check if the target group is active and doesn't contain any targets
                if (targetGroups[i].isActiveAndEnabled && targetGroups[i].m_Targets.Length == 0)
                {
                    // Remove the target group from the scene
                    Destroy(targetGroups[i].gameObject);
                }
            }
        }

        if (cameras.Length > 1)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                // Check if the virtual camera is active and doesn't have a valid target or look-at object assigned
                if (cameras[i].AbstractFollowTargetGroup == null && cameras[i].AbstractLookAtTargetGroup == null)
                {                    
                    // Remove the virtual camera from the scene
                    Destroy(cameras[i].gameObject);
                }
            }
        }
    }

}



