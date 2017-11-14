using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
	[SerializeField]
	float _moveSpeed = 30f;

	[SerializeField]
	Material _sectorMaterial;
	[SerializeField]
	PhysicsMaterial2D _sectorPhysicsMaterial;
	Rigidbody2D _body;

	private void Awake()
	{
		_body = GetComponent<Rigidbody2D>();

		for (int j = 0; j < 1; j++)
		{
			for (int i = 0; i < 8; i++)
			{
				var prefab = Resources.Load("Prefabs/Blocker");
				var obj = (GameObject)Instantiate(prefab);

				obj.transform.position = transform.position;

				var blocker = obj.GetComponent<Blocker>();
				blocker.SetSector(Vector2.zero, i / 8f * 360f, i / 8f * 360f + 40f, (j+1)*0.5f+0.1f,(j+1)*0.5f+0.4f, 8);
				blocker.SetMaterial(_sectorMaterial);
				blocker.SetPhysicsMaterial(_sectorPhysicsMaterial);

				obj.transform.parent = transform;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		bool inpLeft = Input.GetKey(KeyCode.LeftArrow);
		bool inpRight = Input.GetKey(KeyCode.RightArrow);

		bool inpA = Input.GetKey(KeyCode.A);
		bool inpD = Input.GetKey(KeyCode.D);

		float mover = 0f;

		if (inpLeft)
			mover -= _moveSpeed;
		if (inpRight)
			mover += _moveSpeed;

		Vector3 vel = new Vector3(mover, 0f, 0f);
		_body.velocity = vel;

		if (inpA)
			transform.SetEulerAnglesZ(transform.eulerAngles.z + 5f);
	}
}
