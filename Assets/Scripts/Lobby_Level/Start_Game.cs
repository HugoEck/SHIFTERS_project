using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Start_Game : MonoBehaviour
{
    
    private List<GameObject> _activePlayers;

    [SerializeField] private TextMeshProUGUI _startGameText;

    private void Start()
    {        
        StartCoroutine(FindPlayers());
        _startGameText.enabled = false;
    }
    private IEnumerator FindPlayers()
    {
        while (true)
        {            

            // Find all game objects with the tag "Player"
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
            _activePlayers = objectsWithTag.ToList();

            // Filter the game objects based on whether they have the "Shift_Shape" script attached
            foreach (GameObject obj in objectsWithTag)
            {                                                               
                _activePlayers.Add(obj);                
            }

            yield return new WaitForSeconds(1f); // update the list every 1 second
        }
    }
    public void Update()
    {
        if(_activePlayers.Count > 2)
        {
            _startGameText.enabled = true;
            
        }
    }
}
