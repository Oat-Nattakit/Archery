using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveoutChild : MonoBehaviour
{
    public void moveChildToworld(Transform child){
        child.SetParent(gameObject.transform);
    }
}
