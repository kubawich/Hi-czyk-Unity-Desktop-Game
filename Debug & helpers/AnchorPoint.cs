using System;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPoint : MonoBehaviour {

	public float GizmoSize = 0.5f;
	public float SphereScale = 0.1f;
	public bool SpherePoint = true;
	public Color ShpereColor = Color.red;
	public Color FrontColor = Color.blue;
	public Color LineColor = Color.green;

	Color shpereColor = new Color(0,0,0,0.3f);

	void OnDrawGizmos(){
		Color tmp = Gizmos.color;
		{
			Gizmos.color = ShpereColor;
			Gizmos.DrawWireSphere (transform.position, GizmoSize);
			Gizmos.color = FrontColor;
			Gizmos.DrawRay (transform.position,transform.forward *  GizmoSize * 2);
			if (SpherePoint) {
				Gizmos.color = shpereColor;
				Gizmos.DrawSphere (transform.position, SphereScale * GizmoSize);
			}
		}
		Gizmos.color = tmp;
	}
}
