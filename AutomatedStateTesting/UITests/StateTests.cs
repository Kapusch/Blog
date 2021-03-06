﻿using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;

namespace AutomatedStateTesting.UITests
{
  //  [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [Test]
        public void StateTest()
        {
            app = AppInitializer.StartApp(platform);

            // Set values
            app.Tap("Button");
            app.Tap("Button");
            app.EnterText("Text", "Some text");

            SaveAppState();

            // Restart the app
            app = AppInitializer.StartApp(platform, true);

            // Verify vlues
            app.WaitForElement("Clicked 2 Times");
            app.WaitForElement(x => x.Marked("Text").Text("Some text"));
        }

        private void SaveAppState()
        {
            if (platform == Platform.iOS)
            {
                app.Invoke("SuspendApp:", "");
            }
            else
            {
                app.Invoke("SuspendApp");
            }
        }
    }
}
