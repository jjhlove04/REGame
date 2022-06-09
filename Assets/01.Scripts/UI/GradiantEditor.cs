using UnityEngine;
using UnityEngine.UI;

public class GradiantEditor : MonoBehaviour
{
    public Gradient _gradiant;
    public float _range;
    private Text _text;

    void Awake() 
    {
        _text = GetComponent<Text>();
    }
    void Update()
    {
        _text.color = _gradiant.Evaluate(_range);
    }
}
