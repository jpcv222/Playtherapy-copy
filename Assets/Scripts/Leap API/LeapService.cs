/**
 * 
**/
using Leap;
namespace LeapAPI
{
    public static class LeapService
    {
        //public static LeapService leapService;
        private static Leap.Controller leap_controller_;

        //public void CreateService()
        //{
            //leapService = new LeapService();
        //}

        private static void InitializeFlags()
        {
            if (leap_controller_ == null)
            {
                return;
            }

            leap_controller_.ClearPolicy(Leap.Controller.PolicyFlag.POLICY_OPTIMIZE_HMD);

        }

        public static void DestroyController()
        {
            if (leap_controller_ != null)
            {
                if (leap_controller_.IsConnected)
                {
                    leap_controller_.ClearPolicy(Leap.Controller.PolicyFlag.POLICY_OPTIMIZE_HMD);
                }
                leap_controller_.StopConnection();
                leap_controller_ = null;
            }
        }

        public static void CreateController()
        {
            if (leap_controller_ != null)
            {
                DestroyController();
            }

            leap_controller_ = new Leap.Controller();
            if (leap_controller_.IsConnected)
            {
                InitializeFlags();
            }
            else
            {
                leap_controller_.Device += onHandControllerConnect;
            }

        }

        private static void onHandControllerConnect(object sender, LeapEventArgs args)
        {
            InitializeFlags();
            leap_controller_.Device -= onHandControllerConnect;
        }

        public static Leap.Controller GetLeapController()
        {
            //Null check to deal with hot reloading
            if (leap_controller_ == null)
            {
                CreateController();
            }
            return leap_controller_;
        }

        public static Frame GetFrame()
        {
            if (leap_controller_ == null)
            {
                CreateController();
            }
            return leap_controller_.Frame();
        }

        public static bool IsConected()
        {
            return leap_controller_.IsConnected;
        }
    }

}
