using Godot;
using System;

public partial class Gun : CharacterBody2D
{
	public override void _PhysicsProcess(double delta)
	{
		LookAt(GetGlobalMousePosition());
		MoveAndSlide();
	}
}
