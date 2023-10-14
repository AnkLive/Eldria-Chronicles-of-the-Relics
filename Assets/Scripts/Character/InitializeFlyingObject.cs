using UnityEngine;

public class InitializeFlyingObject : MonoBehaviour
{
    private GameObject _instance;
    private Rigidbody2D _rb;
    private GameObject _instanceObj;
    private Vector2 _currentDirection;
    
    [field:SerializeField] public GameObject Prefab { get; set; }
    [field:SerializeField] public float Speed       { get; set; }

    private void FixedUpdate()
    {
        if (_rb != null)
            _rb.MovePosition(Vector2.MoveTowards(_rb.position, _currentDirection, Speed * Time.deltaTime));
    }

    public void CreateNewPrefab(Vector2 direction)
    {
        _instanceObj = Instantiate(Prefab, transform.position, transform.rotation);
        _rb = _instanceObj.GetComponent<Rigidbody2D>();
        _instanceObj.transform.parent = transform.parent;
        _currentDirection = direction;
    }
}
