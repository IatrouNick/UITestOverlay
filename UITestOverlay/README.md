UITestOverlay
UITestOverlay is a lightweight .NET library that adds a visual overlay to your browser during Playwright UI automation. It shows the current test step and keeps a running history — perfect for debugging, demos, and presentations.

✨ Features
✅ Shows the current test step in real-time
✅ Keeps a visible, scrollable step history
✅ Stays fixed in the browser (bottom-right corner)
✅ Works out-of-the-box with Playwright
✅ .NET 8 compatible

📦 Installation
Install via NuGet:

dotnet add package UITestOverlay

🚀 Quick Usage

🔧 1. Initialize the Overlay
Call once before your first step:

await StepOverlay.InitAsync(page);
🧠 This injects the necessary CSS + HTML into the page.
-----------------------------------------------------------------

✏️ 2. Show Steps as They Happen
Call this before (or after) any test action:

await StepOverlay.ShowAsync(page, "🧪 Filling login form");
📋 The latest step will appear highlighted, and previous ones stay visible.
-----------------------------------------------------------------

🧹 3. Clear the Overlay (optional)
Call when the test finishes or resets:

await StepOverlay.ClearAsync(page);