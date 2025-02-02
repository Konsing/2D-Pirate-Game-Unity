﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

public class PirateController : MonoBehaviour
{
    [SerializeField]
    public IPirateCommand activeCommand;
    
    [SerializeField]
    public GameObject productPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.activeCommand = ScriptableObject.CreateInstance<FastWorkerPirateCommand>();
    }

    // Update is called once per frame
    void Update()
    {
        var working = this.activeCommand.Execute(this.gameObject, this.productPrefab);

        this.gameObject.GetComponent<Animator>().SetBool("Exhausted", !working);
    }
    
    // Has received motivation. A likely source is from one of the Captain's morale inducements.
    public void Motivate()
    {
        int commandType = Random.Range(0, 3);

        switch (commandType)
        {
            case 0:
                this.activeCommand = Object.Instantiate(ScriptableObject.CreateInstance<SlowWorkerPirateCommand>());
                break;
            case 1:
                this.activeCommand = Object.Instantiate(ScriptableObject.CreateInstance<NormalWorkerPirateCommand>());
                break;
            case 2:
                this.activeCommand = Object.Instantiate(ScriptableObject.CreateInstance<FastWorkerPirateCommand>());
                break;
        }
    }
}
