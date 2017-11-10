using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshCreator
{
	public static Mesh createSector(Vector3 origin, float beginAngle, float endAngle, float innerRadius, float outerRadius, int division)
	{
		Mesh mesh = new Mesh();

		int vCount = division * 4;
		int iCount = division * 6;

		//頂点座標
		Vector3[] vertices = new Vector3[vCount];
		int[] triangles = new int[iCount];

		//頂点座標割り当て
		for (int i = 0; i < division; i++)
		{
			float angle = Mathf.Lerp(beginAngle, endAngle, (float)i / division) * Mathf.Deg2Rad;
			float nextAngle = Mathf.Lerp(beginAngle, endAngle, (float)(i + 1) / division) * Mathf.Deg2Rad;

			vertices[i * 4 + 0] = new Vector3(innerRadius * Mathf.Cos(angle), innerRadius * Mathf.Sin(angle), 0f);
			vertices[i * 4 + 1] = new Vector3(innerRadius * Mathf.Cos(nextAngle), innerRadius * Mathf.Sin(nextAngle),0f);
			vertices[i * 4 + 2] = new Vector3(outerRadius * Mathf.Cos(angle), outerRadius * Mathf.Sin(angle),0f);
			vertices[i * 4 + 3] = new Vector3(outerRadius * Mathf.Cos(nextAngle), outerRadius * Mathf.Sin(nextAngle),0f);
		}

		//頂点インデックス割り当て
		int index = 0;
		for (int i = 0; i < division; i++)
		{
			triangles[index++] = (i * 4) + 1;
			triangles[index++] = (i * 4) + 0;
			triangles[index++] = (i * 4) + 2;

			triangles[index++] = (i * 4) + 2;
			triangles[index++] = (i * 4) + 3;
			triangles[index++] = (i * 4) + 1;
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;

		mesh.name = "sector";
		return mesh;
	}

	public static void createSectorAndCollider(Vector3 origin, float beginAngle, float endAngle, float innerRadius, float outerRadius, int division,ref Mesh mesh,ref PolygonCollider2D collider)
	{
		//メッシュ
		var sector = createSector(origin, beginAngle, endAngle, innerRadius, outerRadius, division);
		mesh = sector;

		//コライダーの生成
		var res = new List<Vector2>();
		for (int i = 0; i < division; i++)
		{
			var p1 = sector.vertices[i * 4 + 0];
			var p2 = sector.vertices[i * 4 + 1];
			res.Add(p1);
			res.Add(p2);
		}

		for (int i = division - 1; i >= 0; i--)
		{
			var p1 = sector.vertices[i * 4 + 2];
			var p2 = sector.vertices[i * 4 + 3];
			res.Add(p2);
			res.Add(p1);
		}

		collider.points = res.ToArray();
	}
}
