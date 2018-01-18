/*
 * States have two actions that need to be implemented:
 *      onEnter is when the machine switched the this state from a different one.
 *      onExit is when the machine switched out of this state to a different one.
 */
public interface IGameState{
    void onEnter();
    void onExit();
}
