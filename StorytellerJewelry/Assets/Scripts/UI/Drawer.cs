using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;
using UnityEngine.UI;

public class Drawer : MonoBehaviour
{
    private static Drawer _instance;
    public static Drawer Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public RectTransform Content;

    public float AnimationTime = 2f;

    private bool _isOpen;

    private float _width;
    private float _closedXPosition;
    private float _openXPosition;

    private int? _moveDrawerTweenId;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _width = Content.sizeDelta.x;
        _closedXPosition = 0f - _width;
        _openXPosition = 0f;

        _isOpen = true;
        CloseDrawer(snap: true);
    }

    public void Toggle()
    {
        if (_isOpen)
        {
            CloseDrawer();
        }
        else
        {
            OpenDrawer();
        }
    }

    public void CloseDrawer(bool snap = false)
    {
        if (_isOpen == false)
            return;
        _isOpen = false;
        Vector3 newPos = new Vector2(_closedXPosition, Content.localPosition.y);

        if (snap)
        {
            Content.localPosition = newPos;
        }
        else
        {
            float time = AnimationTime;

            if (_moveDrawerTweenId.HasValue)
            {
                LeanTween.cancel(_moveDrawerTweenId.Value);
                _moveDrawerTweenId = null;
                time = GetAnimationTime(newPos.x);
            }

            _moveDrawerTweenId = LeanTween.moveLocal(Content.gameObject, newPos, time).setEase(LeanTweenType.easeInOutBack).id;
            LeanTween.descr(_moveDrawerTweenId.Value).setOnComplete(() =>
            {
                LeanTween.cancel(_moveDrawerTweenId.Value);
                _moveDrawerTweenId = null;
            });
        }

        gameObject.GetComponent<Image>().enabled = false;
    }

    public void OpenDrawer()
    {
        if (_isOpen)
            return;
        _isOpen = true;
        Vector3 newPos = new Vector2(_openXPosition, Content.localPosition.y);

        float time = AnimationTime;

        if (_moveDrawerTweenId.HasValue)
        {
            LeanTween.cancel(_moveDrawerTweenId.Value);
            _moveDrawerTweenId = null;
            time = GetAnimationTime(newPos.x, isClosing: true);
        }

        _moveDrawerTweenId = LeanTween.moveLocal(Content.gameObject, newPos, time).setEase(LeanTweenType.easeInOutExpo).id;
        LeanTween.descr(_moveDrawerTweenId.Value).setOnComplete(() =>
        {
            LeanTween.cancel(_moveDrawerTweenId.Value);
            _moveDrawerTweenId = null;
        });
        gameObject.GetComponent<Image>().enabled = true;
    }

    private float GetAnimationTime(float newPosX, bool isClosing = false)
    {
        return AnimationTime / 2;
    }
}
