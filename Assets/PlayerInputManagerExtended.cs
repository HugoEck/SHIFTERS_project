using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerExtended : MonoBehaviour
{

    public GameObject playerPrefab;
    public Transform spawnPoint;
    public CinemachineTargetGroup targetGroup;


    private void Start()
    {
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[4];
    }

    public void SpawnPlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        newPlayer.name = "Player(Clone)";

        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            if (targetGroup.m_Targets[i].target == null)
            {
                targetGroup.m_Targets[i].target = newPlayer.transform;
                targetGroup.m_Targets[i].weight = 1f;
                break;
            }
        }
    }
}
