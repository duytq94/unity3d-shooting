using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
	private int quantityAlive;
	private int quantityDead;
	private Text quantityAliveBoard;
	private Text quantityDeadBoard;

	// Use this for initialization
	void Start ()
	{
		quantityAliveBoard = GameObject.FindGameObjectWithTag ("QuantityAlive").GetComponent<Text> ();
		quantityDeadBoard = GameObject.FindGameObjectWithTag ("QuantityDead").GetComponent<Text> ();
		quantityAliveBoard.text = "0";
		quantityDeadBoard.text = "0";
	}
	
	// Update is called once per frame
	void Update ()
	{
		quantityAlive = GameObject.FindGameObjectsWithTag ("Skeleton").Length;
		quantityAliveBoard.text = quantityAlive.ToString ();
	}

	public void SkeletonDead ()
	{
		quantityDead++;
		quantityDeadBoard.text = quantityDead.ToString ();
	}
}
