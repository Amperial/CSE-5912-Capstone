using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController {

    private IController currentCtrl, threeDCtrl, twoDCtrl;
	public MasterController(GameObject twoDObj, GameObject threeDObj) {
        threeDCtrl = new ThreeDCtrl(threeDObj);
        twoDCtrl = new TwoDCtrl(twoDObj);
        currentCtrl = threeDCtrl;
	}

    public void grab()
    {
        currentCtrl.grab();
    }

    public void jump()
    {
        currentCtrl.jump();
    }

    public void moveDown()
    {
        currentCtrl.moveDown();
    }

    public void moveLeft()
    {
        currentCtrl.moveLeft();
    }

    public void moveRight()
    {
        currentCtrl.moveRight();
    }

    public void moveUp()
    {
        currentCtrl.moveUp();
    }

    public void release()
    {
        currentCtrl.release();
    }

    public void switchCtrl()
    {
        if (currentCtrl is TwoDCtrl)
            currentCtrl = threeDCtrl;
        else
            currentCtrl = twoDCtrl;
    }
}
