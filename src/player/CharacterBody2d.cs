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
	private Vector2 _gunSpriteDefaultOffset;
	private Vector2 _gunSpriteDefaultPosition;
	private Gun _gunSprite;

	[Export]
	public float GunSpeed { get; set; } = 1000.0f;
	readonly PackedScene bullet = GD.Load<PackedScene>("res://src/bullet.tscn");

	public override void _Ready()
	{
		_lynneSprite = GetNode<AnimatedSprite2D>("LynneSprite");
		_gunSprite = GetNode<Gun>("Gun");
		_gunSpriteDefaultOffset = new Vector2(_gunSprite.Offset.X, _gunSprite.Offset.Y);
		_gunSpriteDefaultPosition = new Vector2(_gunSprite.Position.X, _gunSprite.Position.Y);
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

		// Gun and Bullet
		// if (Input.IsActionJustPressed("click"))
		// 	_gunSprite.Shoot();

		// if (direction != Vector2.Zero)
		// 	_gunSprite.SetupDirection(direction);

		// Animation
		if (velocity.X != 0)
			_lynneSprite.Play("running");
		else
			_lynneSprite.Play("idle");

		UpdateFacingDirection();
	}

	public void UpdateFacingDirection()
	{
		if (Velocity.X < 0)
		{
			_lynneSprite.FlipH = true;
			_gunSprite.FlipH = true;
			_gunSprite.Offset = new Vector2(_gunSpriteDefaultOffset.X * -1, _gunSpriteDefaultOffset.Y);
			_gunSprite.Position = new Vector2(_gunSpriteDefaultPosition.X * -1, _gunSpriteDefaultPosition.Y);
		}
		else if (Velocity.X > 0)
		{
			_lynneSprite.FlipH = false;
			_gunSprite.FlipH = false;
			_gunSprite.Offset = _gunSpriteDefaultOffset;
			_gunSprite.Position = _gunSpriteDefaultPosition;
		}
	}
}
