using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public SpriteRenderer Renderer;
    
    [SerializeField] private AudioSource _source;

    public void Placed()
    {
        _source.Play();
    }
}
