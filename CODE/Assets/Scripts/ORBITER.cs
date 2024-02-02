// -- IMPORTS

using UnityEngine;

// -- TYPES

public class ORBITER : MonoBehaviour
{
    // -- ATTRIBUTES

    public Transform
        TargetTransform;
    public Vector3
        TargetOffsetVector = new Vector3( 0.0f, 1.0f, 0.0f );
    public float
        Distance = 6.0f,
        MinimumDistance = 6.0f,
        MaximumDistance = 10.0f,
        TranslationSpeed = 10.0f,
        XRotationSpeed = 25.0f,
        YRotationSpeed = 25.0f,
        XRotationAngle = 0.0f,
        YRotationAngle = 0.0f,
        MinimumYRotationAngle = 1.0f,
        MaximumYRotationAngle = 40.0f;
    public Vector3
        TranslationMousePositionVector,
        RotationMousePositionVector;
    public bool
        IsTranslating,
        IsRotating;

    // -- OPERATIONS

    void UpdateCamera(
        )
    {
        Quaternion
            rotation_quaternion;
        Vector3
            position_vector;

        XRotationAngle = XRotationAngle % 360.0f;
        YRotationAngle = Mathf.Clamp( YRotationAngle, MinimumYRotationAngle, MaximumYRotationAngle );

        rotation_quaternion = Quaternion.Euler( YRotationAngle, XRotationAngle, 0.0f );
        position_vector
            = TargetTransform.position
              + TargetOffsetVector
              + rotation_quaternion * new Vector3( 0.0f, 0.0f, -Distance );

        transform.rotation = rotation_quaternion;
        transform.position = position_vector;
    }

    // ~~

    void Start(
        )
    {
        UpdateCamera();
    }

    // ~~

    void LateUpdate(
        )
    {
        Vector3
            mouse_position_offset_vector;

        if ( TargetTransform != null )
        {
            if ( Input.GetMouseButton( 1 ) )
            {
                if ( !IsTranslating )
                {
                    IsTranslating = true;
                    TranslationMousePositionVector = Input.mousePosition;
                }

                mouse_position_offset_vector = ( Input.mousePosition - TranslationMousePositionVector ) / Screen.width;
                TranslationMousePositionVector = Input.mousePosition;

                Distance -= mouse_position_offset_vector.y * TranslationSpeed;
                Distance = Mathf.Clamp( Distance, MinimumDistance, MaximumDistance );
            }
            else
            {
                IsTranslating = false;
            }

            if ( Input.GetMouseButton( 0 ) )
            {
                if ( !IsRotating )
                {
                    IsRotating = true;
                    RotationMousePositionVector = Input.mousePosition;
                }

                mouse_position_offset_vector = ( Input.mousePosition - RotationMousePositionVector ) * ( 1000.0f / Screen.width );
                RotationMousePositionVector = Input.mousePosition;

                XRotationAngle += mouse_position_offset_vector.x * XRotationSpeed * Time.deltaTime;
                YRotationAngle -= mouse_position_offset_vector.y * YRotationSpeed * Time.deltaTime;
            }
            else
            {
                IsRotating = false;
            }

            UpdateCamera();
        }
    }
}
