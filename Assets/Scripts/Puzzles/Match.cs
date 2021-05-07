using UnityEngine.Events;

public class Match : Interactable
{
    public UnityAction OnSolve;

    private void Start()
    {
        OnInteract += StartPuzzle;
    }

    private void OnDestroy()
    {
        OnInteract -= StartPuzzle;
    }

    private void StartPuzzle()
    {
        
    }
}
