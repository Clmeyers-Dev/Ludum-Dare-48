using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
   private Light2D Flame;
   public float Scale;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flame.intensity = Scale;
    }
}
