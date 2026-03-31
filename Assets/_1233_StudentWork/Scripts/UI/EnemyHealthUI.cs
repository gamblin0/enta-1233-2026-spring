using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthFillImage;
    [SerializeField] private Vector3 _offset = new Vector3 (0, 2f, 0);

    private Camera _camera;

    
    private void Awake()
    {
        _camera = Camera.main;

        if (_health == null )
            _health = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        if (_health == null) return;

        _health.OnHealthChanged += UpdateHealthBar;
        _health.OnDied += Hide;
        _health.OnReset += Show;

        UpdateHealthBar(_health);
    }

    private void OnDisable()
    {
        if (_health == null) return;

        _health.OnHealthChanged -= UpdateHealthBar;
        _health.OnDied -= Hide;
        _health.OnReset -= Show;

        UpdateHealthBar(_health);
    }

    private void UpdateHealthBar(Health health)
    {
        if (_healthFillImage == null) return;
        _healthFillImage.fillAmount = health.NormalizedHealth;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if (_camera == null || _health == null) return;

        transform.position = _health.transform.position + _offset;
        transform.rotation = _camera.transform.rotation;
    }
}
