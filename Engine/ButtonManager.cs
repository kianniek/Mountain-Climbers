using Microsoft.Xna.Framework.Input;

namespace BaseProject.Engine
{
    public class ButtonManager
    {
        //Big Player Buttons
        protected readonly Keys Left_BigPlayer = Keys.A;
        protected readonly Keys Right_BigPlayer = Keys.D;
        protected readonly Keys Up_BigPlayer = Keys.W;
        protected readonly Keys Down_BigPlayer = Keys.S;

        protected readonly Keys Interact_Bigplayer = Keys.E;
        protected readonly Keys Sprint_Bigplayer = Keys.LeftShift;

        //Small Player Button
        protected readonly Keys Left_SmallPlayer = Keys.Left;
        protected readonly Keys Right_SmallPlayer = Keys.Right;
        protected readonly Keys Up_SmallPlayer = Keys.Up;
        protected readonly Keys Down_SmallPlayer = Keys.Down;

        protected readonly Keys Interact_SmallPlayer = Keys.E;
        protected readonly Keys Sprint_SmallPlayer = Keys.RightShift;


        protected readonly string A_Button = "ButtonPrompts/SNES_A.png";
        protected readonly string Y_Button = "ButtonPrompts/Switch_Y.png";
        protected readonly string B_Button = "ButtonPrompts/SNES_B.png";
        protected readonly string X_Button = "ButtonPrompts/Switch_X.png";
        protected readonly string Dpad_Button = "ButtonPrompts/SNES_Dpad.png";
        protected readonly string Dpad_Down_Button = "ButtonPrompts/SNES_Dpad_Down.png";
        protected readonly string ADpad_Left_Button = "ButtonPrompts/SNES_Dpad_Left.png";
        protected readonly string Dpad_Right_Button = "ButtonPrompts/SNES_Dpad_Right.png";
        protected readonly string Dpad_Up_Button = "ButtonPrompts/SNES_Dpad_Up.png";
        protected readonly string LB_Button = "ButtonPrompts/SNES_LB.png";
        protected readonly string RB_Button = "ButtonPrompts/SNES_RB.png";
        protected readonly string Select_Button = "ButtonPrompts/SNES_Select.png";
        protected readonly string Start_Button = "ButtonPrompts/SNES_Start.png";
    }
}
