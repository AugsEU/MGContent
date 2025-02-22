namespace MGContent;

public class MGContent : Game
{
	GraphicsDeviceManager mGraphics;
	ImGuiRenderer mImGuiRenderer;

	public MGContent() : base()
	{
		mGraphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		IsMouseVisible = true;
	}

	protected override void Initialize()
	{
		Window.AllowUserResizing = true;
		Window.AllowAltF4 = true;
		Window.Title = "MGContent++";

		base.Initialize();

		mImGuiRenderer = new ImGuiRenderer(this);
	}

	protected override void LoadContent()
	{
	}

	protected override void Update(GameTime gameTime)
	{
		if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.Black);

		mImGuiRenderer.BeforeLayout(gameTime);

		ImGui.Text("Hello");

		mImGuiRenderer.AfterLayout();

		base.Draw(gameTime);
	}
}
