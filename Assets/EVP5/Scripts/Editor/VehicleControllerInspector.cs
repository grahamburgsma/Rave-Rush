//------------------------------------------------------------------------------------------------
// Edy's Vehicle Physics
// (c) Angel Garcia "Edy" - Oviedo, Spain
// http://www.edy.es
//------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEditor;

namespace EVP
{

[CustomEditor(typeof(VehicleController))]
public class VehicleControllerInspector : Editor
	{
	TextureCanvas m_canvas = null;
	const int m_graphWidth = 242;
	const int m_graphHeight = 64;

	VehicleController m_target;


	public override void OnInspectorGUI ()
		{
		serializedObject.Update();
		m_target = (VehicleController)target;

		BeginInspectorContent(120);

		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("wheels"), true);

		// Handling

		EditorGUILayout.PropertyField(serializedObject.FindProperty("centerOfMass"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("tireFriction"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("antiRoll"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSteerAngle"));

		SetInspectorMinLabelWidth(150);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("aeroDrag"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("aeroDownforce"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("aeroAppPointOffset"));
		ResetInspectorMinLabelWidth();

		// Motor

		SetInspectorMinLabelWidth(150);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSpeedForward"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSpeedReverse"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxDriveForce"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxDriveSlip"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("driveForceToMaxSlip"));
		ResetInspectorMinLabelWidth();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("forceCurveShape"));

		Rect graphRect = AllocateRectForGraphic();

		// Brakes

		SetInspectorMinLabelWidth(150);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxBrakeForce"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("brakeForceToMaxSlip"));
		ResetInspectorMinLabelWidth();

		EditorGUILayout.Space();
		SerializedProperty brakeMode = serializedObject.FindProperty("brakeMode");
		EditorGUILayout.PropertyField(brakeMode);
		if (brakeMode.enumValueIndex == 0)
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxBrakeSlip"));
		else
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxBrakeRatio"));

		EditorGUILayout.Space();
		SerializedProperty handbrakeMode = serializedObject.FindProperty("handbrakeMode");
		EditorGUILayout.PropertyField(handbrakeMode);
		if (handbrakeMode.enumValueIndex == 0)
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHandbrakeSlip"));
		else
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHandbrakeRatio"));

		// Aids

		EditorGUILayout.PropertyField(serializedObject.FindProperty("tcEnabled"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("tcRatio"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("absEnabled"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("absRatio"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("espEnabled"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("espRatio"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("steerInput"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("throttleInput"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("brakeInput"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("handbrakeInput"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("spinUpdateRate"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelPositionMode"));

		SetInspectorMinLabelWidth(200);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("disallowRuntimeChanges"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("disableWheelHitCorrection"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("disableSteerAngleCorrection"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("showContactGizmos"));
		ResetInspectorMinLabelWidth();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("sleepVelocity"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("steeringOverdrive"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultGroundGrip"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultGroundDrag"));

		serializedObject.ApplyModifiedProperties();

		GUIDrawForceGraph(graphRect);
		EndInspectorContent();
		}



	Rect AllocateRectForGraphic ()
		{
		Rect graphRect = EditorGUILayout.GetControlRect(false, m_graphHeight + 5);
		graphRect.y += 5;
		graphRect.height -= 5;

		// Graph legend info

		GUIContent legend = new GUIContent("Hover here to legend",
			"Horizontal scale: speed (m/s)\n" +
			"Vertical scale: force (N)\n\n" +
			"Green: max drive force per drive wheel\n" +
			"Dashed white: max speed"
			);

		GUIStyle tmpStyle = new GUIStyle(EditorStyles.miniLabel);
		tmpStyle.alignment = TextAnchor.UpperCenter;
		tmpStyle.normal.textColor = Color.gray;
		EditorGUILayout.LabelField(legend, tmpStyle);

		return graphRect;
		}



	void GUIDrawForceGraph (Rect graphRect)
		{
		if (m_canvas == null || Event.current.type != EventType.Ignore)
			{
			if (m_canvas == null)
				{
				m_canvas = new TextureCanvas(m_graphWidth, m_graphHeight);
				m_canvas.alpha = 0.75f;
				m_canvas.color = Color.black;
				m_canvas.Clear();
				m_canvas.Save();
				}
			else
				{
				m_canvas.Restore();
				}

			m_target = (VehicleController)serializedObject.targetObject;

			// Calculate the dimmensions of the speed graph

			float maxSpeed = Mathf.Max(m_target.maxSpeedForward * 1.1f, 1.0f);
			float maxForce = Mathf.Max(m_target.maxDriveForce * 1.1f, 1.0f);

			// Set dimensions and draw grids

			m_canvas.rect = new Rect(0.0f, 0.0f, maxSpeed, maxForce);
			m_canvas.color = Color.green*0.1f;
			m_canvas.Grid(1.0f, 100.0f);
			m_canvas.color = Color.green*0.4f;
			m_canvas.Grid(10.0f, 1000.0f);

			// Origin lines

			m_canvas.color = Color.grey;
			m_canvas.HorizontalLine(0.0f);
			m_canvas.VerticalLine(0.0f);

			// Force graph

			CommonTools.BiasLerpContext biasCtx = new CommonTools.BiasLerpContext();

			m_canvas.color = Color.green;
			m_canvas.Function(x => m_target.maxDriveForce
				* CommonTools.BiasedLerp(1.0f - x/m_target.maxSpeedForward, m_target.forceCurveShape, biasCtx));

			// Limit lines

			m_canvas.color = Color.white;
			m_canvas.lineType = TextureCanvas.LineType.Dashed;
			m_canvas.Line(m_target.maxSpeedForward, 0.0f, m_target.maxSpeedForward, m_target.maxDriveForce);
			m_canvas.lineType = TextureCanvas.LineType.Solid;

			}

		// Non-scaled, horizontally centered, bottom-aligned, shadow effect

		m_canvas.EditorGUIDraw(graphRect);
		}



	// Label width utilities


	static float m_inspectorLabelWidth;
	static float m_inspectorMinLabelWidth;

	public static void BeginInspectorContent (float minLabelWidth = 0.0f)
		{
		m_inspectorLabelWidth = EditorGUIUtility.labelWidth;
		m_inspectorMinLabelWidth = minLabelWidth;
		ResetInspectorMinLabelWidth();
		}

	public static void EndInspectorContent ()
		{
		EditorGUIUtility.labelWidth = m_inspectorLabelWidth;
		}

	public static void SetInspectorMinLabelWidth (float minLabelWidth = 0.0f)
		{
		EditorGUIUtility.labelWidth = Mathf.Max(EditorGUIUtility.currentViewWidth * 0.4f, minLabelWidth);
		}

	public static void ResetInspectorMinLabelWidth ()
		{
		EditorGUIUtility.labelWidth = Mathf.Max(EditorGUIUtility.currentViewWidth * 0.4f, m_inspectorMinLabelWidth);
		}
	}
}