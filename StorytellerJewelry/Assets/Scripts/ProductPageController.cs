using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductPageController : MonoBehaviour, IRoute
{
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public Image Picture;
    public Text Name;
    public Text Price;
    public Text Description;
    public Text MaterialDescription;
    public Text ModelDescription;
    public Text MeasureDescription;

    [Header("Fotogravura objects")]
    public GameObject FotogravuraPanel;
    public Text NoPicText;
    public GameObject RemovePicButton;
    public GameObject GravuraTextPanel;
    public GameObject LoadedImagePanel;
    public Image LoadedImage;

    [Header("Gravura Text objects")]
    public InputField GravuraTextInputField;
    public Dropdown GravuraTypeDropdown;
    public GameObject TextVersoPanel;
    public InputField TextVersoInput;

    [Header("Nume Text objects")]
    public GameObject NumeTextPanel;
    public InputField NumeInputField;

    private Product _product;
    private ProductOptions _productOptions;

    private Texture2D _currentLoadedPicture;
    private string _fileName;

    private Route _thisRoute = new Route()
    {
        RoutePath = "Products"
    };

    public void Refresh(Route route)
    {
        MakeEverythingActive();

        if (route.RouteKey == 0)
            route.RouteKey = 1;

        RemovePic();

        ProductData.Instance.GetProduct(route.RouteKey, (Product product) =>
        {
            _product = product;

            _product.IsNume = (_product.model_name.ToLower().IndexOf("nume") >= 0);
            ShowOptions();

            _productOptions = new ProductOptions()
            {
                id_product = _product.id,
                id_client = UserDetailsController.Instance.ID_CLIENT,
                id_gravura = 1
            };

            Sprite sprite = Resources.Load("ProductImages/" + product.picture_path, typeof(Sprite)) as Sprite;
            Picture.sprite = sprite;

            Name.text = product.name;
            Price.text = product.product_price + " Lei";

            Description.text = product.description;
            MaterialDescription.text = product.material_name + ", " + product.material_price + " Lei";
            ModelDescription.text = product.model_name + ", " + product.model_grams + "g, " + product.model_price + " Lei";
            MeasureDescription.text = "Diameter: " + product.measure_diameter + ", Thickness: " + product.measure_thickness + ", Height: " + product.measure_height + ", Width: " + product.measure_width;
        });
    }

    public void TakePic()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;

        _currentLoadedPicture = null;
        _fileName = null;

#if UNITY_EDITOR

        System.Windows.Forms.OpenFileDialog openFileDialog;
        openFileDialog = new System.Windows.Forms.OpenFileDialog()
        {
            InitialDirectory = @"D:\",
            Title = "Browse Text Files",

            CheckFileExists = true,
            CheckPathExists = true,

            DefaultExt = "png",
            Filter = "png files (*.png)|*.jpg",
            FilterIndex = 2,
            RestoreDirectory = true,

            ReadOnlyChecked = true,
            ShowReadOnly = true
        };

        if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            Debug.Log(openFileDialog.FileName);
            var fileData = System.IO.File.ReadAllBytes(openFileDialog.FileName);
            _currentLoadedPicture = new Texture2D(2, 2);
            _currentLoadedPicture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            _fileName = openFileDialog.SafeFileName;
        }

        OnPictureLoad(_currentLoadedPicture);

#elif UNITY_ANDROID
        
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((string path) => {
            
            if (path != null)
            {
                // Create Texture from selected image
                _currentLoadedPicture = NativeGallery.LoadImageAtPath(path);
                OnPictureLoad(_currentLoadedPicture);
            }
        }, "", "image/*");
#endif
    }

    private void MakeEverythingActive()
    {
        LoadedImagePanel.SetActive(true);
        LoadedImage.gameObject.SetActive(true);
        NoPicText.gameObject.SetActive(true);
        RemovePicButton.SetActive(true);
        GravuraTextPanel.SetActive(true);
        FotogravuraPanel.SetActive(true);
        GravuraTextPanel.SetActive(true);
        TextVersoPanel.SetActive(true);
        NumeTextPanel.SetActive(true);
        GravuraTypeDropdown.value = 0;
        GravuraTextInputField.text = "";
        NumeInputField.text = "";
        TextVersoInput.text = "";
    }

    public void RemovePic()
    {
        LoadedImagePanel.SetActive(false);
        LoadedImage.gameObject.SetActive(false);
        NoPicText.gameObject.SetActive(true);
        RemovePicButton.SetActive(false);

        GravuraTextPanel.SetActive(true);
        OnGravuraTypeChange();

        _currentLoadedPicture = null;
        _fileName = null;
    }

    private void ShowOptions()
    {
        if (_product.IsNume)
        {
            FotogravuraPanel.SetActive(false);
            GravuraTextPanel.SetActive(false);
            TextVersoPanel.SetActive(false);

            // numele e numai pe fata
            if (_productOptions != null)
            {
                _productOptions.id_gravura = 1;
            }
        }
        else
        {
            NumeTextPanel.SetActive(false);
        }
    }

    private void OnPictureLoad(Texture2D texture)
    {
        if (texture == null)
        {
            Debug.Log("Couldn't load texture");
            return;
        }

        LoadedImagePanel.SetActive(true);
        LoadedImage.gameObject.SetActive(true);
        NoPicText.gameObject.SetActive(false);
        RemovePicButton.SetActive(true);
        GravuraTextPanel.SetActive(false);

        // poza e numai pe fata
        _productOptions.id_gravura = 1;

        Rect rec = new Rect(0, 0, texture.width, texture.height);
        LoadedImage.sprite = Sprite.Create(texture, rec, new Vector2(0, 0), .01f);
    }

    public void OnGravuraTextChange()
    {
        if (string.IsNullOrWhiteSpace(GravuraTextInputField.text) == false)
        {
            FotogravuraPanel.SetActive(false);
        }
        else
        {
            FotogravuraPanel.SetActive(true);
        }
    }

    public void OnGravuraTypeChange()
    {
        if (_product != null && _product.IsNume)
            return;

        if (GravuraTypeDropdown.value > 0)
        {
            TextVersoPanel.SetActive(true);
            if (_productOptions != null)
                _productOptions.id_gravura = 3;
        }
        else
        {
            TextVersoPanel.SetActive(false);
            if (_productOptions != null)
                _productOptions.id_gravura = 1;
        }
    }

    public void AddToCart()
    {
        if (GravuraTextPanel.activeSelf == true)
        {
            _productOptions.text = GravuraTextInputField.text;

            if (_productOptions.id_gravura == 3)
            {
                _productOptions.text_verso = TextVersoInput.text;
            }
        }
        else if (_product.IsNume)
        {
            _productOptions.nume = NumeInputField.text;
        }
        else
        {
            _productOptions.picture_bytes = _fileName != null ? _currentLoadedPicture.EncodeToJPG() : null;
            _productOptions.file = _fileName;
        }

        CartData.Instance.SaveProductToCart(_productOptions, () =>
        {

        });
    }

    public Route GetRoute()
    {
        return _thisRoute;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
