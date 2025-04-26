using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _pickUpCLip, _dropclip;
    
    private bool _dragging, _placed;

    private Vector2 _offset, _originalPosition;

    private PuzzleSlot _slot;

    public void Init(PuzzleSlot slot)
    {
        _renderer.sprite = slot.Renderer.sprite;
        _slot = slot;
    }

    void Awake()
    {
        _originalPosition = transform.position;
    }

    void Update()
    {
        if (_placed) return;
        if (!_dragging) return;
        
        var mousePosition = GetMousePos();
        
        transform.position = mousePosition - _offset;
    }

    void OnMouseDown()
    {
        _dragging = true;
        _source.PlayOneShot(_pickUpCLip);
        
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, _slot.transform.position) < 3)
        {
            transform.position = _slot.transform.position;
            _slot.Placed();
            _placed = true;
        }
        else
        {
            transform.position = _originalPosition;
            _source.PlayOneShot(_dropclip);
            _dragging = false;
        }
        
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
