using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class moduleWatchdog : MonoBehaviour
{
    // Database holding all the information we could ever need
    private Database database;
    // Image of Gameobject, holding the image, indicating the marker tracker status
    private Image markerIndicator;
    private Text markerIndicatorText;
    private float phiMax;
    // Lists holding the transforms of the markers and modules
    private List<Transform> markerList;
    private List<Transform> moduleList;
    // The amount of seconds that is allowed to pass between losing a module marker and removing it from the module database (which does all sort background/network work with it).
    private float databaseStayTime = 20;
    public float DatabaseStayTime { set { databaseStayTime = value; } }
    // List holding the amount of seconds that each module marker has already been lost. Items are 0 if the module has not been lost
    private List<float> moduleLostTimeList;

    // Use this for initialization
    void Awake ()
    {
        // Get database
        database = GameObject.Find("Database").GetComponent<Database>();
        // get marker tracker indicator
        markerIndicator = GameObject.Find("MarkerIndicator").GetComponent<Image>();
        markerIndicatorText = GameObject.Find("MarkerIndicatorText").GetComponent<Text>();
        phiMax = (databaseStayTime / 2)*(360+90);
        // Init lists
        markerList = new List<Transform>();
        moduleList = new List<Transform>();
        moduleLostTimeList = new List<float>();

        // Find all marker and module transforms:
        // "Drivers" only has markers as direct children. Thus this works!
        foreach (Transform watchDogChild in gameObject.transform)
        {
            // Add to marker list.
            markerList.Add(watchDogChild);
            // Add new entry to the moduleLostTime list to be able to track this modules lost time
            moduleLostTimeList.Add(0);
            Debug.Log("Watchdog: marker: " + watchDogChild.name + " found.");
            // Markers only have one child: the module that the marker is attached to. Save the transform of the module too.
            foreach (Transform markerChild in watchDogChild)
            {
                moduleList.Add(markerChild);
                Debug.Log("Watchdog: marker: " + watchDogChild.name + ": module " + markerChild.name + " found.");
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        int lastLost = 0;
        // Go through all modules that can be tracked
        for(int i=0; i<moduleList.Count;i++)
        {
            // Get modules transform
            Transform moduleTransform = moduleList[i];
            
            // Kudan Camera sets the gameObject.activeInHierarchy attribute according to whether the marker has been detected or not.
            // If the module is currently active i.e. it is detected
            if (moduleTransform.gameObject.activeInHierarchy)
            {
                // Reset time the module has been lost
                moduleLostTimeList[i] = 0;
                // Check if the module is in the module database
                if (database.ModuleDatabase.ContainsKey(moduleTransform.name) == false)
                {
                    // If that is NOT the case: add the modules transform to the database
					if (!database.ModuleDatabase.ContainsKey (moduleTransform.name)) {
						//Debug.Log (moduleTransform.name);
						database.AddModuleToDatabase (moduleTransform.name);
					}
                }
                // If it's in the database: everything is fine
            }
            // If the module is currently inactive i.e. it is currently not detected
            else
            {
                // To do that it must be lost and be lost for longer than the stay time 
                if (moduleLostTimeList[i] != 0 && (Time.time - moduleLostTimeList[i]) > databaseStayTime && database.ModuleDatabase.ContainsKey(moduleTransform.name))
                {
                    // If we those conditions are met: remove the module from the module database
                    database.DeleteModuleInDatabasse(moduleTransform.name);
                    //Debug.Log("Watchdog: module " + moduleTransform.name + " removed from database.");
                }
                // If that is not the case: check if the module is not yet lost
                else if (moduleLostTimeList[i] == 0)
                {
                    // Debug.Log("Watchdog: " + moduleTransform.name + " tracking lost.");
                    // If that is the case: set current time as the time at which the module was lost
                    moduleLostTimeList[i] = Time.time;
                    // Get all children that are present in the 3D model and extract empties. These are the relevant process objects that have buttons that need to be deactivated.
                    // Empties are located in the second hierarchy level under the module game object!
                    Transform[] moduleChildren = moduleTransform.gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform moduleProcessObject in moduleChildren)
                    {
                        // Look for all direct children of the 3D model (should be empties).
                        // If we have found something that is NOT the direct child of the 3D model: continue
                        if (moduleProcessObject.parent.transform != moduleTransform) continue;
                        // Exclude the ground
                        if (moduleProcessObject.name == "ground") continue;

                        // If we get here: we have a direct child of the 3D model: extract name and deactivate in database
                        if(database.ModuleDatabase.ContainsKey(moduleTransform.name))
                        {
							database.ModuleDatabase[moduleTransform.name][moduleProcessObject.name].CurrentlyVisible = false;
                        }
                        
                    }
                    lastLost = i;
                }
            }
        }
        //fade only for first marker
        if (/*moduleLostTimeList[lastLost] == 0 &&*/ database.ModuleDatabase.Count > 0) fadeImage(0);
        //else if (Time.time - moduleLostTimeList[lastLost] <= databaseStayTime && database.ModuleDatabase.Count > 0) fadeImage((Time.time - moduleLostTimeList[0]) / databaseStayTime);
        else if (database.ModuleDatabase.Count == 0) fadeImage(1);
    }

    private void fadeImage(float relTime) {
        Color curColor = markerIndicator.color;
        float radian = Mathf.Sin(((phiMax * relTime) - 90F)*Mathf.PI / 180) + 1F;
        curColor.a = radian/2;
        markerIndicator.color = curColor;
        // Update MarkerIndicatorText
        if(relTime == 1) markerIndicatorText.text = "	No marker detected!";
        else markerIndicatorText.text = "";
    }
    
}
