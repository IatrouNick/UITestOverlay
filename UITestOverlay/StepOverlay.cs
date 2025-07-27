using Microsoft.Playwright;
using System.Threading.Tasks;


namespace UITestOverlay
{
    public static class StepOverlay
    {

        private const string InjectScript = @"
            if (!window.__stepOverlay) {
                const style = document.createElement('style');
                style.textContent = `
                #step-visualizer {
                    position: fixed;
                    bottom: 70px;
                    right: 10px;
                    z-index: 99999;
                    background: rgba(0, 0, 0, 0.7);
                    color: white;
                    font-family: sans-serif;
                    padding: 10px;
                    border-radius: 4px; 
                    max-width: 300px;
                    max-height: 140px;
                    overflow-y: auto;
                    display: flex;
                    flex-direction: column;
                }

                #step-visualizer > div {
                    margin-bottom: 5px;
                    line-height: 1.4em;
                }

                #step-visualizer > div.current {
                    font-weight: bold;
                    color: #0f0;
                }
            `;

                document.head.appendChild(style);

                const container = document.createElement('div');
                container.id = 'step-visualizer';
                document.body.appendChild(container);

                window.__stepOverlay = {
                    steps: [],
                    addStep: function(stepText) {
                        const div = document.createElement('div');
                        div.textContent = stepText;

                        // Remove current from existing children
                        Array.from(container.children).forEach(child => child.classList.remove('current'));

                        div.classList.add('current');
                        container.insertBefore(div, container.firstChild); // insert at top

                        // Scroll to top so the newest step is visible
                        container.scrollTop = 0;
                    },
                    clear: function() {
                        container.innerHTML = '';
                    }
                };
            }";

        public static async Task InitAsync(IPage page)
        {
            await page.EvaluateAsync(InjectScript);
        }

        public static async Task ShowAsync(IPage page, string stepDescription)
        {
            await page.EvaluateAsync(@"(step) => {
            if (window.__stepOverlay) {
                window.__stepOverlay.addStep(step);
            }
        }", stepDescription);
        }

        public static async Task ClearAsync(IPage page)
        {
            await page.EvaluateAsync(@"() => {
            if (window.__stepOverlay) {
                window.__stepOverlay.clear();
            }
        }");
        }
    }
}
