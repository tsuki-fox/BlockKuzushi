using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	//現ウェーブの経過時間
	float _currentElapsed;
	//現ウェーブの制限時間
	[SerializeField]
	float _timeLimit;
	//残り時間
	[Extractable]
	public float RemainingTime
	{
		get
		{
			return _timeLimit - _currentElapsed; 
		}
	}

	void Update()
	{
		_currentElapsed += Time.deltaTime;	
	}
}
