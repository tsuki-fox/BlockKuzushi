using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

[AddComponentMenu("Generals/BlockEmitter")]
public class BlockEmitter : MonoBehaviour
{
	[System.Serializable]
	public class BlockRing
	{
		/// <summary>生成個数</summary>
		public int emitNum;
		/// <summary>角度オフセット</summary>
		public float angleOffset;
		/// <summary>ブロック間の余白</summary>
		public float margin;
		/// <summary>距離</summary>
		public float distance;
		/// <summary>ブロックの太さ</summary>
		public float thickness;
	}

	[SerializeField, Header("ブロックプレハブ")]
	GameObject _sourceBlock;
	[SerializeField, Header("メッシュ分割数")]
	int _meshDivisions;
	[SerializeField, Header("マテリアル")]
	Material _material;
	[SerializeField, Header("物理マテリアル")]
	PhysicsMaterial2D _phyMaterial2D;

	[SerializeField, Header("複層設定")]
	List<BlockRing> _blockRings = new List<BlockRing>();

	[SerializeField]
	GameObject _blockRoot = null;
	void EmitBlock(float angle,float size,float distance,float thickness)
	{
		var go = Instantiate(_sourceBlock);

		float begin = angle + size / 2f;
		float end = angle - size / 2f;
		float inner = distance - thickness / 2f;
		float outer = distance + thickness / 2f;

		var mesh = new Mesh();
		var meshFilter = go.GetComponent<MeshFilter>();
		var collider = go.GetComponent<PolygonCollider2D>();
		var render = go.GetComponent<MeshRenderer>();

		MeshCreator.createSectorAndCollider(transform.position, begin, end, inner, outer, _meshDivisions, ref mesh, ref collider);
		meshFilter.mesh = mesh;
		render.material = _material;
		collider.sharedMaterial = _phyMaterial2D;

		go.transform.SetParent(_blockRoot.transform, false);
	}

	void EmitBlockRing(BlockRing blockRing)
	{
		float angle = 0f;
		float size = 360f / blockRing.emitNum - blockRing.margin;
		for (int f1 = 0; f1 < blockRing.emitNum; f1++)
		{
			EmitBlock(angle+blockRing.angleOffset, size, blockRing.distance, blockRing.thickness);
			angle += size + blockRing.margin;
		}
	}

	void EmitAll()
	{
		//ルートオブジェクト生成
		_blockRoot = new GameObject("Blocks");
		_blockRoot.transform.SetParent(transform, false);
		foreach (var item in _blockRings)
			EmitBlockRing(item);
	}

	void DeleteAllOnEditor()
	{
#if UNITY_EDITOR
		EditorApplication.delayCall += () => DestroyImmediate(_blockRoot);
#endif
	}

	void Start()
	{
		EmitAll();
	}

	void OnBorrow()
	{
		EmitAll();
	}

	void OnRepay()
	{
		Destroy(_blockRoot);
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(BlockEmitter))]
	public class BlockEmitterInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var self = target as BlockEmitter;

			if (GUILayout.Button("ReEmitAll"))
				self.EmitAll();
			if (GUILayout.Button("DeleteAll"))
				self.DeleteAllOnEditor();
			base.OnInspectorGUI();
		}
	}
#endif
}
