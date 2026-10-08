using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    // 인터페이스에서 상속받은 상호작용 메서드 구현
    public void Interact()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Debug.Log("문이 열렸습니다.");
            // 여기에 문을 회전시키거나 애니메이션을 재생하는 코드를 넣을 수 있습니다.
            transform.Rotate(0, 90, 0);
        }
        else
        {
            Debug.Log("문이 닫혔습니다.");
            transform.Rotate(0, -90, 0);
        }
    }
}