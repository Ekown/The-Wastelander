using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FactManager : MonoBehaviour {

    private List<string> facts = new List<string>()
    {
        "Recycling is a process to create new items from old and used materials. This helps in reducing energy and potentially useful materials from being wasted.",
        "Philippines is the top 3 contributor of plastic waste in sea with around 0.28-0.75 MMT of plastic waste.",
        "Recycling is a part of waste disposal hierarchy – Reduce, Reuse, Recycle.",
        "Most of the illegally dumped waste originates in squatters’ areas, which house 30-40% of the population in the Philippines",
        "Solid waste refers to the range of garbage arising from animal and human activities that are discarded as unwanted and useless.",
        "Solid waste is generated from industrial, residential and commercial activities in a given area, and may be handled in a variety"
    };

    public TextMeshProUGUI factsText;

	// Use this for initialization
	void Start () {
        factsText.text = facts[Random.Range(0,5)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
