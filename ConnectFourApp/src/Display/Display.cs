using System;

namespace ConnectFour.Display
{
    class Display {
        private static readonly Display instance = new Display();
        private string currentScreen = "Test Screen";

        private Display() {
            DisplayText();
        }

        public static Display GetInstance() {
            return instance;
        }

        public void SetCurrentScreen(string _text) {
            currentScreen = _text;
        }

        public void DisplayText() {
            Console.SetCursorPosition(0,0);
            Console.Write(currentScreen);
        }
    }
}