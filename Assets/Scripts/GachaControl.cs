using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaControl : MonoBehaviour
{
    [SerializeField]
    private List<Cats> _catsToPull = new List<Cats>();
    private Cats _rolledCat;
    public void Roll()
    {
        if(_catsToPull.Count > 0)
        {
            int r = Random.Range(0, _catsToPull.Count);
            _rolledCat = _catsToPull[r];
            _catsToPull.RemoveAt(r);
            GameManager.instance.AddCat(_rolledCat);
        }
        else
        {
            Debug.Log("Not more cats to add");
        }

    }

    public void LoadCatsToPull(Cats cat)
    {
        _catsToPull.Remove(cat);
    }
}
