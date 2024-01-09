using Godot;

public partial class HitFlashComponent : Node
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }

	[Export]
	public ShaderMaterial HitFlashMaterial { get; set; }

	private Tween _hitFlashTween;

	public override void _Ready()
	{
		HealthComponent.Connect("HealthChanged", new Callable(this, nameof(OnHealthChanged)));
		Sprite.Material = HitFlashMaterial;
	}

	public void OnHealthChanged()
	{
		if (_hitFlashTween != null && _hitFlashTween.IsValid())
		{
			_hitFlashTween.Kill();
		}

		(Sprite.Material as ShaderMaterial).SetShaderParameter("lerp_procent", 1f);
		_hitFlashTween = CreateTween();
		_hitFlashTween.TweenProperty(Sprite.Material, "shader_parameter/lerp_procent", 0f, .2);
	}
}
