// -- IMPORTS

using UnityEngine;
using UnityEngine.UIElements;

// -- TYPES

public class FrameRateDisplay : MonoBehaviour
{
    // -- ATTRIBUTES

    public UIDocument
        Document;
    public Label
        FrameRateLabel;
    public float
        TimeStep,
        RemainingTime;
    public int
        FrameRate;

    // -- OPERATIONS

    void Start(
        )
    {
        FrameRateLabel = new Label();
        FrameRateLabel.style.fontSize = 14;
        FrameRateLabel.style.color = Color.white;
        FrameRateLabel.style.position = Position.Absolute;
        FrameRateLabel.style.top = 10;
        FrameRateLabel.style.right = 10;

        Document = GetComponent<UIDocument>();
        Document.rootVisualElement.Add( FrameRateLabel );
    }

    // ~~

    void Update(
        )
    {
        TimeStep += ( Time.unscaledDeltaTime - TimeStep ) * 0.1f;

        RemainingTime -= Time.deltaTime;

        if ( RemainingTime <= 0.0f )
        {
            RemainingTime = 1.0f;

            FrameRate = Mathf.FloorToInt( 1.0f / TimeStep );
            FrameRateLabel.text = $"{FrameRate} FPS";
        }
    }
}
