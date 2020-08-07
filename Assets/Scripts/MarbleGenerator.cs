using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MarbleGenerator : MonoBehaviour
{
    public GameObject MarblePrefab;
    int[] ratios = new int[3] {1, 1, 1};
    string[] MarbleTags;

    public void GenerateMarble()
    {
        GameObject Marble = Instantiate(MarblePrefab);
        int dice = Random.Range(0, this.ratios.Sum());
        Marble.tag = MarbleTags[dice];
    }

    public void SetRatios(int[] ratios)
    {
        this.ratios = ratios;
        this.SetMarbleTags();
    }

    void Start()
    {
        this.SetMarbleTags();
        this.GenerateMarble();
    }
    private void SetMarbleTags()
    {
        this.MarbleTags = new string[this.ratios.Sum()];
        int j = 0;
        for(int i = 0; i < this.ratios[0]; i++){
            this.MarbleTags[j] = "RedMarble";
            j++;
        }
        for (int i = 0; i < this.ratios[1]; i++)
        {
            this.MarbleTags[j] = "BlueMarble";
            j++;
        }
        for (int i = 0; i < this.ratios[2]; i++)
        {
            this.MarbleTags[j] = "BlackMarble";
            j++;
        }
    }
    
}
