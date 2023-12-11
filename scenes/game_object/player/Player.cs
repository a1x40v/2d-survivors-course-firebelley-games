using Godot;
using Godot.Collections;

public partial class Player : CharacterBody2D
{
	public const int Speed = 125;
	public const int AccelerationSmoothing = 25;

	private Node _abilities;
	private Node2D _visuals;
	private AnimationPlayer _animationPlayer;
	private ProgressBar _healthBar;
	private Timer _damageIntervalTimer;
	private int _numberCollidingBodies;

	public HealthComponent HealthComponent { get; set; }

	public override void _Ready()
	{
		_abilities = GetNode<Node>("Abilities");
		_visuals = GetNode<Node2D>("Visuals");

		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		HealthComponent = GetNode<HealthComponent>("HealthComponent");
		_healthBar = GetNode<ProgressBar>("HealthBar");
		_damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");
		_damageIntervalTimer.Timeout += OnDamageIntervalTimerTimeout;

		var collisionArea2D = GetNode<Area2D>("CollisionArea2D");
		collisionArea2D.BodyEntered += OnBodyEntered;
		collisionArea2D.BodyExited += OnBodyExited;

		HealthComponent.Connect("HealthChanged", new Callable(this, nameof(OnHealthChanged)));

		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.Connect("AbilityUpgradeAdded", new Callable(this, nameof(OnAbilityUpgradeAdded)));

		UpdateHealthDisplay();
	}

	public override void _Process(double delta)
	{
		Vector2 movementVector = GetMovementVector();
		Vector2 direction = movementVector.Normalized();
		Vector2 targetVelocity = direction * Speed;

		Velocity = Velocity.Lerp(targetVelocity, 1 - Mathf.Exp(-(float)delta * AccelerationSmoothing));

		MoveAndSlide();

		if (movementVector.X != 0 || movementVector.Y != 0)
		{
			_animationPlayer.Play("walk");
		}
		else
		{
			_animationPlayer.Play("RESET");
		}

		var moveSign = Mathf.Sign(movementVector.X);
		if (moveSign != 0)
		{
			_visuals.Scale = new Vector2(moveSign, 1);
		}

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
		HealthComponent.Damage(1);
		_damageIntervalTimer.Start();
	}

	private void UpdateHealthDisplay()
	{
		_healthBar.Value = HealthComponent.GetHealthPercent();
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

	public void OnHealthChanged()
	{
		UpdateHealthDisplay();
	}

	public void OnAbilityUpgradeAdded(AbilityUpgrade upgrade, Dictionary<string, Dictionary> currentUpgrades)
	{
		if (upgrade is not Ability) return;

		_abilities.AddChild((upgrade as Ability).AbilityControllerScene.Instantiate());
	}
}
