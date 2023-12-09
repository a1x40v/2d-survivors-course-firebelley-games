using Godot;

public partial class Player : CharacterBody2D
{
	public const int Speed = 125;
	public const int AccelerationSmoothing = 25;

	private HealthComponent _healthComponent;
	private Timer _damageIntervalTimer;
	private int _numberCollidingBodies;

	public override void _Ready()
	{
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		_damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");
		_damageIntervalTimer.Timeout += OnDamageIntervalTimerTimeout;

		var collisionArea2D = GetNode<Area2D>("CollisionArea2D");
		collisionArea2D.BodyEntered += OnBodyEntered;
		collisionArea2D.BodyExited += OnBodyExited;
	}

	public override void _Process(double delta)
	{
		Vector2 movementVector = GetMovementVector();
		Vector2 direction = movementVector.Normalized();
		Vector2 targetVelocity = direction * Speed;

		Velocity = Velocity.Lerp(targetVelocity, 1 - Mathf.Exp(-(float)delta * AccelerationSmoothing));

		MoveAndSlide();
	}

	private Vector2 GetMovementVector()
	{
		float xMovement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		float yMovement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		return new Vector2(xMovement, yMovement);
	}

	private void CheckDealDamage()
	{
		if (_numberCollidingBodies == 0 || !_damageIntervalTimer.IsStopped()) return;
		_healthComponent.Damage(1);
		_damageIntervalTimer.Start();
		GD.Print(_healthComponent.CurrentHealth);
	}

	public void OnBodyEntered(Node2D otherBody)
	{
		_numberCollidingBodies += 1;
		CheckDealDamage();
	}

	public void OnBodyExited(Node2D otherBody)
	{
		_numberCollidingBodies -= 1;
	}

	public void OnDamageIntervalTimerTimeout()
	{
		CheckDealDamage();
	}
}
