﻿// You wanted to explore her inside I see?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class kawaiitlynScript : MonoBehaviour
{

	public KMAudio Audio;
	public KMNeedyModule Module;
	public KMSelectable Module2;

	private bool positioned = false;
	private bool holding = false;
	private int t = 0;

	private static readonly int[][] Takes = { new int[] { 2, 2, 2, 2, 3, 2, 2 }, new int[] { 3, 3, 3, 3, 3, 3, 3, 3 }, new int[] { 3, 3, 3, 3, 3, 3, 3, 3 }, new int[] { 3, 3, 4, 4, 3, 3, 3 } };

	void Awake()
	{
		Module.OnNeedyActivation += OnNeedyActivation;
		Module2.OnInteract += delegate { holding = true; return false; };
		Module2.OnDeselect += delegate { holding = false; };
	}

	// Just used to get rid of normal neediness
	void Update()
	{
		if (!positioned)
		{
			Module.transform.localScale /= 2f;
			positioned = true;
		}
		Module.SetNeedyTimeRemaining(999999f);
	}

	protected void OnNeedyActivation()
	{
		StartCoroutine(NoticeMe());
	}

	private IEnumerator NoticeMe()
	{
		//audio here maybe?
		while (true)
		{
			if (holding)
			{
				if (t > 1)
					t -= 2;
				else if (t > 0)
					t--;
			}
			else
				t++;
			if (t % 100 == 0 && t != 0 && !holding && t < 300)
				PlayRandom(0);
			else if (t % 50 == 0 && !holding && t >= 300 && t < 450)
				PlayRandom(1);
			else if (t % 30 == 0 && !holding && t >= 450 && t < 600)
				PlayRandom(2);
			else if (t % 20 == 0 && !holding && t >= 600)
				PlayRandom(3);
			yield return new WaitForSeconds(0.1f);
		}
	}

	private void PlayRandom(int state)
    {
		int pick = Rnd.Range(0, Takes[state].Length);
		Audio.PlaySoundAtTransform(new string[] { "Neutral","Concern","Anxious","Desperate" }[state] + " line " + (pick + 1) + " take " + (Rnd.Range(0, Takes[state][pick]) + 1), Module.transform);
    }

#pragma warning disable 414
	private string TwitchHelpMessage = "Just send me any command to be with me. You must stay with me until I'm happy again, though.";
#pragma warning restore 414
	IEnumerator ProcessTwitchCommand(string command)
	{
		while (t > 0)
		{
			holding = true;
			yield return null;
		}
		holding = false;
	}
	IEnumerator TwitchHandleForcedSolve()
	{
		while (true)
		{
			holding = true;
			yield return true;
		}
	}
}