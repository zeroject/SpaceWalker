using Godot;
using System;

public partial class CameraFollow : Node2D
{
	[Export] public Node2D toFollow {get; private set;}
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (toFollow == null)
			return;

		GlobalPosition = GlobalPosition.Lerp(toFollow.GlobalPosition, 1f * (float) delta);
	}
}
