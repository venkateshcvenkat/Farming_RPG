using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentAnimationEvent : MonoBehaviour
{
    public void NotifyAncestors(string message)
    {
        SendMessageUpwards(message);
    }
}
