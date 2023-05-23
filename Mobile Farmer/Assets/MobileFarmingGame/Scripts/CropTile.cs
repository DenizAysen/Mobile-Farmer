using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    public enum State { Empty, Sown, Watered}
    private State state;
    void Start()
    {
        state = State.Empty;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public bool IsEmpty()
    {
        return state == State.Empty;
    }
    public void Sow()
    {
        state = State.Sown;

        GameObject gmo = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        gmo.transform.position = transform.position;
        gmo.transform.localScale = Vector3.one / 2;
    }
}
