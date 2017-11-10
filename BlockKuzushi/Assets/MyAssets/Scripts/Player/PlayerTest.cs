using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
	[SerializeField]
	GameObject _blockers;

	// Use this for initialization
	void Start ()
	{
		var emitter = GetComponent<BlockerEmitter>();
		
		for(int i=0;i<4;i++)
		{
			emitter.Emit(i / 4f * 360f, 30f, 1f, 0.3f);
		}

		_blockers = transform.Find("Blockers").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float delta = 0f;
		if (MyInput.leftRot)
			delta += 10f;
		if (MyInput.rightRot)
			delta -= 10f;
		_blockers.transform.localEulerAngles += new Vector3(0f, 0f, delta);
	}
}
