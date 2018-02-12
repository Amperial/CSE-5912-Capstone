using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IController
{
    void moveUp();
    void moveDown();
    void moveLeft();
    void moveRight();

    void jump();
    void grab();
    void release();
}
