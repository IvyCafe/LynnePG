using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 300.0f;
	[Export]
	public float JumpVelocity { get; set; } = -600.0f;
	[Export]
	public float Gravity { get; set; } = 1.0f;

	private AnimatedSprite2D _lynneSprite;

	public override void _Ready()
	{
		_lynneSprite = GetNode<AnimatedSprite2D>("LynneSprite");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity += GetGravity() * (float)delta * Gravity;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "jump", "squat");
		if (direction != Vector2.Zero)
			velocity.X = direction.X * Speed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		Velocity = velocity;
		MoveAndSlide();

		_lynneSprite.Play("idle");
		UpdateFacingDirection();
	}

	public void UpdateFacingDirection()
	{
		if (Velocity.X < 0)
			_lynneSprite.FlipH = true;
		else if (Velocity.X > 0)
			_lynneSprite.FlipH = false;
	}
}
