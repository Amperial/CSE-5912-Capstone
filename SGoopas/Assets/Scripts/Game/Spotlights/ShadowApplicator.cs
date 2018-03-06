using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ShadowApplicator
 {
    void OnTriggerEnter(Collider collider);

    void OnTriggerExit(Collider collider);

    void OnTriggerStay(Collider collider);
 }

