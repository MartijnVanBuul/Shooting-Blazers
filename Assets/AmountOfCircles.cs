using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AmountOfCircles : MonoBehaviour
{
    private List<CaptureType> _captureTypes = new List<CaptureType>();
    public Dictionary<CaptureType, Texture2D> SpriteDictionary;
    public GameObject CircleGUIHolder;
    [Range(0, 50)] public int MovementSpeed = 20;
    private List<GameObject> _circleImages = new List<GameObject>();

    [Range(0, 100)] public int OffSet = 30;
    public int ImageSizes = 60;

    private int _currentPosition;


    void Start()
    {
        _beginPosition = transform.position;
    }
    private Vector3 _beginPosition;
    public void SetCircles(List<CaptureType> caputerTypes)
    {
        transform.position = _beginPosition;
        _captureTypes = caputerTypes;
        _currentPosition = 0;
        FillDictonary();
        LoadCirclesOnGUI();
    }

    void LoadCirclesOnGUI()
    {
        int count = 0;
        foreach (CaptureType captureType in _captureTypes)
        {
            GameObject parent = Instantiate(CircleGUIHolder);
            GameObject graphics = parent.transform.GetChild(0).gameObject;
            _circleImages.Add(parent);

            parent.name = captureType.ToString();
            parent.transform.SetParent(this.gameObject.transform);

            RectTransform rectParent = parent.GetComponent<RectTransform>();
            rectParent.localPosition = new Vector2(OffSet * count, 0);
            rectParent.localScale = new Vector3(1, 1, 1);


            RawImage rawImage = graphics.GetComponent<RawImage>();
            rawImage.texture = SpriteDictionary[captureType];
            RectTransform rectGraphics = graphics.GetComponent<RectTransform>();
            rectGraphics.sizeDelta = new Vector2(ImageSizes, ImageSizes);

            count++;
        }
    }

    void FillDictonary()
    {
        SpriteDictionary = new Dictionary<CaptureType, Texture2D>();
        Texture2D[] sprites = Resources.LoadAll<Texture2D>("Sprites/UI/Icons");

        foreach (Texture2D sprite in sprites)
        {
            foreach (CaptureType captureType in Enum.GetValues(typeof(CaptureType)))
            {
                if (sprite.ToString().ToLower().Contains(captureType.ToString().ToLower()))
                {
                    SpriteDictionary.Add(captureType, sprite);
                }
            }
        }
        Debug.Log("Added " + SpriteDictionary.Count + " items to the sprite dictonary");
    }
	


    public void NextCaptureType()
    {
        for (int i = 0; i < _circleImages.Count; i++)
        {
            GameObject circle = _circleImages[i];

            if (i <= _currentPosition)
            {
                circle.SetActive(false);
            }
            else
            {
                circle.SetActive(true);
            }
           
        }
        MoveCircles();
        _currentPosition++;
    }

    private Vector3 _startPosition;
    private bool _isMoving = false;
    public void MoveCircles()
    {
        _isMoving = true;
        _startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.position += Vector3.left*Time.fixedDeltaTime * MovementSpeed;

            if (Vector3.Distance(_startPosition, transform.localPosition) > 70)
                _isMoving = false;
        }
    }
}
