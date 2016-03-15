using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This is the World Crosshair control script. This should be attached to the object you wish to use as your crosshair.
/// By default this is a quad + texture with the special crosshair shader applied. See the WorldCrosshair prefab for 
/// an example of how to set up a custom crosshair object.
/// </summary>
public class WorldCrosshair : MonoBehaviour
{
    /// <summary>
    /// If enabled, this WorldCrosshair can be used as a cursor, selecting and interacting with Unity's standard UI elements.
    /// This takes more computation, so it should be disabled unless you need it.
    /// </summary>
    /// TODO: This is not implemented yet, but it will be enabled in future updates.
    //[SerializeField]
    //private bool enableUIselection = false;

    /// <summary>
    /// Where should the start of the raycast be for the crosshair? 
    /// This is usually the head of the player, their gun's barrel, their hands, etc.
    /// It should be the same transform as your CrosshairProjector if you use one.
    /// </summary>
    public Transform raycastOrigin;

    /// <summary>
    /// How far out should the crosshair look for objects? Larger numbers are further but more computationally intensive. 
    /// If nothing is sensed at this distance, the crosshair will default to a position at this distance.
    /// If this is set to <= 0, the far clipping plane of the main camera (tag: "MainCamera") will be used.
    /// </summary>
    public float maxSenseDistance = 0f;

    /// <summary>
    /// How often should the crosshair be updated (in seconds)? Lower numbers have less delay but are more performance intensive. 
    /// 0 means as fast as possible. Unless you have a lot of colliders/physics stuff going on or experience performance
    /// issues, it's probably ok to leave this at 0.
    /// </summary>
    public float updateInterval = 0f;

    /// <summary>
    /// Should the FixedUpdate method be used instead of the Update method? 
    /// If your crosshair does anything with physics, this should be set to <c>true</c>.
    /// Otherwise, it should be <c>false</c>.
    /// </summary>
    public bool useFixedUpdate = false;

    /// <summary>
    /// If <c>true</c>, this will enable an adaptive scaling algorithm that makes the crosshair appear to be the same size 
    /// regardless of its distance from the raycastOrigin/projector. Note that this cannot be changed at runtime without totally reloading
    /// the crosshair object - there WILL be issues if you do. Be warned!
    /// </summary>
    [SerializeField]
    private bool enableAdaptiveScaling = false;

    /// <summary>
    /// If enabled, this will effectively increase the size of the crosshair slightly when the object being targeted is 
    /// very close to the camera. This prevents the weird illusion that the crosshair is very small in VR environments. 
    /// You should do testing both with it enabled and disabled in order to confirm your preference.
    /// </summary>
    public bool stereoscopicScaleTweak = false;

    /// <summary>
    /// How far forward should the crosshair be offset from the object that it detects? For most cases, this should be left at 0.
    /// Positive numbers go farther away from the raycastOrigin, negative numbers go toward it.
    /// </summary>
    public float depthOffset = 0f;

    /// <summary>
    /// The object that this crosshair is over right now. 
    /// It will select the object that the raycast detects when it's updated.  
    /// Note that this will only work if <code>enableObjectSelection</code> is enabled. It will be <code>null</code> otherwise.
    /// </summary>
    [HideInInspector]
    public GameObject selectedObject = null;

    /// <summary>
    /// The original scale of the crosshair, stored to keep it consistent when adaptive scaling (see above) is enabled.
    /// </summary>
    private Vector3 origScale;

    private bool ready = false;

    // Use this for initialization
    void Start()
    {
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (maxSenseDistance <= 0 && mainCam != null)
            maxSenseDistance = mainCam.farClipPlane;
        else if(mainCam == null)
            Debug.LogError("No max sense distance set for WorldCrosshair! Make sure to set this in the inspector or tag your" +
                " camera as \"MainCamera\" for auto-setup."); 

        if (raycastOrigin == null)
        {
            CrosshairProjector p = GameObject.FindObjectOfType<CrosshairProjector>();
            raycastOrigin = p == null ? null : p.transform;
            if (raycastOrigin == null)
            {
                Debug.LogError("World Space Crosshair: There is no Raycast Orign specified for your crosshair!");
                return;
            }
        }

        origScale = this.transform.localScale/2.6f;

        ready = true; // if we get here, everything's ready to go!
    }

    /// <summary>
    /// How long it has been since the last update.
    /// </summary>
    private float timeSinceLastUpdate = 0f;

    /// <summary>
    /// Is it time to update the crosshair?
    /// </summary>
    /// <returns><c>true</c> if it's time to update. Otherwise <c>false</c>.</returns>
    private bool CheckIfReadyToUpdate()
    {
        if (useFixedUpdate == true)
            timeSinceLastUpdate += Time.fixedDeltaTime;
        else
            timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            timeSinceLastUpdate = 0;
            return true;
        }

        return false;
    }

    void Update()
    {
        if (useFixedUpdate == true)
            return;

        if (CheckIfReadyToUpdate() == false)
            return;

        CrosshairUpdate();
    }

    void FixedUpdate()
    {
        if (useFixedUpdate == false)
            return;

        if (CheckIfReadyToUpdate() == false)
            return;

        CrosshairUpdate();
    }

    /// <summary>
    /// Called at a specified regular interval to update the crosshair. Can be called manually to immediately update, as well.
    /// Will do nothing if the crosshair isn't set up correctly.
    /// </summary>
    public void CrosshairUpdate()
    {
        // if the pre-conditions aren't met, don't do anything
        if (!ready)
            return;

        RaycastHit? hit = FindObjectRaycast();
        selectedObject = hit.HasValue ? hit.Value.collider.gameObject : null;
        UpdateCrosshairDepth(hit);
    }

    /// <summary>
    /// Updates the z-position of the crosshair based on the current <c>selectedObject</c>. Sets the depth to the maxSenseDistance if nothing is selected.
    /// </summary>
    private void UpdateCrosshairDepth(RaycastHit? hit)
    {
        float distance;
        if (hit.HasValue)
            distance = hit.Value.distance;
        else
            distance = maxSenseDistance + depthOffset;

        // Magic Numbers: there are significantly diminishing difference in scale at distances > 10, so for optimization's sake this check is here
        if (stereoscopicScaleTweak && distance < 10.0f)
            distance *= 1 + 5 * Mathf.Exp(-distance); // see the definition of stereoscopicScaleTweak above for more info on what this does

        // adaptive scaling applied here
        if (enableAdaptiveScaling)
            transform.localScale = origScale * distance;

        // final depth applied here
        transform.localPosition =  Vector3.forward * distance;
    }

    /// <summary>
    /// Cast a ray to determine depth and the object being hit.
    /// </summary>
    private RaycastHit? FindObjectRaycast()
    {
        // find the direction of the raycast from the origin to the crosshair
        Vector3 direction = transform.position - raycastOrigin.position;

        RaycastHit hit;
        bool foundObject = Physics.Raycast(raycastOrigin.position, direction, out hit, maxSenseDistance);
        Debug.DrawRay(raycastOrigin.position, direction, Color.yellow, .1f, true);
        if (foundObject == true)
        {
            //Debug.Log("Object selected: " + hit.collider.gameObject.name);
            return hit;
        }

        return null;
    }
}
