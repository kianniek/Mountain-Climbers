using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    public class ButtonManager
    {
        //Big Player Buttons
        public static Keys Left_BigPlayer = Keys.A; // Dpad-Left
        public static Keys Right_BigPlayer = Keys.D; //Dpad-Right
        public static Keys Up_BigPlayer = Keys.W; //Dpad-Up And X-BLUE button BigPlayer
        public static Keys Down_BigPlayer = Keys.S; //Dpad-Down

        public static Keys AimL_BigPlayer = Keys.O; // LB_Button
        public static Keys AimR_BigPlayer = Keys.P; // RB_Button
        public static Keys Throw_BigPlayer = Keys.X; // Y-Green button Bigplayer

        public static Keys Interact_Bigplayer = Keys.E; // A-RED button on controller BigPlayer
        public static Keys Sprint_Bigplayer = Keys.LeftShift; // B-Yellow button on controller BigPlayer

        //Small Player Button
        public static Keys Left_SmallPlayer = Keys.Left; //Dpad-Left
        public static Keys Right_SmallPlayer = Keys.Right;//Dpad-Right
        public static Keys Up_SmallPlayer = Keys.Up; //Dpad-Up And X-BLUE button BigPlayer
        public static Keys Down_SmallPlayer = Keys.Down; //Dpad-Down
        public static Keys SelfEject_SmallPlayer = Keys.X; //Y-Green button Smallplayer

        public static Keys DogeL_SmallPlayer = Keys.OemComma; // LB_Button
        public static Keys DogeR_SmallPlayer = Keys.OemPeriod; // RB_Button
        public static Keys Interact_SmallPlayer = Keys.M; // A-RED button on controller SmallPlayer
        public static Keys Sprint_SmallPlayer = Keys.RightShift; // B-Yellow button on controller SmallPlayer

        public static Keys Start = Keys.G; //Start button on both controlers
        public static Keys Select = Keys.H; //Select button on both controlers



        public static string interract_Button = "ButtonPrompts/SNES_A";
        public static string Y_Button = "ButtonPrompts/Switch_Y";
        public static string sprint_Button = "ButtonPrompts/SNES_B";
        public static string jump_Button = "ButtonPrompts/Switch_X";
        public static string Dpad_Button = "ButtonPrompts/SNES_Dpad";
        public static string Dpad_Down_Button = "ButtonPrompts/SNES_Dpad_Down";
        public static string ADpad_Left_Button = "ButtonPrompts/SNES_Dpad_Left";
        public static string Dpad_Right_Button = "ButtonPrompts/SNES_Dpad_Right";
        public static string Dpad_Up_Button = "ButtonPrompts/SNES_Dpad_Up";
        public static string LB_Button = "ButtonPrompts/SNES_LB";
        public static string RB_Button = "ButtonPrompts/SNES_RB";
        public static string Select_Button = "ButtonPrompts/SNES_Select";
        public static string Start_Button = "ButtonPrompts/SNES_Start";

        public static readonly Dictionary<string, SpriteGameObject> buttonPrompts = new Dictionary<string, SpriteGameObject>
        {
            { interract_Button,new SpriteGameObject(interract_Button) },
            { Y_Button,new SpriteGameObject(Y_Button) },
            { sprint_Button,new SpriteGameObject(sprint_Button) },
            { jump_Button,new SpriteGameObject (jump_Button) },
            { Dpad_Button,new SpriteGameObject(Dpad_Button)},
            { Dpad_Down_Button,new SpriteGameObject(Dpad_Down_Button)},
            { ADpad_Left_Button,new SpriteGameObject(ADpad_Left_Button)},
            { Dpad_Right_Button,new SpriteGameObject(Dpad_Right_Button)},
            { Dpad_Up_Button,new SpriteGameObject(Dpad_Up_Button)},
            { LB_Button,new SpriteGameObject(LB_Button)},
            { RB_Button,new SpriteGameObject(RB_Button)},
            { Select_Button,new SpriteGameObject(Select_Button)},
            { Start_Button,new SpriteGameObject(Start_Button)},
        };
    }
}
