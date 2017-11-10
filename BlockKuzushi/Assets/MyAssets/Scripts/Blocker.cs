using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Blocker : MonoBehaviour
{
	private void Awake()
	{
		gameObject.AddComponent<PolygonCollider2D>();
	}

	public void SetSector(Vector2 origin,float beginAngle,float endAngle,float innerRadius,float outerRadius,int division)
	{
		var collider = GetComponent<PolygonCollider2D>();
		var mf = GetComponent<MeshFilter>();
		var mesh = new Mesh();

		MeshCreator.createSectorAndCollider(origin, beginAngle, endAngle, innerRadius, outerRadius, division, ref mesh, ref collider);
		mf.mesh = mesh;
	}

	public void SetMaterial(Material mat)
	{
		var render = GetComponent<MeshRenderer>();
		render.material = mat;
	}

	public void SetPhysicsMaterial(PhysicsMaterial2D mat)
	{
		var collider = GetComponent<PolygonCollider2D>();
		collider.sharedMaterial = mat;
	}
}
