using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export]
	public float Speed { get; set; } = 500.0f;
	// [Export]
	// public int Damage { get; set; } = 150;

	Vector2 direction;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(_OnBodyEntered)));

		LookAt(GetGlobalMousePosition());
		GetTree().CreateTimer(3).Timeout += QueueFree;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition += direction * Speed * (float)delta;
	}
	
	public void _OnBodyEntered(Node2D body)
	{
	// 	if (body.IsInGroup("enemies"))
	// 	{
	// 		body.GetDamage(Damage);
	// 		QueueFree();
	// 	}
	}

	public void SetDirection(Vector2 bulletDirection)
	{
		direction = bulletDirection;
		RotationDegrees = Mathf.RadToDeg(GlobalPosition.AngleToPoint(GlobalPosition + direction));
	}
}
