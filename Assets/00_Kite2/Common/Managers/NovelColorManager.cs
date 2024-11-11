using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelColorManager
{
    private static NovelColorManager instance;

    private Color color;

    private float canvasHeight;
    private float canvasWidth;

    public static NovelColorManager Instance()
    {
        if (instance == null)
        {
            instance = new NovelColorManager();
        }
        return instance;
    }

    public void SetColor (Color color)
    {
        this.color = color;
    }

    public Color GetColor ()
    {
        return color;
    }

public void SetCanvasHeight(float height)
{
    if (height > 0) // Überprüfung, ob die Höhe positiv ist
    {
        this.canvasHeight = height;
    }
}

// Getter für canvasHeight
public float GetCanvasHeight()
{
    return this.canvasHeight;
}

// Setter für canvasWidth
public void SetCanvasWidth(float width)
{
    if (width > 0) // Überprüfung, ob die Breite positiv ist
    {
        this.canvasWidth = width;
    }
}

// Getter für canvasWidth
public float GetCanvasWidth()
{
    return this.canvasWidth;
}
}
