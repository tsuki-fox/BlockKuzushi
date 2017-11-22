using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerEmitter : MonoBehaviour
{
	[SerializeField]
	Material _material;
	[SerializeField]
	PhysicsMaterial2D _phyMaterial;
	[SerializeField]
	int _division = 8;

	[SerializeField]
	GameObject _src;
	GameObject _parent;

	private void Awake()
	{
		//_src = (GameObject)Resources.Load("Prefabs/Blocker");

		//親オブジェクト生成
		_parent = new GameObject("Blockers");
		_parent.transform.SetParent(transform, false);
	}

	public GameObject Emit(float angle, float size, float radius, float thickness)
	{
		//オブジェクト生成
		var go = Instantiate(_src);
		var blocker = go.GetComponent<Blocker>();
		go.AddComponent<CollisionSubscriber>();
		go.transform.position = Vector3.zero;

		//メッシュ+コライダーの生成
		float begin = angle + size / 2f;
		float end = angle - size / 2f;
		float inner = radius - thickness / 2f;
		float outer = radius + thickness / 2f;

		blocker.SetSector(Vector2.zero, begin, end, inner, outer, _division);
		blocker.SetMaterial(_material);
		blocker.SetPhysicsMaterial(_phyMaterial);

		//親子の設定
		go.transform.SetParent(_parent.transform, false);
		return go;
	}
}
