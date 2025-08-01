using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [Header("������")]
    [SerializeField] private Animator animator; // ��������� ���������
    [SerializeField] private Transform characterTransform; // ��������� ���������

    [Header("���������")]
    [SerializeField] private float rotationSpeed = 10f; // �������� ��������

    private Vector3 lastPosition; // ��������� ������� ��� ����������� ��������

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
        // ����������, �������� �� ��������
        bool isMoving = IsMoving();

        // ��������� ��������
        UpdateAnimation(isMoving);

        // ������������ ��������� � ����������� ��������
        if (isMoving)
        {
            RotateCharacter();
        }

        // ��������� ��������� �������
        lastPosition = characterTransform.position;
    }

    // ��������, �������� �� ��������
    private bool IsMoving()
    {
        return Vector3.Distance(characterTransform.position, lastPosition) > 0.01f;
    }

    // ���������� ��������
    private void UpdateAnimation(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", isMoving);
        }
    }

    // ������� ��������� � ����������� ��������
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
