
using UnityEngine;

public class Persone
{
    public Sprite icon {  get; private set; }
    public Color color { get; private set; }

    public Persone(Sprite icon, Color color)
    {
        this.icon = icon;
        this.color = color;
    }
}
