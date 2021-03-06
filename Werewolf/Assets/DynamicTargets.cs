﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class DynamicTargets : MonoBehaviour
{

    private bool mChipsObjectCreated = false;


    // Update is called once per frame
    void Start()
    {

        if (DataSet.Exists("WerewolfAR"))
        {
            Debug.Log("dataset werewlf exists ");
        }
        else Debug.Log("dataset werewlf dont exist ");

        IEnumerable<TrackableBehaviour> trackableBehaviours = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
        Debug.Log("Trackable n: " + System.Linq.Enumerable.Count(trackableBehaviours));

        int i = 0;
        // Loop over all TrackableBehaviours.
        foreach (TrackableBehaviour trackableBehaviour in trackableBehaviours)
        {
            string name = trackableBehaviour.TrackableName;
            Debug.Log("Trackable name: " + name);

           
            if (!name.Equals("wolf") || !name.Equals("qrCode"))
            {

                trackableBehaviour.gameObject.name = "Player " + (i + 1);

                // chips target detected for the first time
                // augmentation object has not yet been created for this target
                // let's create it
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // attach cube under target
                cube.transform.parent = trackableBehaviour.transform;

                // Add a Trackable event handler to the Trackable.
                // This Behaviour handles Trackable lost/found callbacks.
                trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();

                // set local transformation (i.e. relative to the parent target)
                cube.transform.localPosition = new Vector3(0, 0.2f, 0);
                cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                cube.transform.localRotation = Quaternion.identity;
                cube.gameObject.SetActive(true);

               // mChipsObjectCreated = true;
            }
            i++;
            
        }
    }
}