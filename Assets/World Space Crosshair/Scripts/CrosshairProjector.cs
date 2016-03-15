using UnityEngine;
using System.Collections;

/// <summary>
/// This is a simple script to control the rotation of your crosshair projector.
/// You should not use this component if you have your own method of rotating the crosshair projector 
/// (for example, rotating a camera or weapon that you use as a projector with other input scripts).
/// </summary>
public class CrosshairProjector : MonoBehaviour
{
    /// <summary>
    /// The name of the crosshair's X axis in Unity's Input settings. Moving this axis will move the crosshair horizontally.
    /// </summary>
    [SerializeField]
    private string crosshairXaxis = "Mouse X";

    /// <summary>
    /// The name of the crosshair's Y axis in Unity's Input settings. Moving this axis will move the crosshair vertically.
    /// </summary>
    [SerializeField]
    private string crosshairYaxis = "Mouse Y";

    /// <summary>
    /// How quickly the cursor moves around the canvas.
    /// </summary>
    public float sensitivityFactor = 5f;

    // Update is called once per frame
    void Update()
    {
        RotateProjector();
    }

    // Update location of cursor based on mouse location
    private void RotateProjector()
    {
        float inputX = Input.GetAxisRaw(crosshairXaxis);
        float inputY = Input.GetAxisRaw(crosshairYaxis);

        // horizontal         
        this.transform.Rotate(Vector3.up, inputX * sensitivityFactor);
        // vertical
        this.transform.Rotate(Vector3.right, -inputY * sensitivityFactor);
        
        // reset the z angle rotation so that rotation artifacts don't get introduced onto the projector or crosshair
        this.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

}
