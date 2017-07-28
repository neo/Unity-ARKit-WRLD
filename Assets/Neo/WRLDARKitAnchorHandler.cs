using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class WRLDARKitAnchorHandler : MonoBehaviour 
{
	private UnityARAnchorManager m_unityARAnchorManager;
	[SerializeField]
	private Transform m_wrldMapParent;

	[SerializeField]
	private Transform m_wrldMapMask;

	void Start()
	{
		//Create an instance of UnityARAnchorManager so we can get details for anchors
		m_unityARAnchorManager = new UnityARAnchorManager();
	}

	void Update()
	{
		//Getting a list of all anchors
		List<ARPlaneAnchorGameObject> arpags = m_unityARAnchorManager.GetCurrentPlaneAnchors ();

		//For the sake of this tutorial we will simply position our map on the first anchor that we find
		if (arpags.Count >= 1) 
		{
			ARPlaneAnchor arPlaneAnchor = arpags [0].planeAnchor;

			//Setting the position of our map to match the position of anchor
			m_wrldMapParent.position = UnityARMatrixOps.GetPosition (arPlaneAnchor.transform);

			//Updating our mask according to the ARKit anchor
			m_wrldMapMask.rotation = UnityARMatrixOps.GetRotation (arPlaneAnchor.transform);
			m_wrldMapMask.localPosition = new Vector3(arPlaneAnchor.center.x, arPlaneAnchor.center.y, -arPlaneAnchor.center.z);
			m_wrldMapMask.localScale  = new Vector3(arPlaneAnchor.extent.x, m_wrldMapMask.localScale.y, arPlaneAnchor.extent.z);
		}
	}
}
