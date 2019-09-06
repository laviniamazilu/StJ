using UnityEngine;
using System.Collections;

[System.Serializable]
public class Product
{
    public int id;
    public string name;
    public string description;
    public string picture_path;
    public int id_category;
    public int id_subcategory;

    public int id_material;
    public string material_name;
    public float material_price;
    public float material_handicraftprice;

    public int id_model;
    public string model_name;
    public float model_grams;
    public float model_price;

    public int id_measure;
    public float measure_height;
    public float measure_width;
    public float measure_thickness;
    public float measure_diameter;

    public float product_price;
}
