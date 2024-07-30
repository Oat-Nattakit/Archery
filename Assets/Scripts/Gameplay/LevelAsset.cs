using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAsset : MonoBehaviour
{
    public GameObject[] LevelPrefabs;
    private GameObject MapObject = null;
    public GameObject mockPre;    

    public void InitLevel(string level)
    {
        
        if (SceneManager.GetActiveScene().name == "LevelTest")
        {
            MapObject = GameObject.Instantiate(mockPre);
        }
        else
        {
            GameObject prefabs = null;
            foreach(var obj in LevelPrefabs){
                if(obj.name == level){
                    prefabs = obj;
                    break;
                }
            }
            MapObject = GameObject.Instantiate(prefabs);

        }
        MapObject.transform.position = Vector3.zero;
    }
}
