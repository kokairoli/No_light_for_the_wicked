using UnityEngine;

public class Chestcontroller : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCloseChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.SetBool("isOpen", true);
        }
        else
        {
            isOpen = false;
            animator.SetBool("isOpen", false);
        }
    }
}
