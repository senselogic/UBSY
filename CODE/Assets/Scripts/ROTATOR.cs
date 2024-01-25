// -- IMPORTS

using UnityEngine;

// -- TYPES

public class ROTATOR : MonoBehaviour
{
    // -- ATTRIBUTES

    public float
        RotationSpeed = 10.0f;

    // -- OPERATIONS

    public void Update(
        )
    {
        transform.Rotate( Vector3.up, RotationSpeed * Time.deltaTime );
    }
}
