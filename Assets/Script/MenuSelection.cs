using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    public GameObject[] menuItems;
    public int initialSelectionIndex = 0;
    public float joystickThreshold = 0.5f;

    private int currentSelectionIndex;

    private void Start()
    {
        currentSelectionIndex = initialSelectionIndex;
        UpdateSelection();
    }

    private void Update()
    {
        // Joystick input
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(verticalInput) > joystickThreshold)
        {
            if (verticalInput > 0)
            {
                currentSelectionIndex--;
                if (currentSelectionIndex < 0)
                    currentSelectionIndex = menuItems.Length - 1;
            }
            else
            {
                currentSelectionIndex++;
                if (currentSelectionIndex >= menuItems.Length)
                    currentSelectionIndex = 0;

            }

            UpdateSelection();
        }

        // Keyboard input
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentSelectionIndex--;
            if (currentSelectionIndex < 0)
                currentSelectionIndex = menuItems.Length - 1;

            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentSelectionIndex++;
            if (currentSelectionIndex >= menuItems.Length)
                currentSelectionIndex = 0;

            UpdateSelection();
        }
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (i == currentSelectionIndex)
                menuItems[i].SetActive(true);
            else
                menuItems[i].SetActive(false);
        }
    }
}
