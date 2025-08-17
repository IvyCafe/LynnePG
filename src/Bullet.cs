using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	[Export]
	public float Speed { get; set; } = 10.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LookAt(GetGlobalMousePosition());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		MoveAndCollide(new Vector2(0, 0) * (float)delta);

		// RayCast
		CheckAllDirection();
	}

	public bool IsTouching(Vector2 direction, float distance = 10f)
	{
		var spaceState = GetWorld2D().DirectSpaceState;

		var query = PhysicsRayQueryParameters2D.Create(
			GlobalPosition,
			GlobalPosition + direction.Normalized() * distance
		);
		query.Exclude = new Godot.Collections.Array<Rid> { GetRid() };

		var result = spaceState.IntersectRay(query);
		return result.Count > 0;
	}

	public void CheckAllDirection()
	{
		if (IsTouching(Vector2.Down, 10f))
			QueueFree();
		if (IsTouching(Vector2.Up, 10f))
			QueueFree();
		if (IsTouching(Vector2.Left, 10f))
			QueueFree();
		if (IsTouching(Vector2.Right, 10f))
			QueueFree();
	}
}
