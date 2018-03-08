using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ShadowApplicator
 {
    void OnTriggerEnter2D(Collider2D collider);

    void OnTriggerExit2D(Collider2D collider);

    void OnTriggerStay2D(Collider2D collider);
 }

