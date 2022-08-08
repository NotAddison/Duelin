using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveSound : MonoBehaviour
{
    // This script basically preserves the sound so that the OST continues playing until it is called to stop
    private static PreserveSound instance = null;
    public static PreserveSound Instance
    {
        get { return instance; }
    }

    void Awake(){
        if (instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject); // Prevents obj from being destroyed when changing scenes
    }
}
