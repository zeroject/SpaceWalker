using Godot;
using Godot.NativeInterop;
using System;

[Tool]
public partial class InnerGravityRange : Line2D
{
	int resolution;
	float radius;

	public override void _Ready()
	{
		if (!Engine.IsEditorHint())
			QueueFree();
	}

	public override void _Process(double delta)
	{
		if (Engine.IsEditorHint()) {
			radius = GetParent<Gravity>().innerRadius;
			resolution = GetParent<Gravity>().gravityRangeResolution;
			CreateRadiusPoints();
		}
	}

	private void CreateRadiusPoints() {
		Vector2 start = Vector2.Up * radius;
		Vector2[] newPoints = new Vector2[resolution + 1];

		float theta = Mathf.DegToRad(360f / resolution);

		for (int i = 0; i < newPoints.Length; i++) {
			start = start.Rotated(theta);
			newPoints[i] = new Vector2(start.X, start.Y);
		}	

		this.Points = newPoints;
	}
}