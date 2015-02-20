using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace IoTLab.Device
{
    public partial class Program
    {
        private bool lightOn = false;
        void ProgramStarted()
        {
            led7C.SetColor(LED7C.Color.Off);
            button.ButtonPressed += button_ButtonPressed;
            Debug.Print("Program Started");
        }
        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            lightOn = !lightOn;
            led7C.SetColor(lightOn ? LED7C.Color.Green : LED7C.Color.Off );
            Debug.Print("Light " + (lightOn ? "On" : "Off"));
        }
    }
}
