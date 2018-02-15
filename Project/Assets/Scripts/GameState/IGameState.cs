/*
 * States have actions that need to be implemented:
 *      onEnter is when the machine switched the this state from a different one.
 *      onExit is when the machine switched out of this state to a different one.
 *      onPause is when the pause screen is about to be loaded.
 *      onUnpause is when the pause screen is about to be unloaded.
 */
using UnityEngine;

public interface IGameState{
    void onEnter();
    void onExit();
    void onPause();
    void onUnpause();
}
