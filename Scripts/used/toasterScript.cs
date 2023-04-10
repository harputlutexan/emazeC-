using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toasterScript : MonoBehaviour
{
	public void _disable()
	{
		GetComponent<Animator>().enabled = false;

	}
}
