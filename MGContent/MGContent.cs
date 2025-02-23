using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace MGContent;

public class MGContent : Game
{
	#region rConst

	static readonly Point WINDOW_START_SIZE = new Point(900, 600);
	static readonly ImVec2 BROWSER_START_SIZE = new ImVec2(700.0f, 500.0f);

	const float MIN_WINDOW_SIZE = 120.0f;
	
	#endregion rConst





	#region rMembers

	// Render
	GraphicsDeviceManager mGraphics;
	ImGuiRenderer? mImGuiRenderer;

	// Windows
	ContentBrowser mContentBrowser;
	InfoPanel mInfoPanel;
	BuildPanel mBuildPanel;

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
									| ImGuiWindowFlags.NoMove;
		mContentBrowser = new ContentBrowser(commonFlags);
		mInfoPanel = new InfoPanel(commonFlags | ImGuiWindowFlags.NoResize);
		mBuildPanel = new BuildPanel(commonFlags | ImGuiWindowFlags.NoResize);

		mContentBrowser.RequestSize = BROWSER_START_SIZE;
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

		mGraphics.PreferredBackBufferWidth = WINDOW_START_SIZE.X;
		mGraphics.PreferredBackBufferHeight = WINDOW_START_SIZE.Y;
		mGraphics.ApplyChanges();

		mContentBrowser.SizeMinMax = (new ImVec2(MIN_WINDOW_SIZE * 2.0f, MIN_WINDOW_SIZE),
								new ImVec2(WINDOW_START_SIZE.X - MIN_WINDOW_SIZE, WINDOW_START_SIZE.Y - MIN_WINDOW_SIZE));

		// Init ImGui
		mImGuiRenderer = new ImGuiRenderer(this);
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
		ImVec2 browserSize = mContentBrowser.Size;

		ImVec2 browserRatio = new ImVec2(browserSize.X / mPrevWindowBounds.Width, browserSize.Y / mPrevWindowBounds.Height);

		browserSize.X = browserRatio.X * newWindowSize.X;
		browserSize.Y = browserRatio.Y * newWindowSize.Y;

		mContentBrowser.RequestSize = browserSize;

		mContentBrowser.SizeMinMax = (new ImVec2(MIN_WINDOW_SIZE * 2.0f, MIN_WINDOW_SIZE),
								new ImVec2(Window.ClientBounds.Width - MIN_WINDOW_SIZE, Window.ClientBounds.Height - MIN_WINDOW_SIZE));

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

		// Content browser
		mContentBrowser.Position = new ImVec2(0.0f, 0.0f);
		mContentBrowser.AddImGuiCommands(gameTime);

		ImVec2 contentSize = mContentBrowser.Size;

		// Info panel
		mInfoPanel.Position = new ImVec2(contentSize.X, 0.0f);
		mInfoPanel.RequestSize = new ImVec2(windowBounds.X - contentSize.X, windowBounds.Y);

		mInfoPanel.AddImGuiCommands(gameTime);

		// Build panel
		mBuildPanel.Position = new ImVec2(0.0f, contentSize.Y);
		mBuildPanel.RequestSize = new ImVec2(contentSize.X, windowBounds.Y - contentSize.Y);

		mBuildPanel.AddImGuiCommands(gameTime);
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
