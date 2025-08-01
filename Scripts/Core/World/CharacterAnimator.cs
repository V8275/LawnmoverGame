using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private Animator animator; // Компонент аниматора
    [SerializeField] private Transform characterTransform; // Трансформ персонажа

    [Header("Параметры")]
    [SerializeField] private float rotationSpeed = 10f; // Скорость поворота

    private Vector3 lastPosition; // Последняя позиция для определения движения

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (characterTransform == null)
        {
            characterTransform = transform;
        }

        lastPosition = characterTransform.position;
    }

    private void Update()
    {
        // Определяем, движется ли персонаж
        bool isMoving = IsMoving();

        // Обновляем анимацию
        UpdateAnimation(isMoving);

        // Поворачиваем персонажа в направлении движения
        if (isMoving)
        {
            RotateCharacter();
        }

        // Обновляем последнюю позицию
        lastPosition = characterTransform.position;
    }

    // Проверка, движется ли персонаж
    private bool IsMoving()
    {
        return Vector3.Distance(characterTransform.position, lastPosition) > 0.01f;
    }

    // Обновление анимации
    private void UpdateAnimation(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", isMoving);
        }
    }

    // Поворот персонажа в направлении движения
    private void RotateCharacter()
    {
        Vector3 direction = (characterTransform.position - lastPosition).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
