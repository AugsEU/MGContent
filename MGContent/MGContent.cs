using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace MGContent;

public class MGContent : Game
{
	#region rConst

	static readonly Point WINDOW_START_SIZE = new Point(900, 600);
	static readonly ImVec2 BROWSER_START_SIZE = new ImVec2(700.0f, 500.0f);

	const float MIN_WINDOW_SIZE = 50.0f;
	
	#endregion rConst





	#region rMembers

	// Render
	GraphicsDeviceManager mGraphics;
	ImGuiRenderer? mImGuiRenderer;

	// Windows
	ContentBrowser mContentBrowser;
	InfoPanel mInfoPanel;

	// Game
	Rectangle mPrevWindowBounds;

	#endregion rMembers





	#region rInit

	/// <summary>
	/// Create members
	/// </summary>
	public MGContent() : base()
	{
		mGraphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		IsMouseVisible = true;

		// Windows
		ImGuiWindowFlags commonFlags = ImGuiWindowFlags.NoTitleBar
									| ImGuiWindowFlags.NoCollapse
									| ImGuiWindowFlags.NoTitleBar
									| ImGuiWindowFlags.NoMove;
		mContentBrowser = new ContentBrowser(commonFlags);
		mInfoPanel = new InfoPanel(commonFlags | ImGuiWindowFlags.NoResize);

		mContentBrowser.RequestWindowSize(BROWSER_START_SIZE);
	}



	/// <summary>
	/// Called once things are initialised.
	/// </summary>
	protected override void Initialize()
	{
		Window.AllowUserResizing = true;
		Window.AllowAltF4 = true;
		Window.Title = "MGContent++";
		Window.ClientSizeChanged += OnWindowResize;

		mPrevWindowBounds = Window.ClientBounds;
		mPrevWindowBounds.Width = WINDOW_START_SIZE.X;
		mPrevWindowBounds.Height = WINDOW_START_SIZE.Y;

		IsFixedTimeStep = true;
		TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);

		base.Initialize();

		mImGuiRenderer = new ImGuiRenderer(this);

		mGraphics.PreferredBackBufferWidth = WINDOW_START_SIZE.X;
		mGraphics.PreferredBackBufferHeight = WINDOW_START_SIZE.Y;
		mGraphics.ApplyChanges();
	}



	/// <summary>
	/// Load textures and resources for app.
	/// </summary>
	protected override void LoadContent()
	{
	}

	#endregion rInit





	#region rUpdate

	/// <summary>
	/// Update app.
	/// </summary>
	protected override void Update(GameTime gameTime)
	{
		if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();

		base.Update(gameTime);
	}



	/// <summary>
	/// Called when windows resize.
	/// </summary>
	void OnWindowResize(object? sender, EventArgs e)
	{
		ImVec2 newWindowSize = new ImVec2(Window.ClientBounds.Width, Window.ClientBounds.Height);
		ImVec2 browserSize = mContentBrowser.GetWindowSize();

		ImVec2 browserRatio = new ImVec2(browserSize.X / mPrevWindowBounds.Width, browserSize.Y / mPrevWindowBounds.Height);

		browserSize.X = browserRatio.X * newWindowSize.X;
		browserSize.Y = browserRatio.Y * newWindowSize.Y;

		mContentBrowser.RequestWindowSize(browserSize);

		mPrevWindowBounds = Window.ClientBounds;
	}

	#endregion rUpdate





	#region rDraw

	/// <summary>
	/// Draw app.
	/// </summary>
	protected override void Draw(GameTime gameTime)
	{
		if (mImGuiRenderer is null) throw new NullReferenceException();

		GraphicsDevice.Clear(Color.Black);

		mImGuiRenderer.BeforeLayout(gameTime);

		// Menu
		DrawPanels(gameTime);

		mImGuiRenderer.AfterLayout();

		base.Draw(gameTime);
	}

	#endregion rDraw





	#region rUtil

	/// <summary>
	/// Layout the 3 windows and draw them.
	/// </summary>
	void DrawPanels(GameTime gameTime)
	{
		ImVec2 windowBounds = GetImVecWindowSize();

		// Update content browser
		mContentBrowser.RequestWindowPos(0.0f, 0.0f);
		mContentBrowser.AddImGuiCommands(gameTime);

		ImVec2 contentSize = mContentBrowser.GetWindowSize();

		//contentSize.X = Math.Min(contentSize.X, windowBounds.X - MIN_WINDOW_SIZE);
		//contentSize.Y = Math.Min(contentSize.Y, windowBounds.Y - MIN_WINDOW_SIZE);

		//mContentBrowser.RequestWindowSize(contentSize);

		mInfoPanel.RequestWindowPos(contentSize.X, 0.0f);
		mInfoPanel.RequestWindowSize(windowBounds.X - contentSize.X, windowBounds.Y);

		mInfoPanel.AddImGuiCommands(gameTime);
	}



	/// <summary>
	/// Get window size as an ImVec2
	/// </summary>
	ImVec2 GetImVecWindowSize()
	{
		return new ImVec2(Window.ClientBounds.Width, Window.ClientBounds.Height);
	}

	#endregion rUtil
}
