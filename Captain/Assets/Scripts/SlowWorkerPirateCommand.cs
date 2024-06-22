using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

namespace Captain.Command
{
    public class SlowWorkerPirateCommand : ScriptableObject, IPirateCommand
    {
        private float totalWorkDuration;
        private float totalWorkDone;
        private float currentWork;
        private const float PRODUCTION_TIME = 8.0f;
        private bool exhausted = false;

        public SlowWorkerPirateCommand()
        {
            
        }

        private void OnEnable()
        {
            totalWorkDuration = Random.Range(20.0f, 40.0f);
        }

        public bool Execute(GameObject pirate, Object productPrefab)
        {
            //This function returns false when no work is done. 
            //After you implement work according to the specification and
            //generate instances of productPrefab, this function should return true.
            
            // Check if the pirate is exhausted
            if (totalWorkDone >= totalWorkDuration) {
                exhausted = true;
                return false; // Stop working as the pirate is now exhausted
            }

            // Accumulate the amount of work done
            currentWork += Time.deltaTime;
            totalWorkDone += Time.deltaTime;

            // Check if it's time to produce an item
            if (currentWork >= PRODUCTION_TIME) {
                exhausted = false; // Reset the exhaustion status

                // Instantiate the product prefab at the calculated position
                Object.Instantiate(productPrefab);

                // Reset the current work timer for the next item
                currentWork = 0.0f;

                return true;
            }

            return true; // Continue working as the pirate is still active

        }
    }
}
