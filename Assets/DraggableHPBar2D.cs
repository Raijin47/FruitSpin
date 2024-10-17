using UnityEngine;

public class DraggableHPBar2D : MonoBehaviour
{
    public Transform fillBar;
    public SpriteRenderer fillSpriteRenderer;
    public SpriteRenderer backgroundSpriteRenderer;
    public float maxHealth = 100f;
    public Color fullHealthColor = Color.green;
    public Color lowHealthColor = Color.red;
    public float lowHealthThreshold = 0.3f;

    private float currentHealth;
    private bool isDragging = false;
    private Camera mainCamera;
    private Vector3 barStartPosition;
    private float barWidth;

    private void Start()
    {
        if (fillBar == null || fillSpriteRenderer == null || backgroundSpriteRenderer == null)
        {
            Debug.LogError("Required components not assigned!");
            enabled = false;
            return;
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            enabled = false;
            return;
        }

        currentHealth = maxHealth;
        barStartPosition = backgroundSpriteRenderer.transform.position;
        barWidth = backgroundSpriteRenderer.bounds.size.x;
        UpdateHealthBar();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (backgroundSpriteRenderer.bounds.Contains(touchPosition))
                    {
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isDragging)
                    {
                        UpdateHealthFromTouch(touchPosition);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (backgroundSpriteRenderer.bounds.Contains(mousePosition))
            {
                isDragging = true;
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            UpdateHealthFromTouch(mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void UpdateHealthFromTouch(Vector2 position)
    {
        float localX = position.x - barStartPosition.x;
        float healthPercentage = Mathf.Clamp01(localX / barWidth);
        SetHealth(healthPercentage * maxHealth);
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0f, maxHealth);
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0f);
        UpdateHealthBar();
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;

        // Обновляем размер полосы здоровья
        Vector3 newScale = fillBar.localScale;
        newScale.x = healthPercentage;
        fillBar.localScale = newScale;

        // Обновляем цвет полосы здоровья
        fillSpriteRenderer.color = Color.Lerp(lowHealthColor, fullHealthColor, Mathf.InverseLerp(0, lowHealthThreshold, healthPercentage));

        // Здесь можно добавить дополнительную логику, например, вызов событий или обновление текста
        Debug.Log($"Current Health: {currentHealth}/{maxHealth}");
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
