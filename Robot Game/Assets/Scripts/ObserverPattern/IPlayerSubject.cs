/*
 * Gerard Lamoureux
 * ISubject
 * Team Project 1
 * Interface for Subjects of Observers
 */

public interface IPlayerSubject
{
    void RegisterPlayerObserver(IPlayerObserver observer);
    void RemovePlayerObserver(IPlayerObserver observer);
    void NotifyPlayerObservers();
}
