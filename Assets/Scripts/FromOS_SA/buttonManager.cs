using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// COMBINES CHECKVISIBILITY AND BUTTON UPDATE
// TO BE PLACED IN THE MARKER GAME OBJECT!

public class buttonManager : MonoBehaviour
{
    private Animator displayAreaAnimator; // we need this for entering the component detail view

    // Database holding all the information we could ever need
    private Database database;
    // skript, holding methods which updates the gui and handles button callbacks
    // private guiUpdate guiUpdateSkript;
    // Transform of the 3D model of the module. (Blender model)
    private Transform moduleTransform;
    // List holding the transform of all process objects that are present in the 3D model of the module
    private List<Transform> moduleProcessObjects;
    // List holding all the buttons. More precisely: holding the canvases that are the buttons.
    private List<Canvas> moduleButtons;
    // The camera that we are using
    public new Camera camera;

    // Button prefab
    public GameObject ButtonPrefab;
    // For interfacing with Unity or something? // TODO: check purpose
    public LayerMask objectLayer;


    // Use this for initialization. Only called once per script 
    void Start ()
    {
        displayAreaAnimator = GameObject.Find("DisplayArea").GetComponent<Animator>();
        // Get ref to database
        database = GameObject.Find("Database").GetComponent<Database>();
        // get ref to guiUpdate
        // guiUpdateSkript = GameObject.Find("GUI_Update").GetComponent<guiUpdate>();

        // Get transform of the 3d model of the module.
        // Marker game object only has one child, thus this works!
        foreach (Transform moduleModel in gameObject.transform)
        {
            moduleTransform = moduleModel;
        }

        // Get transform and make button for every process object
        moduleProcessObjects = new List<Transform>();
        moduleButtons = new List<Canvas>();

        // Get all children that are present in the 3D model and extract empties. These are the relevant process objects.
        // Empties are located in the second hierarchy leve under the module game object!
        Transform[] moduleChildren = moduleTransform.gameObject.GetComponentsInChildren<Transform>();
        foreach(Transform moduleProcessObject in moduleChildren)
        {
            // Look all direct children of the 3D modell (should be empties).
            // If we have found something that is NOT the direct child of the 3D model: continue
            if (moduleProcessObject.parent.transform != moduleTransform) continue;
            // Exclude the ground
            if (moduleProcessObject.name == "ground") continue;

            // If we get here: we have a direct child of the 3D modell: save and make button
            // Save transform
            moduleProcessObjects.Add(moduleProcessObject);
            // Make button, give it a name, get the canvas and save
            GameObject newButton = Instantiate(ButtonPrefab, moduleTransform.parent.transform, false);
            newButton.transform.name = moduleProcessObject.name;
            newButton.GetComponent<Canvas>().enabled = false;
            newButton.GetComponentInChildren<Button>().onClick.AddListener(() => { displayAreaAnimator.SetTrigger("EnterDetailView"); });
            moduleButtons.Add(newButton.GetComponent<Canvas>());
        }
        // We now have everything that we need: the module transform, a list of all process objects in the model and a list of all buttons for those process objects.
    }

	
	// Update is called once per frame
	void Update ()
    {
        // Things to do here: check which process objects are visible and set the status/position of the corresponding button and object in the database accordingly.
        // Check for visibility of each process object
        foreach(Transform processObject in moduleProcessObjects)
        {
            // Helper for determining if part of the process object is visible
            bool partOfProcessObjectVisible = false;
            // Local variables for saving the position and rotation of the button (for later use, if necessary)
            Vector3 buttonPosition = Vector3.zero;
            Quaternion buttonRotation = new Quaternion(0f,0f,0f,0f);
            // The maximum volume of a part of the process Object
            float maxElementVolumeToDate = 0;

            // Each process object can be made up of several 3D elements.
            // Check if the center of mass of at least one of these elements can be seen. Determine best position of button as we go:
            // button is to be placed where the ray to largest (in terms of volume) element penetrates the specified pane
            foreach (Transform processObjectElement in processObject)
            {
                // Temp info
                RaycastHit hitInfo;
                // The camera is in the origin. Search towards the center of mass of the element.
                if (Physics.Raycast(Vector3.zero, processObjectElement.position, out hitInfo, Mathf.Infinity, objectLayer))
                {
                    // Transform to viewportPoint. i.e. the position that the ray went through on a specified plane.
                    Vector3 rayPanePenetrationPoint = camera.WorldToViewportPoint(processObjectElement.position);
                    // Have we hit the element that we were looking for AND is that element in the cameras field of view?
                    if(hitInfo.transform.name == processObjectElement.name && rayPanePenetrationPoint.x >= 0f && rayPanePenetrationPoint.x <= 1f && rayPanePenetrationPoint.y >= 0f && rayPanePenetrationPoint.y <= 1f)
                    {
                        // If that is the case: the process object element in visible!
                        partOfProcessObjectVisible = true;

                        // Calculate the volume of the process object element and use it to check if this is the dominating visible element of the process object
                        Vector3 elementSize = processObjectElement.GetComponent<Collider>().bounds.size;
                        float elementVolume = elementSize.x * elementSize.y * elementSize.z;
                        if(elementVolume > maxElementVolumeToDate)
                        {
                            // If so: save the new volume and preferred position of the button
                            maxElementVolumeToDate = elementVolume;
                            buttonPosition = hitInfo.transform.position;
                            buttonRotation = Quaternion.LookRotation(processObjectElement.position.normalized);
                        }
                    }
                }
            }

            // Retrieve button for further operation
            // Find and return a list element e whose name matches the given name (there can only be one in this case)
            Canvas correspondingProcessObjectButton = moduleButtons.Find(e => e.gameObject.transform.name == processObject.name);
            // If the process object is not visible: deactivate the button and set database value
            if (!partOfProcessObjectVisible)
            {
                // Only disable button if it isn't already disabled
                if (correspondingProcessObjectButton.enabled)
                {
                    correspondingProcessObjectButton.enabled = false;
                }
                // Set database value
                //if (database.ModuleDatabase[moduleTransform.name].ContainsKey(processObject.name))
				if (database.ModuleDatabase[moduleTransform.name].ContainsKey(processObject.name))
                {
                    //database.ModuleDatabase[moduleTransform.name][processObject.name].CurrentlyVisible = false;
					database.ModuleDatabase[moduleTransform.name][processObject.name].CurrentlyVisible = false;
                    //if (database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom == null) {
					if (database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom == null) {
                        //database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom = correspondingProcessObjectButton.transform;
						database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom = correspondingProcessObjectButton.transform;
                    }
                }
            }
            // If the process object is visible: activate button (if not already activated), set position + set values in database
            else
            {
                // Set button values
                correspondingProcessObjectButton.transform.position = buttonPosition;
                correspondingProcessObjectButton.transform.rotation = buttonRotation;
                correspondingProcessObjectButton.enabled = true;

                // Set database values
                //if (database.ModuleDatabase[moduleTransform.name].ContainsKey(processObject.name))
				if (database.ModuleDatabase[moduleTransform.name].ContainsKey(processObject.name))
				{
                    //database.ModuleDatabase[moduleTransform.name][processObject.name].CurrentlyVisible = true;
					database.ModuleDatabase[moduleTransform.name][processObject.name].CurrentlyVisible = true;
                    //database.ModuleDatabase[moduleTransform.name][processObject.name].ModellPosition = buttonPosition;
					database.ModuleDatabase[moduleTransform.name][processObject.name].ModellPosition = buttonPosition;
                    //if (database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom == null) {
					if (database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom == null) {	
                        //database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom = correspondingProcessObjectButton.transform;
						database.ModuleDatabase[moduleTransform.name][processObject.name].ButtonTransfrom = correspondingProcessObjectButton.transform;
                    }
                }
            }
        }
	}
}