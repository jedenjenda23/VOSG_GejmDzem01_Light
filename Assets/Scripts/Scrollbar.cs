using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrollbar : MonoBehaviour
{
    public float scrollMultiplier = 10;
    Vector2 initPos;
    RectTransform myTrasform;

    private void Start()
    {
        myTrasform = GetComponent<RectTransform>();
        initPos = myTrasform.localPosition;
    }

    public void ScrollVertical(float scrollAmount)
    {
        Vector2 newPos = initPos;
        initPos.y = initPos.y + scrollAmount * scrollMultiplier;
        myTrasform.localPosition = newPos;
    }
}
