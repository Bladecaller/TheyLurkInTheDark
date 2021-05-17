using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void Click()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
